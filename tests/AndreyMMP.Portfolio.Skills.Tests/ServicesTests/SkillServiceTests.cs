using AndreyMMP.Portfolio.Skills.Application.DTO;
using AndreyMMP.Portfolio.Skills.Application.Response;
using AndreyMMP.Portfolio.Skills.Application.Services;
using AndreyMMP.Portfolio.Skills.Domain.Interfaces;
using AndreyMMP.Portfolio.Skills.Domain.Models;
using AndreyMMP.Portfolio.Skills.Tests.Fixture;
using AutoMapper;
using Moq;

namespace AndreyMMP.Portfolio.Skills.Tests.ServicesTests
{
    public class SkillServiceTests
    {
        private readonly Mock<ISkillRepository> _skillRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly SkillService _skillService;

        public SkillServiceTests()
        {
            _skillRepository = new Mock<ISkillRepository>();
            _mapper = new Mock<IMapper>();
            _skillService = new SkillService(_skillRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task CreateSkill_WithEmptyName_ShouldThrowExeception()
        {
            var result = () => _skillService.CreateSkill(new SkillDTO { Name = string.Empty });

            Exception ex = await Assert.ThrowsAsync<ArgumentException>(result);
            Assert.Equal(SkillResponse.SkillNameCantBeEmpty, ex.Message);
        }

        [Fact]
        public async Task CreateSkill_WithName_ShouldNotThrowExeception()
        {
            var skillDTO = new SkillDTO { Name = SkillFixture.CreateString(10) };
            _skillRepository.Setup(s => s.CreateSkill(new Skill { Name = skillDTO.Name })).Returns(Task.CompletedTask);
            
            var result = _skillService.CreateSkill(skillDTO);
            Assert.True(result.Exception == null);
        }
    }
}