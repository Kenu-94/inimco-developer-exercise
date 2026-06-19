using InimcoExercise.Api.DTOs;
using InimcoExercise.Api.Models;
using InimcoExercise.Api.Repositories;

namespace InimcoExercise.Api.Services
{
    public class PersonAnalysisService : IPersonAnalysisService
    {
        private static readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };
        private readonly IPersonRepository _repository;

        public PersonAnalysisService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public PersonResponseDto Analyze(PersonRequestDto request)
        {
            var nameOnly = request.FirstName + request.LastName; // voor vowel/consonant telling
            var fullName = $"{request.FirstName} {request.LastName}";

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                SocialSkills = request.SocialSkills,
                SocialAccounts = request.SocialAccounts
                    .Select(a => new SocialAccount { Type = a.Type, Address = a.Address })
                    .ToList()
            };

            _repository.Save(person);

            return new PersonResponseDto
            {
                VowelCount = CountVowels(nameOnly),
                ConsonantCount = CountConsonants(nameOnly),
                FullName = fullName,
                ReversedFullName = Reverse(fullName),
                Person = person
            };
        }

        private static int CountVowels(string input) =>
            input.ToLower().Count(c => Vowels.Contains(c));

        private static int CountConsonants(string input) =>
            input.ToLower().Count(c => char.IsLetter(c) && !Vowels.Contains(c));

        private static string Reverse(string input) =>
            new string(input.Reverse().ToArray());
    }
}