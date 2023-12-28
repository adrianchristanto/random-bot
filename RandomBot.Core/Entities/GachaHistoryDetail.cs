using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class GachaHistoryDetail
    {
        [Key]
        public int GachaHistoryDetailId { get; set; }
        [Required]
        [StringLength(20)]
        public string UserId { get; set; }
        public int ShipfuId { get; set; }
        public int GetCount { get; set; }

        [ForeignKey(nameof(ShipfuId))]
        [InverseProperty("GachaHistoryDetail")]
        public virtual Shipfu Shipfu { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(GachaHistory.GachaHistoryDetail))]
        public virtual GachaHistory User { get; set; }
    }
}
