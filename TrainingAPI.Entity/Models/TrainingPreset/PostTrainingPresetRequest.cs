﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAPI.Entity.Models.TrainingPreset
{
    public class PostTrainingPresetRequest
    {
        public string Title { get; set; }
        public bool PresetDefaultFlag { get; set; }
        public string TrainerId { get; set; }
    }
}
