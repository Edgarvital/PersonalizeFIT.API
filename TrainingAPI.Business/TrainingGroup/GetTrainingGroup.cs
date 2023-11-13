using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Business.Exceptions;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Models.TrainingGroup;

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

        public async Task<GetTrainingGroupResponse> GetTrainingGroupAsync(int Id)
        {
            var trainingGroup = await _context.TrainingGroups
                .Include(e => e.TrainingGroupHasExercises)
                .FirstOrDefaultAsync(e => e.Id == Id) ?? throw new NotFoundException("Grupo de Treinamento não encontrado.");

            return _mapper.Map<GetTrainingGroupResponse>(trainingGroup);
        }
    }

    public interface IGetTrainingGroup
    {
        public Task<GetTrainingGroupResponse> GetTrainingGroupAsync(int Id);
    }
}
