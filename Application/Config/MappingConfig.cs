using AutoMapper;
using Connectors.ExternalServices.Models;
using Entity.Entities;
using Entity.Models;

namespace Application.Config
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<PostTrainerHasStudentsRequest, TrainerHasStudentEntity>()
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                    .ReverseMap();
                config.CreateMap<AuthUserWithRoles, GetTrainerStudentResponse<RoleMappingResponse>>().ReverseMap();
            });
            return mappingConfig;
        }

    }
}
