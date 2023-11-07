using Microsoft.AspNetCore.Mvc;
using TrainingAPI.Business.TrainingGroup;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalizeFIT.TrainingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingPresetController : ControllerBase
    {
        private IGetAllTrainingPresets _getAllTrainingPresets;
        private IGetTrainingPreset _getTrainingPreset;
        private IPostTrainingPreset _postTrainingPreset;
        private IUpdateTrainingPreset _updateTrainingPreset;
        private IDeleteTrainingPreset _deleteTrainingPreset;

        public TrainingPresetController(
            IGetTrainingPreset getTrainingPreset, 
            IPostTrainingPreset postTrainingPreset, 
            IUpdateTrainingPreset updateTrainingPreset, 
            IDeleteTrainingPreset deleteTrainingPreset
            )
        {
            _getTrainingPreset = getTrainingPreset;
            _postTrainingPreset = postTrainingPreset;
            _updateTrainingPreset = updateTrainingPreset;
            _deleteTrainingPreset = deleteTrainingPreset;

        }

        // GET: api/<TrainingPresetController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<TrainingPresetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<TrainingPresetController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<TrainingPresetController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<TrainingPresetController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
