using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.TimeConvertModule
{
    public class TimeConvertModule : ModuleBase<SocketCommandContext>
    {
        public TimeConvertModule(TimeConvertService timeConvertService)
        {
            this.TimeConvertService = timeConvertService;
        }
        private readonly TimeConvertService TimeConvertService;

        [Command("timeconvert", RunMode = RunMode.Async)]
        [Summary("Convert local time to specific time zone")]
        [Alias("tc")]
        public async Task TimeConvert(int timeInput = 0)
        {
            await this.TimeConvertService.TimeConvert(Context, timeInput);
        }
    }
}
