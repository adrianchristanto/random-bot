using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
{
    public class SoonModule : ModuleBase<SocketCommandContext>
    {
        public SoonModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulationService = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulationService;

        [Command("soon", RunMode = RunMode.Async)]
        [Summary("SOON....")]
        public async Task Soon()
        {
            var imageStream = this.ImageManipulationService.GetImageStream(@"Image\Soon.jpg");
            await Context.Channel.SendFileAsync(imageStream, "Soon.jpg");
        }
    }
}
