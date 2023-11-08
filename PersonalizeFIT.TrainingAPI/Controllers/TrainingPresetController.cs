using Microsoft.AspNetCore.Mvc;
using TrainingAPI.Business.Exceptions;
using TrainingAPI.Business.TrainingGroup;
using TrainingAPI.Entity.Models.TrainingPreset;

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
            try
            {
                var TrainingPresets = await _getAllTrainingPresets.GetAllTrainingPresetsAsync();
                return Ok(TrainingPresets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TrainingPresetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var TrainingPreset = await _getTrainingPreset.GetTrainingPresetAsync(id);
                return Ok(TrainingPreset);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<TrainingPresetController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostTrainingPresetRequest request)
        {
            try
            {
                var message = await _postTrainingPreset.PostTrainingPresetAsync(request);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TrainingPresetController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTrainingPresetRequest request)
        {
            try
            {
                var Message = await _updateTrainingPreset.UpdateTrainingPresetAsync(id, request);
                return Ok(Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TrainingPresetController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var TrainingPreset = await _deleteTrainingPreset.DeleteTrainingPresetAsync(id);
                return Ok(TrainingPreset);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
