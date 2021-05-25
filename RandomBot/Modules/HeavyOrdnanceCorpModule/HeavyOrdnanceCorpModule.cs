using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.HeavyOrdnanceCorpModule
{
    public class HeavyOrdnanceCorpModule : ModuleBase<SocketCommandContext>
    {
        public HeavyOrdnanceCorpModule(HeavyOrdnanceCorpService hocService)
        {
            this.HocService = hocService;
        }
        private readonly HeavyOrdnanceCorpService HocService;

        [Command("hoc", RunMode = RunMode.Async)]
        public async Task HeavyOrdnanceCorp([Remainder]string hocName)
        {
            var hocInfo = await this.HocService.GetHocInfo(hocName);
            await ReplyAsync(embed: hocInfo.Build());
        }
    }
}
