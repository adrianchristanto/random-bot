using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomBot.Entities
{
    public partial class Doll
    {
        [Key]
        [StringLength(100)]
        public string DollName { get; set; }
        public int DollTypeId { get; set; }
        [Required]
        [StringLength(12)]
        public string HP { get; set; }
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
        [StringLength(12)]
        public string ROF { get; set; }
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

        [ForeignKey("DollTypeId")]
        [InverseProperty("Doll")]
        public DollType DollType { get; set; }
    }
}
