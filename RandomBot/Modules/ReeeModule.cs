using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class ReeeModule : ModuleBase<SocketCommandContext>
    {
        [Command("REE")]
        [Summary("REEEEEEEEEEEEEEEEEEEE")]
        public async Task Reee()
        {
            await Context.Channel.SendFileAsync(@"Image\rooREEgif.gif");
        }
    }
}
