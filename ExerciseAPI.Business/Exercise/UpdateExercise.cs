using AutoMapper;
using ExerciseAPI.Business.Exceptions;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Entities;
using ExerciseAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExerciseAPI.Business.Exercise
{
    public class UpdateExercise : IUpdateExercise
    {
        private readonly ExerciseDbContext _exerciseContext;
        private readonly IMapper _mapper;
        public UpdateExercise(ExerciseDbContext exerciseContext, IMapper mapper)
        {
            _exerciseContext = exerciseContext;
            _mapper = mapper;
        }

        public async Task<string> UpdateExerciseAsync(int exerciseId, UpdateExerciseRequest request)
        {
            ValidateExerciseRequest(request);

            var exercise = await _exerciseContext
                .Exercises
                .Include(e => e.MuscularGroup)
                .Include(e => e.EquivalentExercises)
                .FirstOrDefaultAsync(e => e.Id == exerciseId);            

            if (exercise == null)
                throw new NotFoundException("Exercicio não encontrado.");

            _mapper.Map(request, exercise);

            SyncAllEquivalentExercises(exercise, request);

            await _exerciseContext.SaveChangesAsync();

            return "Exercicio Alterado com Sucesso!";
        }

        private void ValidateExerciseRequest(UpdateExerciseRequest request)
        {
            if (!IsMuscularGroupIdValid(request.MuscularGroupId))
            {
                throw new ValidationException("MuscularGroupId não é válido");
            }

            var distinctEquivalentExerciseIds = request.EquivalentExerciseIds.Distinct().ToList();

            foreach (int EquivalentExerciseId in distinctEquivalentExerciseIds)
            {
                if (!IsExerciseIdValid(EquivalentExerciseId))
                {
                    throw new ValidationException("EquivalentExerciseId não é Valido");
                }
            }
        }

        private bool IsMuscularGroupIdValid(int MuscularGroupId)
        {
            return _exerciseContext.MuscularGroups.Any(mg => mg.Id == MuscularGroupId);
        }

        private bool IsExerciseIdValid(int ExerciseId)
        {
            return _exerciseContext.Exercises.Any(e => e.Id == ExerciseId);
        }

        private async void SyncAllEquivalentExercises(ExerciseEntity exercise, UpdateExerciseRequest request)
        {
            var equivalentExercises = await GetValidEquivalentExercises(request);
            exercise.EquivalentExercises.Clear();
            exercise.EquivalentExercises.AddRange(equivalentExercises);
        }

        private async Task<List<ExerciseEntity>> GetValidEquivalentExercises(UpdateExerciseRequest request)
        {
            var distinctEquivalentExerciseIds = request.EquivalentExerciseIds.Distinct().ToList();

            var equivalentExercises = await _exerciseContext.Exercises
                .Where(e => distinctEquivalentExerciseIds.Contains(e.Id))
                .ToListAsync();

            return equivalentExercises;
        }               
        
    }

    public interface IUpdateExercise
    {
        public Task<string> UpdateExerciseAsync(int exerciseId, UpdateExerciseRequest request);
    }
}
