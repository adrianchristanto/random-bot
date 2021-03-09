using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class G11DrunkModule : ModuleBase<SocketCommandContext>
    {
        [Command("G11Drunk", RunMode = RunMode.Async)]
        public async Task G11()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://media.discordapp.net/attachments/465884661537570826/745651550311022712/g11DRUNK.gif");
            await ReplyAsync("", embed: embed.Build());
        }
    }
}
