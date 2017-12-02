using Discord.Commands;
using RandomBot.Services;
using System.IO;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class NoModule : ModuleBase<SocketCommandContext>
    {
        public NoModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulation = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulation;

        [Command("no", RunMode = RunMode.Async)]
        [Summary("Say no with image")]
        public async Task NoImage()
        {
            await this.ImageManipulation.GetAvatarFromUrl(Context.User);
            this.ImageManipulation.ManipulateImage("No.jpg", Context.User.AvatarId, 175, 133, "No.jpg");
            await Context.Channel.SendFileAsync(@"Image\ToUpload\No.jpg");
            File.Delete(@"Image\ToUpload\No.jpg");
        }
    }
}
