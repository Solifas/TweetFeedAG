using TweetFeedAG.Core.Interfaces;
using TweetFeedAG.Core.Models;
using TweetFeedAG.Infrastructure;

namespace TweetFeedAG.Tests;

public class TweeterFeedSerializerTest
{
    [Fact]
    public void TweeterFeedSerializer_SerializeTweets_NotNull()
    {
        var twitterFeedSerielizer = new Mock<ITweeterFeedSerializer>();
        var fileService = new Mock<IFileService>();
        var tweetsString = new string[]
        {
            "Solifas> Opportunities don't happen, you create them.",
            "Pearse> It is better to fail in originality than to succeed in imitation.",
            "Alan> If you have a procedure with 10 parameters, you probably missed some."
        };

        var twitterFeedSerieliz = new TweeterFeedSerializer();
        var tweetsList = twitterFeedSerieliz.DeserializeTweets(tweetsString.ToList());

        Assert.NotNull(tweetsList);
    }

    [Fact]
    public void TweeterFeedSerializer_SerializeTweets_Empty_File_Returns_EmptyTweets()
    {
        var twitterFeedSerielizer = new Mock<ITweeterFeedSerializer>();
        var fileService = new Mock<IFileService>();
        var tweetsString = new List<string>();

        var twitterFeedSerieliz = new TweeterFeedSerializer();
        var tweetsList = twitterFeedSerieliz.DeserializeTweets(tweetsString);

        Assert.Empty(tweetsList);
    }

    [Fact]
    public void TweeterFeedSerializer_SerializeUser_NotNull()
    {
        var userString = new string[]
        {
            "Ward follows Alan, Solifas",
            "Alan follows Solifas",
        };

        var twitterFeedSerieliz = new TweeterFeedSerializer();
        var tweetsList = twitterFeedSerieliz.DeserializeUsers(userString.ToList());

        Assert.NotNull(tweetsList);
    }

    [Fact]
    public void TweeterFeedSerializer_SerializeUser_Empty_File_Returns_EmptyUser()
    {
        var tweetsString = new List<string>();

        var twitterFeedSerielizer = new TweeterFeedSerializer();
        var userList = twitterFeedSerielizer.DeserializeUsers(tweetsString);

        Assert.Empty(userList);
    }
}