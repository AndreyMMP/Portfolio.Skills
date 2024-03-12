using AndreyMMP.Portfolio.Skills.Application.DTO;
using AndreyMMP.Portfolio.Skills.Application.Interfaces;
using AndreyMMP.Portfolio.Skills.Application.Response;
using AndreyMMP.Portfolio.Skills.Domain.Interfaces;
using AndreyMMP.Portfolio.Skills.Domain.Models;
using AutoMapper;

namespace AndreyMMP.Portfolio.Skills.Application.Services
{
    public class SkillService : ISkillService
    {
        private ISkillRepository _skillRepository;
        private IMapper _mapper;

        public SkillService(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task CreateSkill(SkillDTO skill)
        {
            if (string.IsNullOrEmpty(skill.Name))
            {
                throw new ArgumentException(SkillResponse.SkillNameCantBeEmpty);
            }

            await _skillRepository.CreateSkill(_mapper.Map<Skill>(skill));
        }

        public async Task<IEnumerable<SkillDTO>> GetSkills()
        {
            var skills = await _skillRepository.GetSkills();

            if (!skills.Any())
            {
                throw new Exception(SkillResponse.NoSkillRecordsFound);
            }

            return _mapper.Map<IEnumerable<SkillDTO>>(skills);
        }

        public async Task<SkillDTO> GetSkillById(int id)
        {
            var skill = await _skillRepository.GetSkillById(id);

            if (skill == null)
            {
                throw new KeyNotFoundException(SkillResponse.NoSkillRecordsFound);
            }

            return _mapper.Map<SkillDTO>(skill);
        }

        public async Task UpdateSkill(SkillDTO skill)
        {
            var skillToUpdate = await _skillRepository.GetSkillById(skill.Id);

            if (skillToUpdate == null)
            {
                throw new KeyNotFoundException(SkillResponse.NoSkillRecordsFound);
            }

            if (string.IsNullOrEmpty(skill.Name))
            {
                throw new ArgumentException(SkillResponse.SkillNameCantBeEmpty);
            }

            await _skillRepository.UpdateSkill(_mapper.Map<Skill>(skill));
        }

        public async Task DeleteSkill(int id)
        {
            var skillToDelete = await _skillRepository.GetSkillById(id);

            if (skillToDelete == null)
            {
                throw new KeyNotFoundException(SkillResponse.NoSkillRecordsFound);
            }

            await _skillRepository.DeleteSkill(id);
        }
    }
}