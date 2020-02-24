using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomBot.Entities
{
    public partial class DollType
    {
        public DollType()
        {
            Doll = new HashSet<Doll>();
        }

        public int DollTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string DollTypeName { get; set; }

        [InverseProperty("DollType")]
        public ICollection<Doll> Doll { get; set; }
    }
}
