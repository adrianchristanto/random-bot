using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.NicknameModule
{
    public class NicknameModule : ModuleBase<SocketCommandContext>
    {
        [Command("nick", RunMode = RunMode.Async)]
        [Summary("Change nickname")]
        [Alias("n")]
        public async Task ChangeNickName([Remainder]string newNickName)
        {
            var user = Context.Guild.GetUser(Context.User.Id);
            var oldNickname = user.Nickname ?? user.Username;
            await user.ModifyAsync(Q =>
            {
                Q.Nickname = newNickName;
            }).ConfigureAwait(false);
            await ReplyAsync(user.Mention + " changed nickname from " + oldNickname);
        }
    }
}
