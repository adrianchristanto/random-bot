using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class FreedomModule : ModuleBase<SocketCommandContext>
    {
        [Command("freedom", RunMode = RunMode.Async)]
        public async Task Freedom()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://cdn.discordapp.com/attachments/465884661537570826/623737210222673920/freedom.jpg");
            await ReplyAsync("", embed: embed.Build());
        }
    }
}
