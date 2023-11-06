using AutoMapper;
using ExerciseAPI.Entity.Entities;
using ExerciseAPI.Entity.Models;

namespace PersonalizeFIT.ExerciseAPI.Config
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {               
                config.CreateMap<PostMuscularGroupRequest, MuscularGroupEntity>().ReverseMap();
                config.CreateMap<GetMuscularGroupResponse, MuscularGroupEntity>().ReverseMap();

                config.CreateMap<PostExerciseRequest, ExerciseEntity>().ReverseMap();
                config.CreateMap<UpdateExerciseRequest, ExerciseEntity>().ReverseMap();
                config.CreateMap<ExerciseEntity, GetExerciseResponse>()
                    .ForMember(dest => dest.MuscularGroup, opt => opt.MapFrom(src => src.MuscularGroup))
                    .ForMember(dest => dest.EquivalentExercises, opt => opt.MapFrom(src => src.EquivalentExercises.Select(e => new ExerciseAttributesResponse
                    {
                        Id = e.Id,
                        Name = e.Name
                    })))
                    .ReverseMap();

                
            });
            return mappingConfig;
        }



    }
}
