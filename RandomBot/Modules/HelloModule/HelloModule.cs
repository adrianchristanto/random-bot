using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Modules.HelloModule
{
    [Group("hello")]
    [Alias("hi")]
    public class HelloModule : ModuleBase<SocketCommandContext>
    {
        [Command(RunMode = RunMode.Async)]
        [Summary("Says basic hello")]
        public async Task Hello()
        {
            await ReplyAsync($"Hello { Context.Message.Author.Mention } <:kyouka:1187613200993239040>");
        }
        [Command(RunMode = RunMode.Async)]
        [Summary("Says basic hello to another user")]
        public async Task Hello(IUser user)
        {
            var messagesToDelete = await Context.Channel.GetMessageAsync(Context.Message.Id);
            if (Context.User.Id == user.Id)
            {
                await this.Hello();
            }
            else
            {
                await ReplyAsync($"{ Context.User.Mention } says hello to you, { user.Mention } <:kyouka:1187613200993239040>");
            }
            await messagesToDelete.DeleteAsync();
        }
    }
}
