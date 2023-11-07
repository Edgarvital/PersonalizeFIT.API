using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;

namespace TrainingAPI.Business.TrainingGroup
{
    public class UpdateTrainingPreset : IUpdateTrainingPreset
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public UpdateTrainingPreset(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task UpdateTrainingPresetAsync(int Id, string request)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUpdateTrainingPreset
    {
        public Task UpdateTrainingPresetAsync(int Id, string request);
    }
}
