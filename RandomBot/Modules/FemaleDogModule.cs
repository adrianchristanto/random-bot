using Discord;
using Discord.Commands;
using RandomBot.Services;
using System.IO;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    [Group("bitch")]
    [Alias("b")]
    public class FemaleDogModule : ModuleBase
    {
        public FemaleDogModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulation = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulation;

        [Command(RunMode = RunMode.Async)]
        [Summary("Darkest Dungeon Meme - solo")]
        public async Task InsultTime()
        {
            await this.ImageManipulation.GetAvatarFromUrl(Context.User);
            this.ImageManipulation.ManipulateImage("DD.jpg", Context.User.AvatarId, 303, 140, "DD.jpg");
            await Context.Channel.SendFileAsync(@"Image\ToUpload\DD.jpg");
            File.Delete(@"Image\ToUpload\DD.png");
        }

        [Command(RunMode = RunMode.Async)]
        [Summary("Darkest Dungeon Meme")]
        public async Task InsultTime(IUser mentionedUser)
        {
            await this.ImageManipulation.GetAvatarFromUrl(mentionedUser);
            this.ImageManipulation.ManipulateImage("DD.jpg", mentionedUser.AvatarId, 303, 140, "DD.jpg");
            await Context.Channel.SendFileAsync(@"Image\ToUpload\DD.jpg");
            File.Delete(@"Image\ToUpload\DD.png");
        }
    }
}
