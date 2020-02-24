using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomBot.Entities
{
    public partial class Shipfu
    {
        public Shipfu()
        {
            GachaHistoryDetail = new HashSet<GachaHistoryDetail>();
        }

        public int ShipfuId { get; set; }
        public int ShipfuRarityId { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipfuName { get; set; }
        [Required]
        public string ShipfuImgUrl { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }

        [ForeignKey("ShipfuRarityId")]
        [InverseProperty("Shipfu")]
        public ShipfuRarity ShipfuRarity { get; set; }
        [InverseProperty("Shipfu")]
        public ICollection<GachaHistoryDetail> GachaHistoryDetail { get; set; }
    }
}
