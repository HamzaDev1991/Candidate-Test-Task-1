using Candidate_Test_Task.Abstraction;
using Candidate_Test_Task.Contracts.Requests;
using Candidate_Test_Task.Services;

namespace Candidate_Test_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController(ICandidateServices candidateServices) : ControllerBase
    {

        private readonly ICandidateServices _candidateServices = candidateServices;

       

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateReequest reequest)
        {
            try
            {
                var currentCandate = await _candidateServices.Get(reequest.Email);

                if (currentCandate is null)
                {
                    var result = await _candidateServices.AddAsync(reequest.Email, reequest.MapToCandidate());
                    return result.IsSuccess ? Ok(reequest) : result.ToProblem(StatusCodes.Status400BadRequest);
                }
                else
                {
                    var result = await _candidateServices.UpdateAsync(reequest.Email, reequest);
                    return result.IsSuccess ? NoContent() : result.ToProblem(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
            
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
