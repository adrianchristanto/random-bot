using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
{
    public class GoCrazyModule : ModuleBase<SocketCommandContext>
    {
        [Command("gocrazy", RunMode = RunMode.Async)]
        [Summary("BnA gif - banging head")]
        [Alias("gc")]
        public async Task GoCrazy()
        {
            await Context.Channel.SendFileAsync(@"Image\BangingHead.gif", Context.User.Mention + " is going crazy");
        }
    }
}
