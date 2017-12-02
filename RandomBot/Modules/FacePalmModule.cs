using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class FacepalmModule : ModuleBase<SocketCommandContext>
    {
        [Command("facepalm", RunMode = RunMode.Async)]
        [Summary("Facepalm image")]
        public async Task Facepalm()
        {
            await Context.Channel.SendFileAsync(@"Image\Facepalm.jpg");
        }
    }
}
