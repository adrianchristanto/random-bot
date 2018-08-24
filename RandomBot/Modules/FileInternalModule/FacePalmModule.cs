using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
{
    public class FacepalmModule : ModuleBase<SocketCommandContext>
    {
        public FacepalmModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulationService = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulationService;

        [Command("facepalm", RunMode = RunMode.Async)]
        [Summary("Facepalm image")]
        public async Task Facepalm()
        {
            var imageStream = this.ImageManipulationService.GetImageStream(@"Image\Facepalm.jpg");
            await Context.Channel.SendFileAsync(imageStream, "Facepalm.jpg");
        }
    }
}
