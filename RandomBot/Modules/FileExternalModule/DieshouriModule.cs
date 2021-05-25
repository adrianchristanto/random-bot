using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class DieshouriModule : ModuleBase<SocketCommandContext>
    {
        [Command("dieshouri", RunMode = RunMode.Async)]
        [Summary("DIESHOURI")]
        public async Task Dieshouri()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://cdn.discordapp.com/attachments/371978863452094464/419495571858915329/dieshouri.jpg");
            await ReplyAsync("", embed: embed.Build());
        }
    }
}
