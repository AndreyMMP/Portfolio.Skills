using AndreyMMP.Portfolio.Skills.API.Context;
using AndreyMMP.Portfolio.Skills.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreyMMP.Portfolio.Skills.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly PortfolioDbContext _portfolioDbContext;

        public SkillController(PortfolioDbContext portfolioDbContext)
        {
            _portfolioDbContext = portfolioDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Skill>> GetSkills()
        {
            return _portfolioDbContext.Skills;
        }

        [HttpGet("{id:int}")]
        public async Task<Skill> GetSkillById(int id)
        {
            return await _portfolioDbContext.Skills.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSkill(Skill skill)
        {
            await _portfolioDbContext.Skills.AddAsync(skill);
            await _portfolioDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkill(Skill skill)
        {
            _portfolioDbContext.Skills.Update(skill);
            await _portfolioDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            Skill skill = await GetSkillById(id);
            _portfolioDbContext.Skills.Remove(skill);
            await _portfolioDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}