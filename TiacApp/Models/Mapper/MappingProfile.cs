using AutoMapper;
using TiacApp.Application.DTOs;

namespace TiacApp.Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SocialMediaDTO, SocialMedia>();
            CreateMap<SocialMedia, SocialMediaDTO>();
            CreateMap<PersoneInputDTO, Person>();
            CreateMap<Person, PersoneInputDTO>();
        }
    }
}
