using System;
using TweetFeedAG.Core.Models;

namespace TweetFeedAG.Core.Interfaces
{
    public interface ITweeterFeedSerializer
    {
        public List<Tweet> GetTweets(List<string> stringTweets);
        public List<User> GetUser(string[] userFollower);
    }
}

