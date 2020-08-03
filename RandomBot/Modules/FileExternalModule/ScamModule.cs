using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class ScamModule : ModuleBase<SocketCommandContext>
    {
        [Command("scam", RunMode = RunMode.Async)]
        public async Task Scam()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://cdn.discordapp.com/attachments/465884661537570826/712358720012812299/kryuger-trade-deal.png");
            await ReplyAsync("", embed: embed.Build());
        }
    }
}
