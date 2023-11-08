using AutoMapper;
using TrainingAPI.Business.TrainingGroup;
using TrainingAPI.Entity.Entities;
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
            });
            return mappingConfig;
        }
    }
}
