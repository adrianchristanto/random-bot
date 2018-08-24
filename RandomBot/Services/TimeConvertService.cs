using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class TimeConvertService
    {
        public async Task TimeConvert(SocketCommandContext Context, int timeInput)
        {
            var localDate = DateTime.Now.AddHours(timeInput);
            var utcTime = localDate.ToUniversalTime();
            var pdtTime = utcTime.AddHours(-7);

            var replyString = $@"
**Server/Bot Time (UTC +7):** { localDate.ToString("dd MMMM yyyy, HH:mm tt") }
**UTC:** { utcTime.ToString("dd MMMM yyyy, HH:mm tt") } ({ -(localDate - utcTime).TotalHours } hours difference)
**ET:** { localDate.AddHours(-11).ToString("dd MMMM yyyy, HH:mm tt") } ({ -(localDate - localDate.AddHours(-11)).TotalHours } hours difference)
**PDT:** { pdtTime.ToString("dd MMMM yyyy, HH:mm tt") } ({ -(localDate - pdtTime).TotalHours } hours difference)";

            await Context.Channel.SendMessageAsync(replyString);
        }
    }
}
