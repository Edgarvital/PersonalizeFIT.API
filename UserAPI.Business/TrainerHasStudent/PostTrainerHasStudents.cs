using AutoMapper;
using System.ComponentModel.DataAnnotations;
using UserAPI.Connectors.Database;
using UserAPI.Entity.Models;
using UserAPI.Connectors.ExternalServices;
using UserAPI.Entity.Entities;

namespace UserAPI.Business.TrainerHasStudent
{
    public class PostTrainerHasStudents : IPostTrainerHasStudents
    {

        private readonly UserDbContext _authUserDbContext;
        private readonly IMapper _mapper;
        private readonly IAuthAPI _authAPI;

        public PostTrainerHasStudents(UserDbContext authUserDbContext, IMapper mapper, IAuthAPI authAPI)
        {
            _authUserDbContext = authUserDbContext;
            _mapper = mapper;
            _authAPI = authAPI;
        }

        public async Task<string> CreateTrainerHasStudents(PostTrainerHasStudentsRequest request)
        {
            var trainerHasStudentsEntity = _mapper.Map<TrainerHasStudentEntity>(request);

            await ValidateTrainerHasStudents(trainerHasStudentsEntity);

            _authUserDbContext.TrainerHasStudent.Add(trainerHasStudentsEntity);
            await _authUserDbContext.SaveChangesAsync();

            return "Vinculo de treinador e aluno criado com sucesso.";
        }

        private async Task ValidateTrainerHasStudents(TrainerHasStudentEntity trainerHasStudent)
        {
            var student_id = trainerHasStudent.StudentId;
            var trainer_id = trainerHasStudent.TrainerId;
            var student = await _authAPI.GetAuthUserById(student_id);
            if (!student.Role.Any(role => role.Name == "student-role"))
            {
                throw new ValidationException("O usuário precisa ser um aluno.");
            }
            var trainer = await _authAPI.GetAuthUserById(trainer_id);
            if (!trainer.Role.Any(role => role.Name == "trainer-role"))
            {
                throw new ValidationException("O usuário precisa ser um treinador.");
            }
        }
    }


    public interface IPostTrainerHasStudents
    {
        public Task<string> CreateTrainerHasStudents(PostTrainerHasStudentsRequest request);
    }
}
