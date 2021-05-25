using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomBot.Entities
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
        public int SRCount { get; set; }
        public int SSRCount { get; set; }

        [InverseProperty("User")]
        public ICollection<GachaHistoryDetail> GachaHistoryDetail { get; set; }
    }
}
