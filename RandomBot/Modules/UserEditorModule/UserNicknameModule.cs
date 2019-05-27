using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.NicknameModule
{
    public class UserNicknameModule : ModuleBase<SocketCommandContext>
    {
        [Command("nick", RunMode = RunMode.Async)]
        [Summary("Change nickname")]
        [Alias("n")]
        public async Task ChangeUserNickName([Remainder]string newNickName)
        {
            await Context.Message.DeleteAsync();
            var user = Context.Guild.GetUser(Context.User.Id);
            await user.ModifyAsync(Q =>
            {
                Q.Nickname = newNickName;
            });
        }
    }
}
