namespace RandomBot.Models
{
    public class DollModel
    {
        public string DollName { get; set; }

        public string DollTypeName { get; set; }

        public string HP { get; set; }

        public string Damage { get; set; }

        public string Accuracy { get; set; }

        public string Evasion { get; set; }

        public string ROF { get; set; }

        public string Armor { get; set; }

        public string ClipSize { get; set; }

        public string Skill1 { get; set; }

        public string Skill2 { get; set; }

        public int TileDollLocation { get; set; }

        public string TileBonusLocation { get; set; }

        public string TileModBonusLocation { get; set; }

        public string TileEffect { get; set; }
    }
}
