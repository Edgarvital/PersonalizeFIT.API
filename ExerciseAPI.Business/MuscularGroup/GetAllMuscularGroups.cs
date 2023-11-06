using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Business.MuscularGroup
{
    public class GetAllMuscularGroups : IGetAllMuscularGroups
    {
        private readonly ExerciseDbContext _exerciseContext;
        private readonly IMapper _mapper;

        public GetAllMuscularGroups(ExerciseDbContext exerciseContext, IMapper mapper)
        {
            _exerciseContext = exerciseContext;
            _mapper = mapper;
        }
        public async Task<List<GetMuscularGroupResponse>> GetAllMuscularGroupsAsync()
        {
            return await _exerciseContext.MuscularGroups
                .ProjectTo<GetMuscularGroupResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }

    public interface IGetAllMuscularGroups
    {
        public Task<List<GetMuscularGroupResponse>> GetAllMuscularGroupsAsync();
    }
}
