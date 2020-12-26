using System.Collections.Generic;
using System.Threading.Tasks;

namespace JackHenry.CodeExercise.App.Services.Interfaces.Matchers
{
    public interface IMatcher
    {
        Task<List<string>> Match(string input);
    }
}
