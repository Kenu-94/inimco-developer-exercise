using InimcoExercise.Api.DTOs;

namespace InimcoExercise.Api.Services
{
    public interface IPersonAnalysisService
    {
        PersonResponseDto Analyze(PersonRequestDto request);
    }
}