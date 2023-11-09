using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingAPI.Business.Exceptions;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Models.TrainingGroup;

namespace TrainingAPI.Business.TrainingGroup
{
    public class UpdateTrainingGroup : IUpdateTrainingGroup
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public UpdateTrainingGroup(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> UpdateTrainingGroupAsync(int Id, UpdateTrainingGroupRequest request)
        {
            var trainingGroup = await _context.TrainingGroups
                .Include(e => e.TrainingGroupHasExercises)
                .FirstOrDefaultAsync(e => e.Id == Id) ?? throw new NotFoundException("Grupo de Treinamento não encontrado.");

            _context.TrainingGroupHasExercise.RemoveRange(trainingGroup.TrainingGroupHasExercises);
            trainingGroup.TrainingGroupHasExercises.Clear();

            _mapper.Map(request, trainingGroup);

            await _context.SaveChangesAsync();

            return "Grupo de Treinamento atualizado com sucesso.";
        }


    }

    public interface IUpdateTrainingGroup
    {
        public Task<string> UpdateTrainingGroupAsync(int Id, UpdateTrainingGroupRequest request);
    }
}
