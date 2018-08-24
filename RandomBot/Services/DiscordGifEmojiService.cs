using Discord.Commands;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class DiscordGifEmojiService
    {
        public async Task SendGifEmoji(SocketCommandContext Context, int count, string gifEmoji)
        {
            await Context.Message.DeleteAsync();
            var strBuilder = string.Empty;
            if (count > 10)
            {
                count = 10;
            }
            for (var i = 0; i < count; i++)
            {
                strBuilder += $"<a:{ gifEmoji }> ";
            }
            await Context.Channel.SendMessageAsync(strBuilder);
        }
    }
}
