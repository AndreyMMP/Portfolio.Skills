using AndreyMMP.Portfolio.Skills.Application.DTO;
using AndreyMMP.Portfolio.Skills.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
                return Ok();
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

                if (skills.IsNullOrEmpty())
                {
                    return NoContent();
                }

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

                if (skill == null)
                {
                    return NoContent();
                }

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
                SkillDTO skillToUpdate = await _skillService.GetSkillById(skill.Id);

                if (skillToUpdate == null)
                {
                    return NoContent();
                }

                await _skillService.UpdateSkill(skill);
                return Ok();
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
                SkillDTO skillToDelete = await _skillService.GetSkillById(id);
                if (skillToDelete == null)
                {
                    return NoContent();
                }

                await _skillService.DeleteSkill(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}