using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Modules.ReadingModule
{
    public class ReadingModule : ModuleBase<SocketCommandContext>
    {
        [Command("reading", RunMode = RunMode.Async)]
        public async Task ReadingIsHard()
        {
            var messagesToDelete = await Context.Channel.GetMessageAsync(Context.Message.Id);
            await messagesToDelete.DeleteAsync();
            await ReplyAsync("Reading is hard");
        }
    }
}
