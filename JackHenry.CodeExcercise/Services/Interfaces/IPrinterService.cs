namespace JackHenry.CodeExercise.App.Services.Interfaces
{
    using System.Threading.Tasks;

    using Models;

    public interface IPrinterService
    {
        Task PrintTotalTweets();

        Task PrintAverageTweets(Tweet tweet);

        Task PrintTopEmoji();

        Task PrintPercentOfTweetsContainsEmoji();

        Task PrintTopHashTag();

        Task PrintPercentOfTweetsContainsUrl();

        Task PrintPercentOfTweetsContainsPhotoUrl();

        Task PrintTopUrlDomain();
    }
}
