using InimcoExercise.Api.DTOs;
using InimcoExercise.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InimcoExercise.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonAnalysisService _analysisService;

        public PersonController(IPersonAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }
        
    /// <summary>
    /// Ontvangt persoonsgegevens, valideert via DataAnnotations op het DTO,
    /// en geeft de analyse (vowels/consonants/reverse/JSON) terug.
    /// Validatiefouten resulteren automatisch in een 400 Bad Request via ModelState.
    /// </summary>
    /// 
        [HttpPost]
        public ActionResult<PersonResponseDto> Post([FromBody] PersonRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _analysisService.Analyze(request);
            return Ok(result);
        }
    }
}