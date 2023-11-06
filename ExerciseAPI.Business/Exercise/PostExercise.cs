using ExerciseAPI.Entity.Models;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Business.Exercise
{

    public class PostExercise : IPostExercise
    {
        private readonly ExerciseDbContext _exerciseDbContext;
        private readonly IMapper _mapper;
        public PostExercise(ExerciseDbContext exerciseDbContext, IMapper mapper)
        {
            _exerciseDbContext = exerciseDbContext;
            _mapper = mapper;
        }

        public async Task<string> CreateExerciseAsync(PostExerciseRequest request)
        {
            ValidateExerciseRequest(request);

            var exercise = _mapper.Map<ExerciseEntity>(request);
            _exerciseDbContext.Exercises.Add(exercise);

            var equivalentExercises = await GetValidEquivalentExercises(request);
            exercise.EquivalentExercises.AddRange(equivalentExercises);

            await _exerciseDbContext.SaveChangesAsync();

            return "Exercício Cadastrado com Sucesso!";
        }

        private async Task<List<ExerciseEntity>> GetValidEquivalentExercises(PostExerciseRequest request)
        {
            var distinctEquivalentExerciseIds = request.EquivalentExerciseIds.Distinct().ToList();

            var equivalentExercises = await _exerciseDbContext.Exercises
                .Where(e => distinctEquivalentExerciseIds.Contains(e.Id))
                .ToListAsync();

            return equivalentExercises;
        }


        private void ValidateExerciseRequest(PostExerciseRequest request)
        {
            if(!IsMuscularGroupIdValid(request.MuscularGroupId))
            {
                throw new ValidationException("MuscularGroupId não é válido");
            }

            var distinctEquivalentExerciseIds = request.EquivalentExerciseIds.Distinct().ToList();

            foreach (int EquivalentExerciseId in distinctEquivalentExerciseIds)
            {
                if(!IsExerciseIdValid(EquivalentExerciseId))
                {
                    throw new ValidationException("EquivalentExerciseId não é Valido");
                }
            }
        }

        private bool IsMuscularGroupIdValid(int MuscularGroupId)
        {
            return _exerciseDbContext.MuscularGroups.Any(mg => mg.Id == MuscularGroupId);
        }

        private bool IsExerciseIdValid(int ExerciseId)
        {
            return _exerciseDbContext.Exercises.Any(e => e.Id == ExerciseId);
        }
    }        
    

    public interface IPostExercise
    {
        public Task<string> CreateExerciseAsync(PostExerciseRequest request);
    }
}
