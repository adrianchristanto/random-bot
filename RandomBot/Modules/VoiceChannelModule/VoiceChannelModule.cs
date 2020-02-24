using Discord;
using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.VoiceChannelModule
{
    public class VoiceChannelModule : ModuleBase<SocketCommandContext>
    {
        public VoiceChannelModule(VoiceChannelService voiceChannelService)
        {
            this.VoiceChannelService = voiceChannelService;
        }
        private readonly VoiceChannelService VoiceChannelService;

        [Command("Join", RunMode = RunMode.Async)]
        public async Task Join()
        {
            this.VoiceChannelService.Context = Context;
            this.VoiceChannelService.VoiceChannel = this.VoiceChannelService.GetVoiceChannel();
            await this.VoiceChannelService.JoinVoiceChannel();
        }

        [Command("Leave", RunMode = RunMode.Async)]
        public async Task Leave()
        {
            this.VoiceChannelService.Context = Context;
            this.VoiceChannelService.VoiceChannel = this.VoiceChannelService.GetVoiceChannel();
            await this.VoiceChannelService.LeaveVoiceChannel();
        }

        [Command("MusicList", RunMode = RunMode.Async)]
        public async Task MusicList()
        {
            this.VoiceChannelService.Context = Context;
            await this.VoiceChannelService.GetMusicList();
        }

        [Command("PLayList", RunMode = RunMode.Async)]
        public async Task PlayList()
        {
            this.VoiceChannelService.Context = Context;
            await this.VoiceChannelService.GetPlayList();
        }
        
        [Command("Play", RunMode = RunMode.Async)]
        public async Task PlayMusic(string musicName)
        {
            this.VoiceChannelService.Context = Context;
            this.VoiceChannelService.VoiceChannel = this.VoiceChannelService.GetVoiceChannel();
            await this.VoiceChannelService.PlayMusic(musicName);
        }

        [Command("Skip", RunMode = RunMode.Async)]
        public async Task Skip()
        {
            this.VoiceChannelService.Context = Context;
            this.VoiceChannelService.VoiceChannel = this.VoiceChannelService.GetVoiceChannel();
            await this.VoiceChannelService.SkipCurrentMusic();
        }
    }
}
