using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace RandomBot.Modules.DecisionModule
{
    public class DecisionModule : ModuleBase<SocketCommandContext>
    {
        [Command("decide", RunMode = RunMode.Async)]
        [Summary("Decide pls")]
        public async Task Decision(params string[] choices)
        {
            var rand = new Random();

            var choiceCount = choices.Length;
            var resultIndex = rand.Next(0, choiceCount);
            await ReplyAsync(choices[resultIndex]);
        }
    }
}
