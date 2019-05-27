using Discord;
using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.GunfuModule
{
    public class GunfuModule : ModuleBase<SocketCommandContext>
    {
        public GunfuModule(GunfuService gunfuService)
        {
            this.GunfuService = gunfuService;
        }
        private readonly GunfuService GunfuService;

        [Command("doll", RunMode = RunMode.Async)]
        public async Task Doll([Remainder]string dollName)
        {
            var dollInfo = await this.GunfuService.GetDollInfo(dollName);
            await ReplyAsync(embed: dollInfo.Build());
        }
    }
}
