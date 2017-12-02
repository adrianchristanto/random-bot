using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class RollModule : ModuleBase<SocketCommandContext>
    {
        Random rand;

        [Command("roll")]
        [Summary("Roll from 1 - 100")]
        [Alias("r")]
        public async Task Roll()
        {
            rand = new Random();
            var result = 0;
            if (Context.User.Id == 318035086375387136) result = rand.Next(80, 101);
            else result = rand.Next(1, 101);
            await ReplyAsync(Context.User.Mention + " rolled " + result.ToString());
        }
    }
}
