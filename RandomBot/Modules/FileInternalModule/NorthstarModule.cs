using Discord;
using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
{
    public class NorthstarModule : ModuleBase<SocketCommandContext>
    {
        public NorthstarModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulationService = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulationService;

        [Command("northstar", RunMode = RunMode.Async)]
        [Summary("Omae wa mou shindeiru")]
        [Alias("kenshiro")]
        public async Task NorthStar(IUser user)
        {
            var imageStream = this.ImageManipulationService.GetImageStream(@"Image\youAreAlreadyDead.jpg");
            await Context.Channel.SendFileAsync(imageStream, "YouAreAlreadyDead.jpg", user.Mention + " is already dead");
        }
    }
}
