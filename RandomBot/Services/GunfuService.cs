using Discord;
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
    public class GunfuService : IDisposable
    {
        public GunfuService (RandomBotDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        private readonly RandomBotDbContext DbContext;

        public void Dispose()
        {
            this.DbContext.Dispose();
        }

        public async Task<EmbedBuilder> GetDollInfo(string dollName)
        {
            var dolls = await this.GetDollModel(dollName);

            var embed = new EmbedBuilder()
                .WithAuthor("T-Doll Info:")
                .WithColor(Color.DarkRed);

            if (dolls.Count == 0)
            {
                embed.AddField("Error", $"Doll with name { dollName } not found");
                return embed;
            }
            
            var doll = dolls.FirstOrDefault(Q => string.Equals(Q.DollName, dollName, StringComparison.InvariantCultureIgnoreCase));

            if (dolls.Count != 1 && doll == null)
            {
                var dollList = new StringBuilder();
                for (var i = 0; i < dolls.Count; i++)
                {
                    dollList.AppendLine($"{ i + 1 }. { dolls[i].DollName }");
                }

                embed.AddField($"Multiple Doll with keyword '{ dollName }' Found", dollList.ToString());

                return embed;
            }

            doll = doll ?? dolls.FirstOrDefault();

            return this.GenerateDollData(embed, doll);
        }

        private async Task<List<DollModel>> GetDollModel(string dollName)
        {
            var doll = await
                (from d in this.DbContext.Doll
                 join dt in this.DbContext.DollType on d.DollTypeId equals dt.DollTypeId
                 where d.DollName.Contains(dollName)
                 select new DollModel
                 {
                     DollName = d.DollName,
                     DollTypeName = dt.DollTypeName,
                     HP = d.HP,
                     Damage = d.Damage,
                     Accuracy = d.Accuracy,
                     Evasion = d.Evasion,
                     ROF = d.ROF,
                     Armor = d.Armor,
                     ClipSize = d.ClipSize,
                     Skill1 = d.Skill1,
                     Skill2 = d.Skill2,
                     TileDollLocation = d.TileDollLocation,
                     TileBonusLocation = d.TileBonusLocation,
                     TileModBonusLocation = d.TileModBonusLocation,
                     TileEffect = d.TileEffect
                 })
                 .ToListAsync();

            return doll;
        }

        private EmbedBuilder GenerateDollData(EmbedBuilder embed, DollModel doll)
        {
            embed
                .AddField("Name", doll.DollName)
                .AddField("Type", doll.DollTypeName)
                .AddField("HP", doll.HP, true)
                .AddField("Damage", doll.Damage, true)
                .AddField("Accuracy", doll.Accuracy, true)
                .AddField("Evasion", doll.Evasion, true)
                .AddField("ROF", doll.ROF, true)
                .AddField("Armor", doll.Armor, true)
                .AddField("Clip Size", doll.ClipSize, true);

            if (string.IsNullOrEmpty(doll.Skill2) == false)
            {
                embed.AddField("Skill 1", doll.Skill1);
                embed.AddField("Skill 2", doll.Skill2);
            }
            else
            {
                embed.AddField("Skill", doll.Skill1);
            }

            var tileCells = new char[9];
            for (var i = 0; i < tileCells.Length; i++)
            {
                tileCells[i] = '☒';
            }

            tileCells[doll.TileDollLocation - 1] = '☐';
            var bonusTileLocations = doll.TileBonusLocation.Split(',');
            for (var i = 0; i < bonusTileLocations.Length; i++)
            {
                var tileNumber = int.Parse(bonusTileLocations[i].Trim());
                tileCells[tileNumber - 1] = '▨';
            }

            if (string.IsNullOrEmpty(doll.TileModBonusLocation) == false)
            {
                var bonusModTileLocations = doll.TileModBonusLocation.Split(',');
                for (var i = 0; i < bonusModTileLocations.Length; i++)
                {
                    var tileNumber = int.Parse(bonusModTileLocations[i].Trim());
                    tileCells[tileNumber - 1] = '◆';
                }
            }

            embed.AddField("Tile Bonus", $@"
{ tileCells[0] }    { tileCells[1] }    { tileCells[2] }
{ tileCells[3] }    { tileCells[4] }    { tileCells[5] }
{ tileCells[6] }    { tileCells[7] }    { tileCells[8] }
{ doll.TileEffect }");

            return embed;
        }

        public string RofCalculation(int rofValue)
        {
            if (rofValue <= 0)
            {
                return "0 or less is basically not shooting <:gigaPepega:547315752949121024> ";
            }
            
            var frameCount = 30 * ((double)50 / rofValue);

            if (frameCount < 12)
            {
                frameCount = 12;
            }
            else if (frameCount % 1 == 0)
            {
                frameCount = frameCount - 1;
            }
            else
            {
                frameCount = Math.Floor(frameCount);
            }

            return $@"ROF Value: { rofValue }
Frames per shot: { (int)frameCount }";
        }
    }
}
