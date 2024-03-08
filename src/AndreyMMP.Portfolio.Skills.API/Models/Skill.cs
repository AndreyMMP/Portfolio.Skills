using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreyMMP.Portfolio.Skills.API.Models
{
    [Table("skill", Schema ="portfolio")]
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("experience_time")]
        public int ExperienceTime { get; set; }
    }
}