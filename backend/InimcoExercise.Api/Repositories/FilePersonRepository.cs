using System.Text.Json;
using InimcoExercise.Api.Models;

namespace InimcoExercise.Api.Repositories
{

    /// <summary>
    /// File-based implementatie van IPersonRepository: bewaart alle personen als JSON-array
    /// in één bestand. Voldoet aan de opdrachtvereiste van een "base repository" zonder
    /// database-afhankelijkheid. De interface IPersonRepository laat toe dit later te
    /// vervangen door een echte database-implementatie zonder de Service-laag te raken.
    /// </summary>
    public class FilePersonRepository : IPersonRepository
    {
        private readonly string _filePath;

        // _lock voorkomt dat twee gelijktijdige requests het JSON-bestand corrupt
        // schrijven (race condition bij read-modify-write).
        private static readonly object _lock = new();

        public FilePersonRepository(IConfiguration configuration)
        {
            _filePath = configuration["RepositorySettings:FilePath"] ?? "Data/persons.json";

             // (terminal, VS Code debugger, ...) — dit voorkomt dat er meerdere kopieën ontstaan.
            _filePath = Path.Combine(AppContext.BaseDirectory, relativePath);

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