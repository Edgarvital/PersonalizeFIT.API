using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using UserAPI.Business.TrainerHasStudent;
using UserAPI.Connectors.ExternalServices.Exceptions;
using UserAPI.Entity.Models;

namespace PersonalizeFIT.UserAPI.Controller
{
    [Route("api/trainer-management")]
    [ApiController]
    public class TrainerHasStudentController : ControllerBase
    {
        private IGetTrainerHasStudents _getTrainerHasStudents;
        private IPostTrainerHasStudents _postTrainerHasStudents;
        public TrainerHasStudentController(IGetTrainerHasStudents getTrainerHasStudents, IPostTrainerHasStudents postTrainerHasStudents)
        {
            _getTrainerHasStudents = getTrainerHasStudents;
            _postTrainerHasStudents = postTrainerHasStudents;
        }


        [HttpGet("trainerHasStudents")]
        [Authorize(Policy = "Trainer")]
        public async Task<IActionResult> GetAll()
        {
            string user_id = await GetAuthUserId();
            try
            {
                var response = await _getTrainerHasStudents.GetAllTrainerStudents(user_id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("trainerHasStudents")]
        [Authorize(Policy = "Trainer")]
        public async Task<IActionResult> Create([FromBody] PostTrainerHasStudentsRequest request)
        {
            try
            {
                var response = await _postTrainerHasStudents.CreateTrainerHasStudents(request);
                return Ok(response);
            }
            catch (HttpErrorResponseException ex)
            {
                HttpResponseMessage exceptionResponse = ex.Response;
                HttpStatusCode statusCode = exceptionResponse.StatusCode;

                if (statusCode == HttpStatusCode.NotFound)
                {
                    return NotFound("Treinador ou Aluno não encontrado.");
                }
                else
                {
                    return BadRequest("Erro ao criar TrainerHasStudents: " + ex.Message);
                }
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao criar TrainerHasStudents: " + ex.Message);
            }
        }


        private async Task<string> GetAuthUserId()
        {
            var authenticationContext = await HttpContext.AuthenticateAsync();

            if (authenticationContext.Succeeded)
            {
                // Encontre a reivindicação "sub" (subject) no token
                var subjectClaim = authenticationContext.Principal.FindFirst(ClaimTypes.NameIdentifier);

                if (subjectClaim != null)
                {
                    // O valor da reivindicação "sub" é o ID do usuário autenticado
                    string userId = subjectClaim.Value;

                    return userId;
                }
            }

            throw new Exception("User not found");
        }


        /*        
        private readonly IPostOnboarding _postOnboarding;
        private readonly IGetTransaction _getTransaction;
        private readonly IPostCallback _postCallback;
        private readonly IGetReasons _getReasons;
        public CafController(IPostOnboarding postOnboarding, 
            IGetTransaction getTransaction,
            IPostCallback postCallback,
            IGetReasons getReasons)
        {
            _postOnboarding = postOnboarding;
            _getTransaction = getTransaction;
            _postCallback = postCallback;
            _getReasons = getReasons;
        }


        [HttpPost("create-onboarding")]
        public async Task<IActionResult> PostOnboarding([FromBody] PostOnboardingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _postOnboarding.CreateOnboarding(request, cancellationToken);

                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [Authorization(Roles = "employee.master")]
        [HttpGet("get-transaction")]
        public async Task<IActionResult> GetTransaction(string transactionId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _getTransaction.Transaction(transactionId, cancellationToken);

                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [Authorization(Roles = "employee.master")]
        [HttpGet("{projectId}/reasons")]
        public async Task<IActionResult> GetReasons(Guid projectId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _getReasons.Reasons(projectId, cancellationToken);

                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("callback")]
        public async Task<IActionResult> PostCallback([FromBody] PostCallbackRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _postCallback.Callback(request, cancellationToken);

                return Ok("OK");
            }
            catch (ValidationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
        */

    }
}
