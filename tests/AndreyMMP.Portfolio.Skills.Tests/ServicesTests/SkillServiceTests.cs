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
        public async Task CreateSkill_WithEmptyName_ShouldThrowException()
        {
            var skillDTO = new SkillDTO { Name = string.Empty };
            var result = () => _skillService.CreateSkill(skillDTO);

            Exception ex = await Assert.ThrowsAsync<ArgumentException>(result);
            Assert.Equal(SkillResponse.SkillNameCantBeEmpty, ex.Message);
            _skillRepository.Verify(r => r.CreateSkill(It.IsAny<Skill>()), Times.Never);
        }

        [Fact]
        public async Task CreateSkill_WithName_ShouldNotThrowException()
        {
            var skillDTO = new SkillDTO { Name = SkillFixture.CreateString(10) };
            _skillRepository.Setup(s => s.CreateSkill(It.IsAny<Skill>())).Returns(Task.CompletedTask);

            var result = _skillService.CreateSkill(skillDTO);
            await Task.Run(() => result);

            Assert.True(result.IsCompleted);
            Assert.True(result.Exception == null);
            _skillRepository.Verify(r => r.CreateSkill(It.IsAny<Skill>()), Times.Once);
        }

        [Fact]
        public async Task UpdateSkill_WithEmptyId_ShouldThrowException()
        {
            var result = () => _skillService.UpdateSkill(new SkillDTO() { Id = 0 });

            Exception ex = await Assert.ThrowsAsync<KeyNotFoundException>(result);
            Assert.Equal(SkillResponse.NoSkillRecordsFound, ex.Message);
            _skillRepository.Verify(r => r.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(r => r.UpdateSkill(It.IsAny<Skill>()), Times.Never);
        }

        [Fact]
        public async Task UpdateSkill_WithEmptyName_ShouldThrowException()
        {
            var skillDTO = new SkillDTO() { Id = SkillFixture.CreateInt() };
            _skillRepository.Setup(s => s.GetSkillById(skillDTO.Id)).Returns(Task.FromResult(new Skill { Id = skillDTO.Id, Name = SkillFixture.CreateString(10)}));

            var result = () => _skillService.UpdateSkill(skillDTO);

            Exception ex = await Assert.ThrowsAsync<ArgumentException>(result);
            Assert.Equal(SkillResponse.SkillNameCantBeEmpty, ex.Message);

            _skillRepository.Verify(r => r.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(r => r.UpdateSkill(It.IsAny<Skill>()), Times.Never);
        }

        [Fact]
        public async Task UpdateSkill_WithIdAndName_ShouldNotThrowException()
        {
            var skillDTO = new SkillDTO() { Id = SkillFixture.CreateInt(), Name = SkillFixture.CreateString(10) };
            _skillRepository.Setup(s => s.GetSkillById(skillDTO.Id)).Returns(Task.FromResult(new Skill { Id = skillDTO.Id, Name = SkillFixture.CreateString(10) }));
            _skillRepository.Setup(s => s.UpdateSkill(It.IsAny<Skill>())).Returns(Task.CompletedTask);

            var result = _skillService.UpdateSkill(skillDTO);
            await Task.Run(() => result);

            Assert.True(result.IsCompleted);
            Assert.True(result.Exception == null);
            _skillRepository.Verify(r => r.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(r => r.UpdateSkill(It.IsAny<Skill>()), Times.Once);
        }

        [Fact]
        public async Task DeleteSkill_WithEmptyId_ShouldThrowException()
        {
            var result = () => _skillService.DeleteSkill(0);

            Exception ex = await Assert.ThrowsAsync<KeyNotFoundException>(result);
            Assert.Equal(SkillResponse.NoSkillRecordsFound, ex.Message);
            _skillRepository.Verify(r => r.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(r => r.DeleteSkill(0), Times.Never);
        }

        [Fact]
        public async Task DeleteSkill_WithId_ShouldNotThrowException()
        {
            var id = SkillFixture.CreateInt();
            _skillRepository.Setup(s => s.GetSkillById(id)).Returns(Task.FromResult(new Skill { Id = id, Name = SkillFixture.CreateString(10) }));
            _skillRepository.Setup(s => s.DeleteSkill(It.IsAny<int>())).Returns(Task.CompletedTask);

            var result = _skillService.DeleteSkill(id);
            await Task.Run(() => result);

            Assert.True(result.IsCompleted);
            Assert.True(result.Exception == null);
            _skillRepository.Verify(r => r.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(r => r.DeleteSkill(It.IsAny<int>()), Times.Once);
        }
    }
}