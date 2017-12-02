using Discord;
using Discord.Commands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class FightService
    {
        
        public FightService(ImageManipulationService imageManipulation)
        {
            this.ImageManipulation = imageManipulation;
            rand = new Random();
        }
        private readonly ImageManipulationService ImageManipulation;
        Random rand;

        public async Task Fight(SocketCommandContext Context, IUser user1, IUser user2)
        {
            if (user1.Id == user2.Id)
            {
                await Context.Channel.SendMessageAsync(user1.Mention + " need some help");
            }
            else
            {
                await ImageManipulation.GetAvatarFromUrl(user1);
                this.ImageManipulation.ManipulateImage("Fight.jpg", user1.AvatarId, 327, 237, "tempFight1.jpg");
                await this.ImageManipulation.GetAvatarFromUrl(user2);
                this.ImageManipulation.ManipulateImage(@"ToUpload\tempFight1.jpg", user2.AvatarId, 622, 213, "Fight.jpg");
                await Context.Channel.SendFileAsync(@"Image\ToUpload\Fight.jpg");
                File.Delete(@"Image\ToUpload\tempFight1.jpg");
                File.Delete(@"Image\ToUpload\Fight.jpg");
                
                var result = rand.Next(1, 101);
                if (result >= 1 && result <= 40) await Context.Channel.SendMessageAsync(user1.Mention + " win!");
                if (result >= 41 && result <= 60) await Context.Channel.SendMessageAsync("It's a draw!");
                if (result >= 61 && result <= 100) await Context.Channel.SendMessageAsync(user2.Mention + " win!");
            }
        }
    }
}
