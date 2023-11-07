﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;

namespace TrainingAPI.Business.TrainingGroup
{
    public class DeleteTrainingPreset : IDeleteTrainingPreset
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public DeleteTrainingPreset(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task DeleteTrainingPresetAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDeleteTrainingPreset
    {
        public Task DeleteTrainingPresetAsync(int Id); 
    }
}
