using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class Shipfu
    {
        public Shipfu()
        {
            GachaHistoryDetail = new HashSet<GachaHistoryDetail>();
        }

        [Key]
        public int ShipfuId { get; set; }
        public int ShipfuRarityId { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipfuName { get; set; }
        [Required]
        public string ShipfuImgUrl { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }

        [ForeignKey(nameof(ShipfuRarityId))]
        [InverseProperty("Shipfu")]
        public virtual ShipfuRarity ShipfuRarity { get; set; }
        [InverseProperty("Shipfu")]
        public virtual ICollection<GachaHistoryDetail> GachaHistoryDetail { get; set; }
    }
}
