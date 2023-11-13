using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Business.Exceptions;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Models.TrainingPreset;

namespace TrainingAPI.Business.TrainingGroup
{
    public class GetTrainingPreset : IGetTrainingPreset
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public GetTrainingPreset(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetTrainingPresetResponse> GetTrainingPresetAsync(int Id)
        {
            var trainingPreset = await _context.TrainingPresets
                .FirstOrDefaultAsync(e => e.Id == Id) ?? throw new NotFoundException("Training Preset não encontrado.");

            return _mapper.Map<GetTrainingPresetResponse>(trainingPreset);
        }
    }

    public interface IGetTrainingPreset
    {
        public Task<GetTrainingPresetResponse> GetTrainingPresetAsync(int Id);
    }
}
