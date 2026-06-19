using InimcoExercise.Api.DTOs;
using InimcoExercise.Api.Models;
using InimcoExercise.Api.Repositories;

namespace InimcoExercise.Api.Services
{
    /// <summary>
    /// Bevat de business logic van de oefening: tellen van klinkers/medeklinkers,
    /// het omkeren van de volledige naam, en het bewaren van de ingevoerde persoon.
    /// Is afhankelijk van IPersonRepository (Dependency Injection) zodat de opslagvorm
    /// later vervangen kan worden (bv. database) zonder deze klasse aan te passen.
    /// </summary>
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
            // Vowels/consonants worden geteld op voornaam+achternaam ZONDER spatie,
            // conform het voorbeeld in de opdracht (John Doe -> 3 vowels, 4 consonants).
            var nameOnly = request.FirstName + request.LastName;

            // De reverse gebeurt op de volledige naam MET spatie, als één string,
            // zodat "John Doe" -> "eoD nhoJ" (en niet de twee namen apart omgedraaid).
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

            // Bewaren gebeurt hier in de service (niet in de controller), zodat de
            // controller dom blijft en alle logica testbaar is zonder HTTP-laag.
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

        /// <summary>Telt klinkers, case-insensitive, negeert niet-letters.</summary>
        private static int CountVowels(string input) =>
            input.ToLower().Count(c => Vowels.Contains(c));

        /// <summary>Telt medeklinkers: een letter die geen klinker is.</summary>
        private static int CountConsonants(string input) =>
            input.ToLower().Count(c => char.IsLetter(c) && !Vowels.Contains(c));

        private static string Reverse(string input) =>
            new string(input.Reverse().ToArray());
    }
}