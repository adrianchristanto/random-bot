using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomBot.Entities
{
    public partial class GachaHistoryDetail
    {
        public int GachaHistoryDetailId { get; set; }
        [Required]
        [StringLength(20)]
        public string UserId { get; set; }
        public int ShipfuId { get; set; }
        public int GetCount { get; set; }

        [ForeignKey("ShipfuId")]
        [InverseProperty("GachaHistoryDetail")]
        public Shipfu Shipfu { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("GachaHistoryDetail")]
        public GachaHistory User { get; set; }
    }
}
