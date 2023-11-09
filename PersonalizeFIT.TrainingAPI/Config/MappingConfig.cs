using AutoMapper;
using TrainingAPI.Business.TrainingGroup;
using TrainingAPI.Entity.Entities;
using TrainingAPI.Entity.Models.TrainingGroup;
using TrainingAPI.Entity.Models.TrainingPreset;

namespace PersonalizeFIT.TrainingAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PostTrainingPresetRequest, TrainingPresetEntity>().ReverseMap();
                config.CreateMap<GetTrainingPresetResponse, TrainingPresetEntity>().ReverseMap();
                config.CreateMap<UpdateTrainingPresetRequest, TrainingPresetEntity>().ReverseMap();

                config.CreateMap<PostTrainingGroupRequest, TrainingGroupEntity>().ReverseMap();
                config.CreateMap<PostTrainingGroupHasExerciseRequest, TrainingGroupHasExercise>().ReverseMap();
                config.CreateMap<UpdateTrainingGroupRequest, TrainingGroupEntity>().ReverseMap();
                config.CreateMap<UpdateTrainingGroupHasExerciseRequest, TrainingGroupHasExercise>().ReverseMap();
                config.CreateMap<GetTrainingGroupResponse, TrainingGroupEntity>().ReverseMap();                
                config.CreateMap<TrainingGroupHasExerciseResponse, TrainingGroupHasExercise>().ReverseMap();
                config.CreateMap<TrainingPresetResponse, TrainingPresetEntity>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
