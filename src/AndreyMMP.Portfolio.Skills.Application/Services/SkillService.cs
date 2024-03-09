using AndreyMMP.Portfolio.Skills.Application.DTO;
using AndreyMMP.Portfolio.Skills.Application.Interfaces;
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
            await _skillRepository.CreateSkill(_mapper.Map<Skill>(skill));
        }

        public async Task<IEnumerable<SkillDTO>> GetSkills()
        {
            return _mapper.Map<IEnumerable<SkillDTO>>(await _skillRepository.GetSkills());
        }

        public async Task<SkillDTO> GetSkillById(int id)
        {
            return _mapper.Map<SkillDTO>(await _skillRepository.GetSkillById(id));
        }

        public async Task UpdateSkill(SkillDTO skill)
        {
            await _skillRepository.UpdateSkill(_mapper.Map<Skill>(skill));
        }

        public async Task DeleteSkill(int id)
        {
            await _skillRepository.DeleteSkill(id);
        }
    }
}