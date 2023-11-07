using AutoMapper;

namespace PersonalizeFIT.TrainingAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<PostMuscularGroupRequest, MuscularGroupEntity>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
