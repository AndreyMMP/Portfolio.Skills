using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AndreyMMP.Portfolio.Skills.Domain.Models
{
    [Table("skill", Schema = "portfolio")]
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