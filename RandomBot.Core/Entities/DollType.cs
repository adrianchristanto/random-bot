using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
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
        public virtual ICollection<Doll> Doll { get; set; }
    }
}
