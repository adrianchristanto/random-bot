using Discord.Commands;
using RandomBot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Modules
{
    public class ShipfuModule : ModuleBase<SocketCommandContext>
    {
        public ShipfuModule(ShipfuService shipfuService)
        {
            this.ShipfuService = shipfuService;
        }
        private readonly ShipfuService ShipfuService;

        [Command("shipfugacha", RunMode = RunMode.Async)]
        [Summary("Gacha shipfu yey")]
        [Alias("sg")]
        public async Task Gacha()
        {
            await this.ShipfuService.Gacha(Context);
        }
    }
}
