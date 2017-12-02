using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class DeleteService
    {
        public async Task DeleteMessages(int num, SocketCommandContext Context)
        {
            if (Context.User.Id != 114003530674733060)
            {
                await Context.Channel.SendMessageAsync("DENIED <:hyperGachi:370860482451734528>");
                return;
            }

            if (num > 0 && num < 100)
            {
                var messagesToDelete = await Context.Channel.GetMessagesAsync(num + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messagesToDelete);

                var messageReply = await Context.Channel.SendMessageAsync(Context.User.Mention + " deleted " + num + " message(s). This message will be deleted in 3 seconds");
                await Task.Delay(1000);
                for (var i = 2; i > 0; i--)
                {
                    await messageReply.ModifyAsync(x => x.Content = Context.User.Mention + " deleted " + num + " message(s). This message will be deleted in " + i + " seconds");
                    await Task.Delay(1000);
                }
                await messageReply.DeleteAsync();
            } else
            {
                await Context.Channel.SendMessageAsync("???");
            }
        }
    }
}
