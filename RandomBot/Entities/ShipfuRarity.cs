using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomBot.Entities
{
    public partial class ShipfuRarity
    {
        public ShipfuRarity()
        {
            Shipfu = new HashSet<Shipfu>();
        }

        public int ShipfuRarityId { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipfuRarityName { get; set; }
        public int ShipfuRarityPercentage { get; set; }

        [InverseProperty("ShipfuRarity")]
        public ICollection<Shipfu> Shipfu { get; set; }
    }
}
