using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Entities
{
    public class Shipfu
    {
        [Key]
        public int ShipfuId { get; set; }

        public string ShipfuImgUrl { get; set; }

        public string ShipfuName { get; set; }

        public int ShipfuRarityId { get; set; }
    }
}
