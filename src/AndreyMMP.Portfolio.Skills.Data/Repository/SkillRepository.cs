using AndreyMMP.Portfolio.Skills.Data.Context;
using AndreyMMP.Portfolio.Skills.Domain.Interfaces;
using AndreyMMP.Portfolio.Skills.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AndreyMMP.Portfolio.Skills.Data.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly PortfolioDbContext _portfolioDbContext;

        public SkillRepository(PortfolioDbContext portfolioDbContext)
        {
            _portfolioDbContext = portfolioDbContext;
        }

        public async Task<IEnumerable<Skill>> GetSkills()
        {
            return await _portfolioDbContext.Skills.ToListAsync();
        }

        public async Task<Skill> GetSkillById(int id)
        {
            return await _portfolioDbContext.Skills.FindAsync(id);
        }

        public async Task CreateSkill(Skill skill)
        {
            await _portfolioDbContext.Skills.AddAsync(skill);
            await _portfolioDbContext.SaveChangesAsync();
        }

        public async Task UpdateSkill(Skill skill)
        {
            _portfolioDbContext.Skills.Update(skill);
            await _portfolioDbContext.SaveChangesAsync();
        }

        public async Task DeleteSkill(int id)
        {
            _portfolioDbContext.Skills.Remove(new Skill { Id = id });
            await _portfolioDbContext.SaveChangesAsync();
        }
    }
}