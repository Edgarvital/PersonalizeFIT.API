using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Business.Exceptions;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Models.TrainingGroup;

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

        public async Task<GetTrainingGroupResponse> DeleteTrainingGroupAsync(int Id)
        {
            var TrainingGroup = await _context.TrainingGroups
                .FirstOrDefaultAsync(e => e.Id == Id) ?? throw new NotFoundException("Grupo de Treinamento não encontrado.");

            _context.TrainingGroups.Remove(TrainingGroup);

            await _context.SaveChangesAsync();

            return _mapper.Map<GetTrainingGroupResponse>(TrainingGroup);
        }
    }

    public interface IDeleteTrainingGroup
    {
        public Task<GetTrainingGroupResponse> DeleteTrainingGroupAsync(int Id);
    }
}
