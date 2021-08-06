using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class HeavyOrdnanceCorp
    {
        [Key]
        public Guid HocId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(4)]
        public string Chip { get; set; }
        [Required]
        [StringLength(8)]
        public string Lethality { get; set; }
        [Required]
        [StringLength(8)]
        public string Pierce { get; set; }
        [Required]
        [StringLength(8)]
        public string Precision { get; set; }
        [Required]
        [StringLength(8)]
        public string Reload { get; set; }
        [Required]
        [StringLength(1)]
        public string Range { get; set; }
        [Required]
        [StringLength(1010)]
        public string NormalAttack { get; set; }
        [Required]
        [StringLength(1010)]
        public string Skill1 { get; set; }
        [Required]
        [StringLength(1010)]
        public string Skill2 { get; set; }
        [Required]
        [StringLength(1010)]
        public string Skill3 { get; set; }
    }
}
