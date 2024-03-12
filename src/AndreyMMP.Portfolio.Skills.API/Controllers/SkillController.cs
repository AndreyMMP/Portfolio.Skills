using AndreyMMP.Portfolio.Skills.Application.DTO;
using AndreyMMP.Portfolio.Skills.Application.Interfaces;
using AndreyMMP.Portfolio.Skills.Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace AndreyMMP.Portfolio.Skills.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSkill(SkillDTO skill)
        {
            try
            {
                await _skillService.CreateSkill(skill);
                return Ok(SkillResponse.SkillCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetSkills()
        {
            try
            {
                IEnumerable<SkillDTO> skills = await _skillService.GetSkills();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetSkillById(int id)
        {
            try
            {
                SkillDTO skill = await _skillService.GetSkillById(id);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkill(SkillDTO skill)
        {
            try
            {
                await _skillService.UpdateSkill(skill);
                return Ok(SkillResponse.SkillUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            try
            {
                await _skillService.DeleteSkill(id);
                return Ok(SkillResponse.SkillDeleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}