using Discord;
using Discord.Commands;
using RandomBot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Modules.FileInternalModule
{
    public class OusenModule : ModuleBase<SocketCommandContext>
    {
        public OusenModule(ImageManipulationService imageManipulation)
        {
            this.ImageManipulation = imageManipulation;
        }
        private readonly ImageManipulationService ImageManipulation;

        [Command("Ousen", RunMode = RunMode.Async)]
        public async Task OusenMock(IUser user)
        {
            var socketGuildUser = this.Context.Guild.GetUser(user.Id);
            var stream = this.ImageManipulation.WriteTextOnImage("Ousen", socketGuildUser, 236, 234);
            await Context.Channel.SendFileAsync(stream, "Ousen.jpg");
        }
    }
}
