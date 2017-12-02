using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Modules
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
