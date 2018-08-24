using Discord.Commands;
using RandomBot.Services;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
{
    public class DabModule : ModuleBase<SocketCommandContext>
    {
        public DabModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulation = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulation;

        [Command("dab", RunMode = RunMode.Async)]
        [Summary("Dab image")]
        public async Task Dab()
        {
            await this.ImageManipulation.GetAvatarFromUrl(Context.User);
            var stream = this.ImageManipulation.ManipulateImage("Dab.jpg", Context.User.AvatarId, 163, 145);
            await Context.Channel.SendFileAsync(stream, "Dab.jpg");
        }
    }
}
