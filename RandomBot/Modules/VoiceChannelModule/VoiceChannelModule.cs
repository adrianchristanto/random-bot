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

        [Command("PLayList", RunMode = RunMode.Async)]
        public async Task PlayList()
        {
            this.VoiceChannelService.Context = Context;
            await this.VoiceChannelService.GetPlayList();
        }

        [Command("Skip", RunMode = RunMode.Async)]
        public async Task Skip()
        {
            this.VoiceChannelService.Context = Context;
            this.VoiceChannelService.VoiceChannel = this.VoiceChannelService.GetVoiceChannel();
            await this.VoiceChannelService.SkipCurrentMusic();
        }

        [Command("CoffeeTime", RunMode = RunMode.Async)]
        public async Task CoffeeTime()
        {
            var musicName = new MusicName();
            this.VoiceChannelService.Context = Context;
            this.VoiceChannelService.VoiceChannel = this.VoiceChannelService.GetVoiceChannel();
            await this.VoiceChannelService.PlayMusic(musicName.CoffeTime);
        }

        [Command("Rain", RunMode = RunMode.Async)]
        public async Task Rain()
        {
            var musicName = new MusicName();
            this.VoiceChannelService.Context = Context;
            this.VoiceChannelService.VoiceChannel = this.VoiceChannelService.GetVoiceChannel();
            await this.VoiceChannelService.PlayMusic(musicName.Rain);
        }
    }
}
