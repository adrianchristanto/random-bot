using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using RandomBot.Entities;
using RandomBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class ShipfuService
    {
        public ShipfuService(RandomBotDbContext dbContext)
        {
            this.DbContext = dbContext;
            rand = new Random();
        }
        private readonly RandomBotDbContext DbContext;
        Random rand;

        [Summary("SHIPFU GACHA")]
        public async Task Gacha(SocketCommandContext Context)
        {
            // Get rarity list
            var rarityList = await this.GetShipfuRarity();

            // Random for rarity
            var randomRarity = rand.Next(1, 101);

            // Get the rarity based on random number
            var flag = true;
            var count = 0;
            var rarityCount = 0;
            var rarityToFind = 0;
            while(flag == true && count < rarityList.Count)
            {
                rarityCount += rarityList[count].ShipfuRarityPercentage;
                if (rarityCount >= randomRarity)
                {
                    flag = false;
                }
                count++;
            }
            rarityToFind = count;

            // Get shipfu list based on rarity
            var shipfuList = await this.DbContext.Shipfu
                .Where(Q => Q.ShipfuRarityId == rarityToFind)
                .Select(Q => new ShipfuModel
                {
                    ShipfuName = Q.ShipfuName,
                    ShipfuImgUrl = Q.ShipfuImgUrl
                })
                .ToListAsync();

            // Get one shipfu by random
            var randomShipfuNumber = rand.Next(0, shipfuList.Count);
            var gachaResult = shipfuList[randomShipfuNumber];

            // Show gacha result
            var additionalMessage = "";
            if (rarityToFind == 4) additionalMessage = "Congratulations, ";
            if (rarityToFind == 3) additionalMessage = "Wow, ";
            if (rarityToFind == 2) additionalMessage = "Yay, ";
            var builder = new EmbedBuilder().WithColor(Discord.Color.DarkRed).WithImageUrl(gachaResult.ShipfuImgUrl);
            await Context.Channel.SendMessageAsync(additionalMessage + Context.User.Mention + " got " + gachaResult.ShipfuName + "!", false, builder);
        }

        [Summary("Show rarity list and percentage")]
        public async Task RarityList(SocketCommandContext Context)
        {
            var rarityList = await this.GetShipfuRarity();

            var builder = new EmbedBuilder().WithAuthor("Shipfu Rarity:").WithColor(Discord.Color.DarkRed);
            foreach(var item in rarityList)
            {
                builder.AddField(item.ShipfuRarityName, item.ShipfuRarityPercentage + "%");
            }
            await Context.Channel.SendMessageAsync("", false, builder);
        }

        [Summary("Get rarity model")]
        public async Task<List<ShipfuRarityModel>> GetShipfuRarity()
        {
            return await this.DbContext.ShipfuRarity.Select(Q => new ShipfuRarityModel
            {
                ShipfuRarityId = Q.ShipfuRarityId,
                ShipfuRarityName = Q.ShipfuRarityName,
                ShipfuRarityPercentage = Q.ShipfuRarityPercentage
            }).ToListAsync();
        }
    }
}
