using InimcoExercise.Api.Models;

namespace InimcoExercise.Api.Repositories
{
    public interface IPersonRepository
    {
        void Save(Person person);
        List<Person> GetAll();
    }
}