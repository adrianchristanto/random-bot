using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Modules.BotNicknameModule
{
    public class BotNicknameModule : ModuleBase<SocketCommandContext>
    {
        [Command("bn", RunMode = RunMode.Async)]
        public async Task BotNickname(string nickname)
        {
            if (Context.User.Id != 318035086375387136)
            {
                await Context.Channel.SendMessageAsync("DENIED <:kokojanai:684341545813409810>");
            }

            var guild = Context.Guild;
            var bot = guild.GetUser(371933819705753600);
            await bot.ModifyAsync(Q => { Q.Nickname = nickname; });
        }
    }
}
