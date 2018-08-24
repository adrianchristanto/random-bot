using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
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
            var stream = this.ImageManipulation.ManipulateImage("Yes.jpg", Context.User.AvatarId, 15, 110);
            await Context.Channel.SendFileAsync(stream, "Yes.jpg");
        }
    }
}
