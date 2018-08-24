using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileExternalModule
{
    public class OrzModule : ModuleBase<SocketCommandContext>
    {
        [Command("orz", RunMode = RunMode.Async)]
        public async Task Orz()
        {
            var embed = new EmbedBuilder()
                .WithColor(Color.DarkRed)
                .WithImageUrl("https://orig00.deviantart.net/3636/f/2013/340/3/1/b_girl_orz_by_suzutsukikanade-d6wwtwa.jpg");
            await ReplyAsync("", embed: embed);
        }
    }
}
