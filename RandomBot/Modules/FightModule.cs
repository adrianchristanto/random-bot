using Discord;
using Discord.Commands;
using RandomBot.Services;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    [Group("fight")]
    [Alias("f")]
    public class FightModule : ModuleBase<SocketCommandContext>
    {
        public FightModule(FightService fightService)
        {
            this.FightService = fightService;
        }
        private readonly FightService FightService;

        [Command(RunMode = RunMode.Async)]
        [Summary("Fight someone by mentioning him/her")]
        public async Task Fight(IUser user)
        {
            await this.FightService.Fight(Context, Context.User, user);
        }
        [Command(RunMode = RunMode.Async)]
        [Summary("Fight someone by mentioning him/her")]
        public async Task Fight(IUser user1, IUser user2)
        {
            await this.FightService.Fight(Context, user1, user2);
        }
    }
}
