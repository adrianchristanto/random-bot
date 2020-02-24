using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class MusicProcess
    {
        public int ProcessId { get; set; }
        public Queue<string> Queue { get; set; }
    }

    public class VoiceChannelService : IDisposable
    {
        public VoiceChannelService(DiscordSocketClient discordSocketClient)
        {
            this.Client = discordSocketClient;
        }
        private readonly DiscordSocketClient Client;
        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();
        private readonly ConcurrentDictionary<IAudioClient, MusicProcess> MusicQueue = new ConcurrentDictionary<IAudioClient, MusicProcess>();

        public void Dispose()
        {
            this.Client.Dispose();
        }

        public SocketCommandContext Context { get; set; }
        public IVoiceChannel VoiceChannel { get; set; }

        public IVoiceChannel GetVoiceChannel()
        {
            var guildUser = this.Context.Message.Author as IGuildUser;
            return guildUser?.VoiceChannel;
        }

        public async Task<IAudioClient> JoinVoiceChannel()
        {
            if (this.VoiceChannel == null)
            {
                await this.Context.Channel.SendMessageAsync("You're not in a voice channel <:gigaPepega:547315752949121024>");
                return null;
            }

            var audioClient = await this.VoiceChannel.ConnectAsync();
            var channelAdded = this.ConnectedChannels.TryAdd(this.Context.Guild.Id, audioClient);
            if (channelAdded == false)
            {
                await this.VoiceChannel.DisconnectAsync();
                Console.WriteLine($"Failed to add voice channel with Server ID: { this.Context.Guild.Id }");
            }
            return audioClient;
        }

        public async Task LeaveVoiceChannel()
        {
            await this.VoiceChannel.DisconnectAsync();
            var channelRemoved = this.ConnectedChannels.TryRemove(this.Context.Guild.Id, out var audioClient);
            this.MusicQueue.Remove(audioClient, out var queue);
            if (channelRemoved == false)
            {
                Console.WriteLine($"Failed to remove voice channel with Server ID: { this.Context.Guild.Id }");
            }
        }

        public async Task GetMusicList()
        {
            var musicDirectory = $"{ Directory.GetCurrentDirectory() }\\Music";
            var directoryInfo = new DirectoryInfo(musicDirectory);
            var musicList = directoryInfo.GetFiles("*.mp3");

            var embedMessage = this.CreateNewEmbedBuilder();
            var itemMessages = "";
            for (var i = 0; i < musicList.Length; i++)
            {
                var fileName = musicList[i].Name;
                fileName = fileName.Substring(0, fileName.Length - 4);
                itemMessages += ($"{ i + 1 }. { fileName }\r");
            }

            embedMessage.AddField("Playlist:", itemMessages);
            await this.Context.Channel.SendMessageAsync(embed: embedMessage.Build());
        }

        public async Task GetPlayList()
        {
            var embedMessage = this.CreateNewEmbedBuilder();
            var inVoiceChannel = this.ConnectedChannels.TryGetValue(this.Context.Guild.Id, out var audioClient);

            if (inVoiceChannel == false)
            {
                await this.Context.Channel.SendMessageAsync("<:sukoNANI:509760285477699595>");
            }

            var musicProcess = this.MusicQueue.GetValueOrDefault(audioClient);
            var itemList = musicProcess.Queue.ToList();
            var itemMessages = "";
            for (var i = 0; i < itemList.Count; i++)
            {
                itemMessages += ($"{ i + 1 }. { itemList[i] }\r");
            }

            embedMessage.AddField("Playlist:", itemMessages);
            await this.Context.Channel.SendMessageAsync(embed: embedMessage.Build());
        }

        public async Task SkipCurrentMusic()
        {
            var inVoiceChannel = this.ConnectedChannels.TryGetValue(this.Context.Guild.Id, out var audioClient);
            var musicProcess = this.MusicQueue.GetValueOrDefault(audioClient);
            var processes = this.GetProcesses();

            var processToKill = processes.Where(Q => Q.Id == musicProcess.ProcessId).FirstOrDefault();
            if (processToKill != null)
            {
                processToKill.Kill();
            }
            else
            {
                await this.Context.Channel.SendMessageAsync("???");
            }
        }

        public async Task PlayMusic(string musicName)
        {
            if (this.HasDirectory(musicName) == false)
            {
                await this.Context.Channel.SendMessageAsync("Music not found");
                return;
            }

            var alreadyInVoiceChannel = this.ConnectedChannels.TryGetValue(this.Context.Guild.Id, out var audioClient);
            if (alreadyInVoiceChannel == false)
            {
                audioClient = await this.JoinVoiceChannel();
            }

            var queueExist = this.MusicQueue.TryGetValue(audioClient, out var musicProcess);
            if (queueExist == true)
            {
                var embedMessage = this.CreateNewEmbedBuilder();
                musicProcess.Queue.Enqueue(musicName);
                this.MusicQueue.AddOrUpdate(audioClient, musicProcess, (key, value) => value = musicProcess);

                embedMessage.AddField($"Adding { musicName } to playlist", $"By: { this.Context.User }");
                await this.Context.Channel.SendMessageAsync(embed: embedMessage.Build());
            }
            else
            {
                musicProcess = new MusicProcess
                {
                    Queue = new Queue<string>()
                };
                musicProcess.Queue.Enqueue(musicName);

                await this.PlayAsync(audioClient, musicProcess);
            }
        }

        private bool HasDirectory(string musicName)
        {
            var directory = $"{ Directory.GetCurrentDirectory() }\\Music\\{ musicName }.mp3";
            return File.Exists(directory);
        }

        private async Task PlayAsync(IAudioClient audioClient, MusicProcess musicProcess)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var queue = musicProcess.Queue;
            while (queue.Count > 0)
            {
                var musicName = queue.Peek();
                using (var ffmpeg = this.CreateProcess($"{ currentDirectory }\\Music\\{ musicName }.mp3"))
                using (var stream = audioClient.CreatePCMStream(AudioApplication.Music))
                {
                    musicProcess.ProcessId = ffmpeg.Id;
                    this.MusicQueue.AddOrUpdate(audioClient, musicProcess, (key, value) => value = musicProcess);

                    var embedMessage = this.CreateNewEmbedBuilder();
                    embedMessage.AddField($"Playing { musicName }", $"By: { this.Context.User }");
                    await this.Context.Channel.SendMessageAsync(embed: embedMessage.Build());

                    await ffmpeg.StandardOutput.BaseStream.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                var queueExist = this.MusicQueue.TryGetValue(audioClient, out musicProcess);
                if (queueExist == true && musicProcess.Queue.Count > 0)
                {
                    musicProcess.Queue.Dequeue();
                    this.MusicQueue.AddOrUpdate(audioClient, musicProcess, (key, value) => value = musicProcess);

                    queue = musicProcess.Queue;
                }
            }
        }

        private Process CreateProcess(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-hide_banner -loglevel panic -i \"{ path }\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
        }

        private Process[] GetProcesses()
        {
            var processes = Process.GetProcessesByName("ffmpeg");
            return processes;
        }

        private EmbedBuilder CreateNewEmbedBuilder()
        {
            return new EmbedBuilder().WithAuthor("Voice Module:").WithColor(Color.DarkRed);
        }
    }
}
