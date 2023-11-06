using ExerciseAPI.Business.Exceptions;
using ExerciseAPI.Business.Exercise;
using ExerciseAPI.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalizeFIT.ExerciseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {

        private IPostExercise _postExercise;
        private IGetExercise _getExercise;
        private IGetAllExercises _getAllExercises;

        public ExerciseController(IPostExercise postExercise, IGetExercise getExercise, IGetAllExercises getAllExercises)
        {
            _postExercise = postExercise;
            _getExercise = getExercise;
            _getAllExercises = getAllExercises;

        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var exercises = await _getAllExercises.GetAllExercisesAsync();
                return Ok(exercises);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var exercise = await _getExercise.GetExerciseAsync(id);
                return Ok(exercise);
            }
            catch (NotFoundException ex){
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostExerciseRequest request)
        {
            try
            {
                var message = await _postExercise.CreateExerciseAsync(request);
                return Ok(message);
            } catch(ValidationException ex)
            {
                return UnprocessableEntity(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
