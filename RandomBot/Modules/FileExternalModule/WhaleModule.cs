using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class WhaleModule : ModuleBase<SocketCommandContext>
    {
        [Command("whale", RunMode = RunMode.Async)]
        public async Task Whale()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://media.discordapp.net/attachments/465884661537570826/689061432217567255/whale.jpg");
            await ReplyAsync("", embed: embed.Build());
        }
    }
}
