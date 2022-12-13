using TweetFeedAG.Core;
using TweetFeedAG.Core.Interfaces;
using TweetFeedAG.Core.Models;
using TweetFeedAG.Infrastructure;

namespace TweetFeedAG.Tests;

public class TweetFeedEngineTest
{
    [Fact]
    public void TweetFeedEngineTweest_Feed_AreEqual()
    {
        var twitterFeedSerielizer = new Mock<ITweeterFeedSerializer>();
        var fileService = new Mock<IFileService>();
        
        var tweets = new List<Tweet>
        {
            new Tweet
            {
                Position = 0,
                UserName = "Solifas",
                TweetText = "Opportunities don't happen, you create them."
            },
            new Tweet
            {
                Position = 1,
                UserName = "Pearse",
                TweetText = "It is better to fail in originality than to succeed in imitation."
            },
            new Tweet
            {
                Position = 2,
                UserName = "Alan",
                TweetText = "If you have a procedure with 10 parameters, you probably missed some."
            },
        };

        var users = new List<User>()
        {
            new User
            {
                Name = "Solifas",
                Following = new string[]{"Pearse"}
            },
            new User
            {
                Name = "Alan",
                Following = new string[]{"Pearse"}
            },
            new User
            {
                Name = "Pearse",
                Following = new string[]{ "Alan"}
            },
        };

        twitterFeedSerielizer.Setup(x => x.DeserializeTweets(It.IsAny<List<string>>())).Returns(tweets);
        twitterFeedSerielizer.Setup(x => x.DeserializeUsers(It.IsAny<List<string>>())).Returns(users);
        var tweetfeedEngine = new TweetFeedEngine(twitterFeedSerielizer.Object,fileService.Object);

        var feeds = tweetfeedEngine.DisplayTweets(It.IsAny<string>(), It.IsAny<string>());

        Assert.Equal(tweets.Count, feeds.Count);
    }
}
