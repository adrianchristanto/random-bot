using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class NorthstarModule : ModuleBase<SocketCommandContext>
    {
        [Command("northstar", RunMode = RunMode.Async)]
        [Summary("Omae wa mou shindeiru")]
        [Alias("kenshiro")]
        public async Task NorthStar(IUser user)
        {
            await Context.Channel.SendFileAsync(@"Image\youAreAlreadyDead.jpg", user.Mention + " is already dead");
        }
    }
}
