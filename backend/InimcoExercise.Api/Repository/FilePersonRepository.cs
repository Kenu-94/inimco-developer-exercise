using System.Text.Json;
using InimcoExercise.Api.Models;

namespace InimcoExercise.Api.Repositories
{
    public class FilePersonRepository : IPersonRepository
    {
        private readonly string _filePath;
        private static readonly object _lock = new();

        public FilePersonRepository(IConfiguration configuration)
        {
            _filePath = configuration["RepositorySettings:FilePath"] ?? "Data/persons.json";
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public void Save(Person person)
        {
            lock (_lock)
            {
                var persons = GetAll();
                persons.Add(person);
                var json = JsonSerializer.Serialize(persons, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
        }

        public List<Person> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Person>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
        }
    }
}