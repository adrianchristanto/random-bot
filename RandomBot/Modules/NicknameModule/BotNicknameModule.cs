using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.NicknameModule
{
    public class BotNicknameModule : ModuleBase<SocketCommandContext>
    {
        [Command("botn", RunMode = RunMode.Async)]
        public async Task ChangeBotNickName([Remainder]string newNickName)
        {
            await Context.Message.DeleteAsync();

            if (Context.User.Id == 318035086375387136)
            {
                var user = Context.Guild.GetUser(371933819705753600);
                await user.ModifyAsync(Q =>
                {
                    Q.Nickname = newNickName;
                });
            }
            else
            {
                await Context.Channel.SendMessageAsync("D:");
            }
        }
    }
}
