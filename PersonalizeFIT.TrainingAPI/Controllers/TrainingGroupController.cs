using Microsoft.AspNetCore.Mvc;
using TrainingAPI.Business.Exceptions;
using TrainingAPI.Business.TrainingGroup;
using TrainingAPI.Entity.Models.TrainingGroup;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalizeFIT.TrainingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingGroupController : ControllerBase
    {
        private IGetAllTrainingGroups _getAllTrainingGroups;
        private IGetTrainingGroup _getTrainingGroup;
        private IPostTrainingGroup _postTrainingGroup;
        private IUpdateTrainingGroup _updateTrainingGroup;
        private IDeleteTrainingGroup _deleteTrainingGroup;

        public TrainingGroupController(
            IGetTrainingGroup getTrainingGroup,
            IGetAllTrainingGroups getAllTrainingGroups,
            IPostTrainingGroup postTrainingGroup,
            IUpdateTrainingGroup updateTrainingGroup,
            IDeleteTrainingGroup deleteTrainingGroup
            )
        {
            _getAllTrainingGroups = getAllTrainingGroups;
            _getTrainingGroup = getTrainingGroup;
            _postTrainingGroup = postTrainingGroup;
            _updateTrainingGroup = updateTrainingGroup;
            _deleteTrainingGroup = deleteTrainingGroup;

        }

        // GET: api/<TrainingGroupController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var trainingGroups = await _getAllTrainingGroups.GetAllTrainingGroupsAsync();
                return Ok(trainingGroups);
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
                var trainingGroup = await _getTrainingGroup.GetTrainingGroupAsync(id);
                return Ok(trainingGroup);
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
        public async Task<IActionResult> Post([FromBody] PostTrainingGroupRequest request)
        {
            try
            {
                var message = await _postTrainingGroup.PostTrainingGroupAsync(request);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TrainingPresetController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTrainingGroupRequest request)
        {
            try
            {
                var message = await _updateTrainingGroup.UpdateTrainingGroupAsync(id, request);
                return Ok(message);
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
                var trainingGroup = await _deleteTrainingGroup.DeleteTrainingGroupAsync(id);
                return Ok(trainingGroup);
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
