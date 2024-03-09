using AndreyMMP.Portfolio.Skills.Application.DTO;
using AndreyMMP.Portfolio.Skills.Domain.Models;
using AutoMapper;

namespace AndreyMMP.Portfolio.Skills.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Skill, SkillDTO>().ReverseMap();
        }
    }
}