using System;
using TweetFeedAG.Core.Models;

namespace TweetFeedAG.Core.Interfaces
{
    public interface ITweeterFeedSerializer
    {
        public List<Tweet> DeserializeTweets(List<string> stringTweets);
        public List<User> DeserializeUsers(List<string> userFollower);
    }
}

