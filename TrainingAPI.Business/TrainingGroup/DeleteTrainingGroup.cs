using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;

namespace TrainingAPI.Business.TrainingGroup
{
    public class DeleteTrainingGroup : IDeleteTrainingGroup
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public DeleteTrainingGroup(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteTrainingGroupAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDeleteTrainingGroup
    {
        public Task DeleteTrainingGroupAsync(int id);
    }
}
