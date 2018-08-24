using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
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
            var stream = this.ImageManipulation.ManipulateImage("No.jpg", Context.User.AvatarId, 175, 133);
            await Context.Channel.SendFileAsync(stream, "No.jpg");
        }
    }
}
