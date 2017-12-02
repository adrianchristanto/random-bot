using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        public HelpModule(HelpService helpService)
        {
            this.HelpService = helpService;
        }
        private readonly HelpService HelpService;

        [Command("help", RunMode = RunMode.Async)]
        [Summary("Shows command list")]
        [Alias("h")]
        public async Task Help()
        {
            await this.HelpService.HelpMessage(Context);
        }
    }
}
