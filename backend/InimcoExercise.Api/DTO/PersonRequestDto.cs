using System.ComponentModel.DataAnnotations;

namespace InimcoExercise.Api.DTOs
{
    public class SocialAccountDto
    {
        [Required(ErrorMessage = "Type is verplicht (bv. Twitter, LinkedIn)")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is verplicht")]
        public string Address { get; set; } = string.Empty;
    }

    public class PersonRequestDto
    {
        [Required(ErrorMessage = "Voornaam is verplicht")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Voornaam mag enkel letters bevatten")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Achternaam is verplicht")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Achternaam mag enkel letters bevatten")]
        public string LastName { get; set; } = string.Empty;

        public List<string> SocialSkills { get; set; } = new();

        public List<SocialAccountDto> SocialAccounts { get; set; } = new();
    }
}