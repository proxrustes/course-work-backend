using System.ComponentModel.DataAnnotations;

namespace DBAccess.Models
{
    public class Character
    {
        public int userId { get; set; }

        [Key]
        public int characterId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string bio { get; set; }

        public string race { get; set; }
        [Range(0, 10)]
        public int strength { get; set; }
        [Range(0, 10)]
        public int intellect { get; set; }
        [Range(0, 10)]
        public int charisma { get; set; }
        [Range(0, 10)]
        public int wisdom { get; set; }
        [Range(0, 10)]
        public int dexterity { get; set; }

        public string? imageLink { get; set; }
    }

}
