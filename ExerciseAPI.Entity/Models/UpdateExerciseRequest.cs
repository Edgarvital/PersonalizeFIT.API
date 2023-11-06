﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseAPI.Entity.Models
{
    public class UpdateExerciseRequest
    {   
        public string Name { get; set; }
        public string Status { get; set; }
        public int MuscularGroupId { get; set; }

        public List<int> EquivalentExerciseIds { get; set; }
    }

}
