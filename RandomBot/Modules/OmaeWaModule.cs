using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class OmaeWaModule : ModuleBase<SocketCommandContext>
    {
        [Command("uralreadydead", RunMode = RunMode.Async)]
        [Summary("Omae wa mou shindeiru")]
        public async Task UrAlreadyDead()
        {
            await ReplyAsync("NANI!? <:remiSweat:370978504004599818>");
        }
    }
}
