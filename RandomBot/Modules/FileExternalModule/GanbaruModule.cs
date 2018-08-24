using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class GanbaruModule : ModuleBase<SocketCommandContext>
    {
        [Command("ganbaru", RunMode = RunMode.Async)]
        [Summary("Ganbaru Unicorn")]
        public async Task Ganbaru()
        {
            var embed = new EmbedBuilder()
                .WithColor(Discord.Color.DarkRed)
                .WithImageUrl("https://i2.wp.com/azurlane.info/wp-content/uploads/2017/11/Eu6JqWN.jpg");
            await ReplyAsync("", embed: embed.Build());
        }
    }
}
