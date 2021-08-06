using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class ShipfuRarity
    {
        public ShipfuRarity()
        {
            Shipfu = new HashSet<Shipfu>();
        }

        [Key]
        public int ShipfuRarityId { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipfuRarityName { get; set; }
        public int ShipfuRarityPercentage { get; set; }

        [InverseProperty("ShipfuRarity")]
        public virtual ICollection<Shipfu> Shipfu { get; set; }
    }
}
