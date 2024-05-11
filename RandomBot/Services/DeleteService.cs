using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class DeleteService
    {
        public async Task DeleteMessages(SocketCommandContext Context, int num)
        {
            var guildOwner = Context.Guild.OwnerId;
            if (Context.User.Id != guildOwner && Context.User.Id != 318035086375387136)
            {
                await Context.Channel.SendMessageAsync("DENIED <:dushANGERY:1082367480422482030>");
            }
            else if (num > 0 && num < 100)
            {
                var messagesToDelete = await Context.Channel.GetMessagesAsync(num + 1).FlattenAsync();
                var channel = Context.Guild.GetTextChannel(Context.Channel.Id);
                await channel.DeleteMessagesAsync(messagesToDelete);

                var messageReply = await Context.Channel.SendMessageAsync($"{ Context.User.Mention } deleted { num } message(s). This message will be deleted in a few seconds");
                await Task.Delay(3000);
                await messageReply.DeleteAsync();
            }
            else
            {
                await Context.Channel.SendMessageAsync("???");
            }
        }

        public async Task CleanUpMessages(SocketCommandContext Context, ulong messageId)
        {
            var messagesToDelete = await Context.Channel.GetMessagesAsync(messageId, Direction.After).FlattenAsync();
            var channel = Context.Guild.GetTextChannel(Context.Channel.Id);
            await channel.DeleteMessagesAsync(messagesToDelete);
        }
    }
}
