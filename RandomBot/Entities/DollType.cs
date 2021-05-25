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

        [Key]
        [StringLength(3)]
        public string DollTypeCode { get; set; }
        [Required]
        [StringLength(14)]
        public string DollTypeName { get; set; }

        [InverseProperty("DollTypeCodeNavigation")]
        public ICollection<Doll> Doll { get; set; }
    }
}
