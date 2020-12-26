using System.Threading.Tasks;

namespace JackHenry.CodeExercise.App.Services.Interfaces.Counters
{
    public interface IEmojiCounter : ICounter
    {
        Task<string> GetUniCode(string input);
    }
}
