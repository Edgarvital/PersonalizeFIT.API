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
    public class DeleteTrainingPreset : IDeleteTrainingPreset
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public DeleteTrainingPreset(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetTrainingPresetResponse> DeleteTrainingPresetAsync(int Id)
        {
            var TrainingPreset = await _context.TrainingPresets
                .FirstOrDefaultAsync(e => e.Id == Id) ?? throw new NotFoundException("Training Preset não encontrado.");

            _context.TrainingPresets.Remove(TrainingPreset);

            await _context.SaveChangesAsync();

            return _mapper.Map<GetTrainingPresetResponse>(TrainingPreset);
        }
    }

    public interface IDeleteTrainingPreset
    {
        public Task<GetTrainingPresetResponse> DeleteTrainingPresetAsync(int Id); 
    }
}
