using Discord;
using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
{
    public class DorimeModule : ModuleBase<SocketCommandContext>
    {
        public DorimeModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulationService = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulationService;

        [Command("dorime", RunMode = RunMode.Async)]
        public async Task Facepalm()
        {
            var imageStream = this.ImageManipulationService.GetImageStream(@"Image\SoppoDorime.jpg");
            await Context.Channel.SendFileAsync(imageStream, "SoppoDorime.jpg");
        }
    }
}
