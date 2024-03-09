using AndreyMMP.Portfolio.Skills.Domain.Models;

namespace AndreyMMP.Portfolio.Skills.Domain.Interfaces
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetSkills();
        Task<Skill> GetSkillById(int id);
        Task CreateSkill(Skill skill);
        Task UpdateSkill(Skill skill);
        Task DeleteSkill(int id);
    }
}