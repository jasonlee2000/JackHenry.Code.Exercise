using System.Threading.Tasks;

namespace JackHenry.CodeExercise.App.Services.Interfaces.Counters
{
    public interface ICounter
    {
        Task Count(string input);
    }
}
