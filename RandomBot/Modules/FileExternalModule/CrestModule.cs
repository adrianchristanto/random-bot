using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class CrestModule : ModuleBase<SocketCommandContext>
    {
        [Command("crest", RunMode = RunMode.Async)]
        public async Task Crest()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://cdn.discordapp.com/attachments/371978863452094464/634285092688363551/d01.jpg");
            await ReplyAsync("", embed: embed.Build());
        }
    }
}
