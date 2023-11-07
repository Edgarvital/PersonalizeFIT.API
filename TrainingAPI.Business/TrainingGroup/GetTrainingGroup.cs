using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;

namespace TrainingAPI.Business.TrainingGroup
{
    public class GetTrainingGroup : IGetTrainingGroup
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public GetTrainingGroup(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task GetTrainingGroupAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IGetTrainingGroup
    {
        public Task GetTrainingGroupAsync(int Id);
    }
}
