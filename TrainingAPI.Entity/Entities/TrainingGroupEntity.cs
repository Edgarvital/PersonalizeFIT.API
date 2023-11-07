using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAPI.Entity.Entities
{
    public class TrainingGroupEntity : BaseEntity
    {
        public int TrainingPresetId { get; set; }
        public string Name { get; set; }

        public TrainingPresetEntity TrainingPreset { get; set; }
    }
}
