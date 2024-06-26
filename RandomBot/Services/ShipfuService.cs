﻿using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using RandomBot.Core.Entities;
using RandomBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class ShipfuService : IDisposable
    {
        public ShipfuService(RandomBotDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.rand = new Random();
        }
        private readonly RandomBotDbContext DbContext;
        private readonly Random rand;

        public void Dispose()
        {
            this.DbContext.Dispose();
        }

        [Summary("SHIPFU GACHA")]
        public async Task Gacha(SocketCommandContext Context)
        {
            var rarityList = await this.GetShipfuRarity();
            var rarityId = this.GetRarityByRandom(rarityList);

            // Get shipfu list based on rarity
            var shipfuList = await this.GetShipfuByRarity(rarityId);

            // Get one shipfu by random
            var randomShipfuNumber = this.rand.Next(0, shipfuList.Count);
            var gachaResult = shipfuList[randomShipfuNumber];

            // Show gacha result
            var additionalMessage = "";
            if (rarityId == 4) additionalMessage = "Congratulations, ";
            if (rarityId == 3) additionalMessage = "Wow, ";
            if (rarityId == 2) additionalMessage = "Yay, ";
            var embed = new EmbedBuilder().WithColor(Discord.Color.DarkRed).WithImageUrl(gachaResult.ShipfuImgUrl);
            await Context.Channel.SendMessageAsync(additionalMessage + Context.User.Mention + " got " + gachaResult.ShipfuName + "!", embed: embed.Build());

            // Record gacha result to database
            this.ReportGacha(Context.User.Id.ToString(), gachaResult);
        }

        [Summary("Get gacha history")]
        public async Task GetGachaHistory(SocketCommandContext Context)
        {
            var gachaHistory = await this.DbContext.GachaHistory.AsQueryable()
                .Where(Q => Q.UserId == Context.User.Id.ToString())
                .FirstOrDefaultAsync();
            var completionRate = await this.GetCompletionRate(Context.User.Id.ToString());
            var embed = new EmbedBuilder().WithColor(Discord.Color.DarkRed);
            if (gachaHistory == null)
            {
                embed.WithDescription(Context.User.Mention + " hasn't pulled any gacha(s)");
            }
            else
            {
                var entryCount = gachaHistory.NormalCount + gachaHistory.RareCount + gachaHistory.SrCount + gachaHistory.SsrCount;
                var stringDetail = @"
{0} has pulled {1} gacha(s) of:
{2} SSR
{3} SR
{4} R
{5} N
{6}% Shipfu Completion Rate";
                embed.WithDescription(string.Format(stringDetail, Context.User.Mention, entryCount, gachaHistory.SsrCount, gachaHistory.SrCount, gachaHistory.RareCount, gachaHistory.NormalCount, completionRate));
            }
            await Context.Channel.SendMessageAsync("", embed: embed.Build());
        }

        [Summary("Get gacha completion rate")]
        public async Task<int> GetCompletionRate(string userId)
        {
            var gachaCompletion = await this.DbContext.GachaHistoryDetail.AsQueryable()
                .Where(Q => Q.UserId == userId)
                .CountAsync();
            var shipfuCount = await this.DbContext.Shipfu.AsQueryable().CountAsync();
            var completionRate = gachaCompletion * 100 / shipfuCount;
            return completionRate;
        }

        [Summary("Get rarity model")]
        private async Task<List<ShipfuRarityModel>> GetShipfuRarity()
        {
            return await this.DbContext.ShipfuRarity.AsQueryable()
                .Select(Q => new ShipfuRarityModel
                {
                    ShipfuRarityId = Q.ShipfuRarityId,
                    ShipfuRarityName = Q.ShipfuRarityName,
                    ShipfuRarityPercentage = Q.ShipfuRarityPercentage
                }).ToListAsync();
        }

        [Summary("Get the rarity based on random number")]
        private int GetRarityByRandom(List<ShipfuRarityModel> rarityList)
        {
            // Random for rarity
            var randomRarity = this.rand.Next(1, 101);

            // Get the rarity based on random number
            var flag = true;
            var rarityId = 0;
            var rarityCount = 0;
            while (flag == true && rarityId < rarityList.Count)
            {
                rarityCount += rarityList[rarityId].ShipfuRarityPercentage;
                if (rarityCount >= randomRarity)
                {
                    flag = false;
                }
                rarityId++;
            }
            return rarityId;
        }

        [Summary("Get shipfu best on rarity")]
        private async Task<List<ShipfuModel>> GetShipfuByRarity(int rarityId)
        {
            return await this.DbContext.Shipfu.AsQueryable()
                .Where(Q => Q.ShipfuRarityId == rarityId && Q.IsAvailable == true)
                .Select(Q => new ShipfuModel
                {
                    ShipfuId = Q.ShipfuId,
                    ShipfuRarity = Q.ShipfuRarityId,
                    ShipfuName = Q.ShipfuName,
                    ShipfuImgUrl = Q.ShipfuImgUrl
                })
                .ToListAsync();
        }

        [Summary("Record gacha into GachaHistory")]
        private async void ReportGacha(string userId, ShipfuModel shipfu)
        {
            // Record GachaHistory
            var gachaHistory = await this.DbContext.GachaHistory.AsQueryable()
                .Where(Q => Q.UserId == userId)
                .FirstOrDefaultAsync();
            if (gachaHistory == null)
            {
                var newGachaHistory = new GachaHistory
                {
                    UserId = userId,
                    NormalCount = shipfu.ShipfuRarity == 1 ? 1 : 0,
                    RareCount = shipfu.ShipfuRarity == 2 ? 1 : 0,
                    SrCount = shipfu.ShipfuRarity == 3 ? 1 : 0,
                    SsrCount = shipfu.ShipfuRarity == 4 ? 1 : 0
                };
                this.DbContext.GachaHistory.Add(newGachaHistory);
            }
            else
            {
                gachaHistory.NormalCount = shipfu.ShipfuRarity == 1 ? gachaHistory.NormalCount + 1 : gachaHistory.NormalCount;
                gachaHistory.RareCount = shipfu.ShipfuRarity == 2 ? gachaHistory.RareCount + 1 : gachaHistory.RareCount;
                gachaHistory.SrCount = shipfu.ShipfuRarity == 3 ? gachaHistory.SrCount + 1 : gachaHistory.SrCount;
                gachaHistory.SsrCount = shipfu.ShipfuRarity == 4 ? gachaHistory.SsrCount + 1 : gachaHistory.SsrCount;
                this.DbContext.GachaHistory.Update(gachaHistory);
            }

            // Record GachaHistoryDetail
            var gachaHistoryDetail = await this.DbContext.GachaHistoryDetail.AsQueryable()
                .Where(Q => Q.UserId == userId && Q.ShipfuId == shipfu.ShipfuId)
                .FirstOrDefaultAsync();
            if (gachaHistoryDetail == null)
            {
                var newGachaHistoryDetail = new GachaHistoryDetail
                {
                    UserId = userId,
                    ShipfuId = shipfu.ShipfuId,
                    GetCount = 1
                };
                this.DbContext.GachaHistoryDetail.Add(newGachaHistoryDetail);
            }
            else
            {
                gachaHistoryDetail.GetCount++;
                this.DbContext.GachaHistoryDetail.Update(gachaHistoryDetail);
            }
            await this.DbContext.SaveChangesAsync();
        }
    }
}
