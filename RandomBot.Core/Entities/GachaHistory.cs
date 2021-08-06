using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class GachaHistory
    {
        public GachaHistory()
        {
            GachaHistoryDetail = new HashSet<GachaHistoryDetail>();
        }

        [Key]
        [StringLength(20)]
        public string UserId { get; set; }
        public int NormalCount { get; set; }
        public int RareCount { get; set; }
        [Column("SRCount")]
        public int SrCount { get; set; }
        [Column("SSRCount")]
        public int SsrCount { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<GachaHistoryDetail> GachaHistoryDetail { get; set; }
    }
}
