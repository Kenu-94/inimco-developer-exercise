namespace InimcoExercise.Api.Models
{
    public class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<string> SocialSkills { get; set; } = new();
        public List<SocialAccount> SocialAccounts { get; set; } = new();
    }
}