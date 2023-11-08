﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Models.TrainingPreset;

namespace TrainingAPI.Business.TrainingGroup
{
    public class GetTrainingPreset : IGetTrainingPreset
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public GetTrainingPreset(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<GetTrainingPresetResponse> GetTrainingPresetAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IGetTrainingPreset
    {
        public Task<GetTrainingPresetResponse> GetTrainingPresetAsync(int Id);
    }
}