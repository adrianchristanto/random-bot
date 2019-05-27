using System.ComponentModel.DataAnnotations;

namespace RandomBot.Entities
{
    public class DollType
    {
        [Key]
        public int DollTypeId { get; set; }

        public string DollTypeName { get; set; }
    }
}
