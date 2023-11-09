using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Models.TrainingPreset;

namespace TrainingAPI.Business.TrainingGroup
{
    public class GetAllTrainingPresets : IGetAllTrainingPresets
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public GetAllTrainingPresets(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetTrainingPresetResponse>> GetAllTrainingPresetsAsync()
        {
            var TrainingPresets = await _context.TrainingPresets
                .ProjectTo<GetTrainingPresetResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return TrainingPresets;
        }
    }

    public interface IGetAllTrainingPresets
    {
        public Task<List<GetTrainingPresetResponse>> GetAllTrainingPresetsAsync();
    }
}
