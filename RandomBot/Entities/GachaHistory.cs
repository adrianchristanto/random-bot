using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomBot.Entities
{
    public class GachaHistory
    {
        public int NormalCount { get; set; }

        public int RareCount { get; set; }

        public int SRCount { get; set; }

        public int SSRCount { get; set; }

        [Key]
        public string UserId { get; set; }
    }
}
