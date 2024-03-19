using AndreyMMP.Portfolio.Skills.API.Controllers;
using AndreyMMP.Portfolio.Skills.Application.DTO;
using AndreyMMP.Portfolio.Skills.Application.Response;
using AndreyMMP.Portfolio.Skills.Application.Services;
using AndreyMMP.Portfolio.Skills.Domain.Interfaces;
using AndreyMMP.Portfolio.Skills.Domain.Models;
using AndreyMMP.Portfolio.Skills.Tests.Fixture;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AndreyMMP.Portfolio.Skills.Tests.ControllersTests
{
    public class SkillControllerTests
    {
        private readonly Mock<ISkillRepository> _skillRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly SkillService _skillService;
        private readonly SkillController _skillController;

        public SkillControllerTests()
        {
            _skillRepository = new Mock<ISkillRepository>();
            _mapper = new Mock<IMapper>();
            _skillService = new SkillService(_skillRepository.Object, _mapper.Object);
            _skillController = new SkillController(_skillService);
        }

        [Fact]
        public async Task CreateSkill_WithEmptyName_ShouldReturnFailureMessage()
        {
            var skillDTO = new SkillDTO { Name = string.Empty };
            var result = _skillController.CreateSkill(skillDTO);
            await Task.Run(() => result);

            var badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.Equal(SkillResponse.SkillNameCantBeEmpty, badRequestObjectResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);
            _skillRepository.Verify(s => s.CreateSkill(It.IsAny<Skill>()), Times.Never);
        }

        [Fact]
        public async Task CreateSkill_WithName_ShouldReturnSkillCreatedMessage()
        {
            var skillDTO = new SkillDTO { Name = SkillFixture.CreateString(10) };
            _skillRepository.Setup(s => s.CreateSkill(It.IsAny<Skill>())).Returns(Task.CompletedTask);

            var result = _skillController.CreateSkill(skillDTO);
            await Task.Run(() => result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.Equal(SkillResponse.SkillCreated, okObjectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.True(result.Exception == null);
            _skillRepository.Verify(s => s.CreateSkill(It.IsAny<Skill>()), Times.Once);
        }

        [Fact]
        public async Task UpdateSkill_WithEmptyId_ShouldReturnNoSkillRecordsFoundMessage()
        {
            var skillDTO = new SkillDTO() { Name = SkillFixture.CreateString(10) };            

            var result = _skillController.UpdateSkill(skillDTO);
            await Task.Run(() => result);

            var badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.Equal(SkillResponse.NoSkillRecordsFound, badRequestObjectResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);
            _skillRepository.Verify(s => s.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(s => s.CreateSkill(It.IsAny<Skill>()), Times.Never);
        }

        [Fact]
        public async Task UpdateSkill_WithIdButNoSkillsRecords_ShouldReturnNoSkillRecordsFoundMessage()
        {
            var skillDTO = new SkillDTO() { Id = SkillFixture.CreateInt(), Name = SkillFixture.CreateString(10) };
            _skillRepository.Setup(s => s.GetSkillById(skillDTO.Id)).Returns(Task.FromResult((Skill)null));

            var result = _skillController.UpdateSkill(skillDTO);
            await Task.Run(() => result);

            var badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.Equal(SkillResponse.NoSkillRecordsFound, badRequestObjectResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);
            _skillRepository.Verify(s => s.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(s => s.CreateSkill(It.IsAny<Skill>()), Times.Never);
        }

        [Fact]
        public async Task UpdateSkill_WithEmptyName_ShouldReturnSkillNameCantBeEmptyMessage()
        {
            var skillDTO = new SkillDTO() { Id = SkillFixture.CreateInt() };
            _skillRepository.Setup(s => s.GetSkillById(skillDTO.Id)).Returns(Task.FromResult(new Skill() { Id = skillDTO.Id }));

            var result = _skillController.UpdateSkill(skillDTO);
            await Task.Run(() => result);

            var badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.Equal(SkillResponse.SkillNameCantBeEmpty, badRequestObjectResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);
            _skillRepository.Verify(s => s.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(s => s.CreateSkill(It.IsAny<Skill>()), Times.Never);
        }

        [Fact]
        public async Task UpdateSkill_WithIdAndName_ShouldReturnSkillUpdatedMessage()
        {
            var skillDTO = new SkillDTO() { Id = SkillFixture.CreateInt(), Name = SkillFixture.CreateString(10) };
            _skillRepository.Setup(s => s.GetSkillById(skillDTO.Id)).Returns(Task.FromResult(new Skill { Id = skillDTO.Id, Name = SkillFixture.CreateString(10) }));
            _skillRepository.Setup(s => s.UpdateSkill(It.IsAny<Skill>())).Returns(Task.CompletedTask);

            var result = _skillController.UpdateSkill(skillDTO);
            await Task.Run(() => result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.Equal(SkillResponse.SkillUpdated, okObjectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.True(result.Exception == null);
            _skillRepository.Verify(s => s.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(s => s.UpdateSkill(It.IsAny<Skill>()), Times.Once);
        }

        [Fact]
        public async Task DeleteSkill_WithEmptyId_ShouldReturnNoSkillRecordsFoundMessage()
        {
            var skillDTO = new SkillDTO() { Name = SkillFixture.CreateString(10) };

            var result = _skillController.DeleteSkill(skillDTO.Id);
            await Task.Run(() => result);

            var badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.Equal(SkillResponse.NoSkillRecordsFound, badRequestObjectResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);
            _skillRepository.Verify(s => s.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(s => s.CreateSkill(It.IsAny<Skill>()), Times.Never);
        }

        [Fact]
        public async Task DeleteSkill_WithId_ShouldReturnSkillDeletedMessage()
        {
            var skillDTO = new SkillDTO() { Id = SkillFixture.CreateInt(), Name = SkillFixture.CreateString(10) };
            _skillRepository.Setup(s => s.GetSkillById(skillDTO.Id)).Returns(Task.FromResult(new Skill { Id = skillDTO.Id, Name = SkillFixture.CreateString(10) }));
            _skillRepository.Setup(s => s.DeleteSkill(It.IsAny<int>())).Returns(Task.CompletedTask);

            var result = _skillController.DeleteSkill(skillDTO.Id);
            await Task.Run(() => result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.Equal(SkillResponse.SkillDeleted, okObjectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.True(result.Exception == null);
            _skillRepository.Verify(s => s.GetSkillById(It.IsAny<int>()), Times.Once);
            _skillRepository.Verify(s => s.DeleteSkill(It.IsAny<int>()), Times.Once);
        }
    }    
}