using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;

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

        public async Task GetAllTrainingGroupsAsync()
        {
            throw new NotImplementedException();
        }
    }

    public interface IGetAllTrainingGroups
    {
        public Task GetAllTrainingGroupsAsync();
    }
}
