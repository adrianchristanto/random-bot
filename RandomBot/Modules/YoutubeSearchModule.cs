using Discord.Addons.Interactive;
using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class YoutubeSearchModule : ModuleBase<SocketCommandContext>
    {
        public YoutubeSearchModule(YoutubeSearchService youtubeSearchService)
        {
            this.YoutubeSearchService = youtubeSearchService;
            //this.Interactive = interactive;
        }
        private readonly YoutubeSearchService YoutubeSearchService;
        //private readonly InteractiveService Interactive;

        [Command("search", RunMode = RunMode.Async)]
        [Summary("Search video from youtube")]
        [Alias("s")]
        public async Task YoutubeSearch([Remainder]string search)
        {
            await this.YoutubeSearchService.YoutubeSearch(Context, search);
        }
    }
}
