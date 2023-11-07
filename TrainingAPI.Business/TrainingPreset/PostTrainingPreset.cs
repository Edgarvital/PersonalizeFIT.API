﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;

namespace TrainingAPI.Business.TrainingGroup
{
    public class PostTrainingPreset : IPostTrainingPreset
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public PostTrainingPreset(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task PostTrainingPresetAsync(string request)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPostTrainingPreset
    {
        public Task PostTrainingPresetAsync(string request);
    }
}
