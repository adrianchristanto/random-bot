using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class KokoroModule : ModuleBase<SocketCommandContext>
    {
        [Command("kokoro", RunMode = RunMode.Async)]
        public async Task Ganbaru()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://cdn.discordapp.com/attachments/465884661537570826/592575856677879809/D9vMCwwW4AAYpKg.png");
            await ReplyAsync(embed: embed.Build());
        }
    }
}
