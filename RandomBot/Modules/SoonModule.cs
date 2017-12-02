using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class SoonModule : ModuleBase<SocketCommandContext>
    {
        [Command("soon", RunMode = RunMode.Async)]
        [Summary("SOON....")]
        public async Task Soon()
        {
            await Context.Channel.SendFileAsync(@"Image\Soon.jpg");
        }
    }
}
