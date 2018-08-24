using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Entities
{
    public class GachaHistoryDetail
    {
        [Key]
        public int GachaHistoryDetailId { get; set; }

        public int GetCount { get; set; }

        public int ShipfuId { get; set; }

        public string UserId { get; set; }
    }
}
