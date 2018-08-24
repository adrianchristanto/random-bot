using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace RandomBot.Modules.DecisionModule
{
    public class DecisionModule : ModuleBase<SocketCommandContext>
    {
        public DecisionModule()
        {
            rand = new Random();
        }
        Random rand;

        [Command("decide", RunMode = RunMode.Async)]
        [Summary("Decide pls")]
        public async Task Decision(params string[] choices)
        {
            var choiceCount = choices.Length;
            var resultIndex = rand.Next(0, choiceCount);
            await ReplyAsync(choices[resultIndex]);
        }
    }
}
