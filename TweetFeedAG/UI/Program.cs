using TweetFeedAG.Core;
using TweetFeedAG.Core.Interfaces;
using TweetFeedAG.Infrastructure;

namespace TweetFeedAG.UI;
class Program
{
    static void Main(string[] args)
    {
        var twitterFeedEngine = new TweetFeedEngine(new TweeterFeedSerializer(), new FileService());
        twitterFeedEngine.UserTweetsGenerator();
        Console.ReadLine();
    }
}
