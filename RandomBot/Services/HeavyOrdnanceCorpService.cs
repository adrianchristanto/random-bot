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
    public class HeavyOrdnanceCorpService
    {
        public HeavyOrdnanceCorpService(RandomBotDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        private readonly RandomBotDbContext DbContext;

        public void Dispose()
        {
            this.DbContext.Dispose();
        }

        public async Task<EmbedBuilder> GetHocInfo(string hocName)
        {
            var hocs = await this.GetHocModel(hocName);
            
            var embed = new EmbedBuilder()
                .WithAuthor("HOC Info:")
                .WithColor(Color.DarkRed);

            if (hocs.Count == 0)
            {
                embed.AddField("Error", $"HOC with name { hocName } not found");
                return embed;
            }

            var specifiedHocFound = hocs.Any(Q => string.Equals(Q.Name, hocName, StringComparison.InvariantCultureIgnoreCase));
            if (hocs.Count != 1 && !specifiedHocFound)
            {
                var builder = new StringBuilder();
                for (var i = 0; i < hocs.Count; i++)
                {
                    builder.AppendLine($"{ i + 1 }. { hocs[i].Name }");
                }

                embed.AddField($"Multiple Doll with keyword '{ hocName }' Found", builder.ToString());

                return embed;
            }

            return this.GenerateHocData(embed, hocs.FirstOrDefault());
        }

        private async Task<List<HocModel>> GetHocModel(string hocName)
        {
            var hocs = await
                (from h in this.DbContext.HeavyOrdnanceCorp
                 where h.Name.Contains(hocName)
                 select new HocModel
                 {
                     Name = h.Name,
                     Chip = h.Chip,
                     Lethality = h.Lethality,
                     Pierce = h.Pierce,
                     Precision = h.Precision,
                     Reload = h.Reload,
                     Range = h.Range,
                     NormalAttack = h.NormalAttack,
                     Skill1 = h.Skill1,
                     Skill2 = h.Skill2,
                     Skill3 = h.Skill3
                 })
                 .AsNoTracking()
                 .ToListAsync();

            return hocs;
        }

        private EmbedBuilder GenerateHocData(EmbedBuilder embed, HocModel hoc)
        {
            embed
                .AddField("Name", hoc.Name, true)
                .AddField("Lethality", hoc.Lethality, true)
                .AddField("Pierce", hoc.Pierce, true)
                .AddField("Precision", hoc.Precision, true)
                .AddField("Reload", hoc.Reload, true)
                .AddField("Range", hoc.Range, true)
                .AddField("Normal Attack", hoc.NormalAttack)
                .AddField("Skill 1", hoc.Skill1)
                .AddField("Skill 2", hoc.Skill2)
                .AddField("Skill 3", hoc.Skill3);

            // There are currently 2 kinds of chips, Red and Blue.
            if (hoc.Chip == "Red")
            {
                embed.WithColor(Discord.Color.Red);
            }
            else
            {
                embed.WithColor(Discord.Color.Blue);
            }

            return embed;
        }
    }
}
