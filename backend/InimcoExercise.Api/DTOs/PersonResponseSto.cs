using InimcoExercise.Api.Models;

namespace InimcoExercise.Api.DTOs
{
    public class PersonResponseDto
    {
        public int VowelCount { get; set; }
        public int ConsonantCount { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ReversedFullName { get; set; } = string.Empty;
        public Person Person { get; set; } = null!;
    }
}