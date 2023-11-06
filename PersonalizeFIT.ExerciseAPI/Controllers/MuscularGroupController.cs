using ExerciseAPI.Business.Exceptions;
using ExerciseAPI.Business.MuscularGroup;
using ExerciseAPI.Entity.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalizeFIT.ExerciseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuscularGroupController : ControllerBase
    {

        private IPostMuscularGroup _postMuscularGroup;
        private IGetAllMuscularGroups _getAllMuscularGroups;
        private IGetMuscularGroup _getMuscularGroup;

        public MuscularGroupController(IPostMuscularGroup postMuscularGroup, IGetAllMuscularGroups getAllMuscularGroups, IGetMuscularGroup getMuscularGroup)
        {
            _postMuscularGroup = postMuscularGroup;
            _getAllMuscularGroups = getAllMuscularGroups;
            _getMuscularGroup = getMuscularGroup;
        }

        // GET: api/<MuscularGroupController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var muscularGroups = await _getAllMuscularGroups.GetAllMuscularGroupsAsync();
                return Ok(muscularGroups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<MuscularGroupController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var muscularGroup = await _getMuscularGroup.GetMuscularGroupAsync(id);
                return Ok(muscularGroup);
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

        // POST api/<MuscularGroupController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostMuscularGroupRequest request)
        {
            try
            {
                var message = await _postMuscularGroup.CreateMuscularGroupAsync(request);
                return Ok(message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<MuscularGroupController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MuscularGroupController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
