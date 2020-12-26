namespace JackHenry.CodeExercise.App.Services.Interfaces
{
    using System.Threading.Tasks;

    using Models;

    public interface ICounterService
    {
        Task Count(Tweet tweet);
    }
}
