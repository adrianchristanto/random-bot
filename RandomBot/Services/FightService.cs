using Discord;
using Discord.Commands;
using System;
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
                await Context.Channel.SendMessageAsync($"{ user1.Mention } need some help");
            }
            else
            {
                await ImageManipulation.GetAvatarFromUrl(user1);
                var halfFinishedStream = this.ImageManipulation.ManipulateImage("Fight.jpg", user1.AvatarId, 327, 237);

                await this.ImageManipulation.GetAvatarFromUrl(user2);
                var finishedStream = this.ImageManipulation.ManipulateImage(halfFinishedStream, user2.AvatarId, 622, 213);
                
                var result = rand.Next(1, 101);
                var message = string.Empty;
                if (result >= 1 && result <= 40) message = $"{ user1.Mention } win!";
                if (result >= 41 && result <= 60) message = "It's a draw!";
                if (result >= 61 && result <= 100) message = $"{ user2.Mention } win!";

                await Context.Channel.SendFileAsync(finishedStream, "Fight.jpg", message);
            }
        }
    }
}
