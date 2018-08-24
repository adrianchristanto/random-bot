using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.AwareModule
{
    public class AwareModule : ModuleBase<SocketCommandContext>
    {
        [Command("aware", RunMode = RunMode.Async)]
        public async Task Aware()
        {
            await Context.Message.DeleteAsync();
            await ReplyAsync("***However, we are aware that this feature is not working 100% of the time.***");
        }
    }
}
