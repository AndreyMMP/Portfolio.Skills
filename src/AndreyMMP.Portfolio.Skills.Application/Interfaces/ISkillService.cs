using AndreyMMP.Portfolio.Skills.Application.DTO;

namespace AndreyMMP.Portfolio.Skills.Application.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDTO>> GetSkills();
        Task<SkillDTO> GetSkillById(int id);
        Task CreateSkill(SkillDTO skill);
        Task UpdateSkill(SkillDTO skill);
        Task DeleteSkill(int id);
    }
}