using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class DamedaneModule : ModuleBase<SocketCommandContext>
    {
        [Command("damedane", RunMode = RunMode.Async)]
        public async Task Damedane()
        {
            await ReplyAsync("https://cdn.discordapp.com/attachments/574253420119326759/739561513437888552/yegor.mp4");
        }
    }
}
