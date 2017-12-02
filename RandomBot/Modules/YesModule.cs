using Discord.Commands;
using RandomBot.Services;
using System.IO;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class YesModule : ModuleBase<SocketCommandContext>
    {
        public YesModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulation = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulation;

        [Command("yes", RunMode = RunMode.Async)]
        [Summary("Say yes with image")]
        public async Task YesImage()
        {
            await this.ImageManipulation.GetAvatarFromUrl(Context.User);
            this.ImageManipulation.ManipulateImage("Yes.jpg", Context.User.AvatarId, 10, 120, "Yes.jpg");
            await Context.Channel.SendFileAsync(@"Image\ToUpload\Yes.jpg");
            File.Delete(@"Image\ToUpload\Yes.jpg");
        }
    }
}
