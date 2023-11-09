using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Models.TrainingGroup;

namespace TrainingAPI.Business.TrainingGroup
{
    public class GetAllTrainingGroups : IGetAllTrainingGroups
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public GetAllTrainingGroups(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetTrainingGroupResponse>> GetAllTrainingGroupsAsync()
        {
            var TrainingGroups = await _context.TrainingGroups
                .ProjectTo<GetTrainingGroupResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return TrainingGroups;
        }
    }

    public interface IGetAllTrainingGroups
    {
        public Task<List<GetTrainingGroupResponse>> GetAllTrainingGroupsAsync();
    }
}
