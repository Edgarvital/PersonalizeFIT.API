using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;

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

        public Task GetAllTrainingPresetsAsync()
        {
            throw new NotImplementedException();
        }
    }

    public interface IGetAllTrainingPresets
    {
        public Task GetAllTrainingPresetsAsync();
    }
}
