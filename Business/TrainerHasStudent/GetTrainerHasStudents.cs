﻿using Connectors.Database;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using Entity.Entities;
using Connectors.ExternalServices;
using Connectors.ExternalServices.Models;
using Connectors.ExternalServices.Exceptions;
using System.Net;
using AutoMapper;

namespace Business.TrainerHasStudent
{
    public class GetTrainerHasStudents : IGetTrainerHasStudents
    {
        private readonly UserDbContext _authUserDbContext;
        private readonly IAuthAPI _authAPI;
        private readonly IMapper _mapper;

        public GetTrainerHasStudents(UserDbContext authUserDbContext, IAuthAPI authAPI, IMapper mapper)
        {
            _authUserDbContext = authUserDbContext;
            _authAPI = authAPI;
            _mapper = mapper;
        }

        public async Task<List<GetTrainerStudentResponse<RoleMappingResponse>>> GetAllTrainerStudents(string user_id)
        {
            var response = await _authUserDbContext.TrainerHasStudent
                .Where(TrainerHasStudent => TrainerHasStudent.TrainerId == user_id)
                .ToListAsync();

            List<GetTrainerStudentResponse<RoleMappingResponse>> list = new List<GetTrainerStudentResponse<RoleMappingResponse>>();

            foreach (TrainerHasStudentEntity trainerHasStudent in response)
            {
                string student_id = trainerHasStudent.StudentId;
                AuthUserWithRoles authUser = null;

                try
                {
                    authUser = await _authAPI.GetAuthUserById(student_id);
                }
                catch (HttpErrorResponseException ex)
                {
                    HttpResponseMessage exceptionResponse = ex.Response;
                    HttpStatusCode statusCode = exceptionResponse.StatusCode;

                    if (statusCode == HttpStatusCode.NotFound)
                    {
                        authUser = null;
                    }
                }
                if (authUser != null)
                {
                    GetTrainerStudentResponse<RoleMappingResponse> getTrainerHasStudent = _mapper.Map<GetTrainerStudentResponse<RoleMappingResponse>>(authUser);
                    list.Add(getTrainerHasStudent);
                }
                
            }

            return list;
        }
    }

    public interface IGetTrainerHasStudents
    {
        public Task<List<GetTrainerStudentResponse<RoleMappingResponse>>> GetAllTrainerStudents(string user_id);
    }
}
