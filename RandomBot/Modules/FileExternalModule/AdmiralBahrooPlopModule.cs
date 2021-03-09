using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class AdmiralBahrooPlopModule : ModuleBase<SocketCommandContext>
    {
        [Command("Plop", RunMode = RunMode.Async)]
        public async Task Plop()
        {
            await ReplyAsync("https://twitter.com/AdmiralBahroo/status/1325950147407327233?s=20");
        }
    }
}
