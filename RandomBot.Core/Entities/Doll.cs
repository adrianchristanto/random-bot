using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RandomBot.Core.Entities
{
    public partial class Doll
    {
        [Key]
        public Guid DollId { get; set; }
        [Required]
        [StringLength(100)]
        public string DollName { get; set; }
        [Required]
        [StringLength(3)]
        public string DollTypeCode { get; set; }
        [Required]
        [Column("HP")]
        [StringLength(12)]
        public string Hp { get; set; }
        [Required]
        [StringLength(12)]
        public string Damage { get; set; }
        [Required]
        [StringLength(12)]
        public string Accuracy { get; set; }
        [Required]
        [StringLength(12)]
        public string Evasion { get; set; }
        [Required]
        [Column("ROF")]
        [StringLength(12)]
        public string Rof { get; set; }
        [Required]
        [StringLength(12)]
        public string Armor { get; set; }
        [Required]
        [StringLength(8)]
        public string ClipSize { get; set; }
        [Required]
        [StringLength(1010)]
        public string Skill1 { get; set; }
        [StringLength(1010)]
        public string Skill2 { get; set; }
        public int TileDollLocation { get; set; }
        [Required]
        [StringLength(20)]
        public string TileBonusLocation { get; set; }
        [StringLength(20)]
        public string TileModBonusLocation { get; set; }
        [Required]
        [StringLength(255)]
        public string TileEffect { get; set; }

        [ForeignKey(nameof(DollTypeCode))]
        [InverseProperty(nameof(DollType.Doll))]
        public virtual DollType DollTypeCodeNavigation { get; set; }
    }
}
