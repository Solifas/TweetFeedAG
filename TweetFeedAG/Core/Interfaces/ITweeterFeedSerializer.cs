using System;
using TweetFeedAG.Core.Models;

namespace TweetFeedAG.Core.Interfaces
{
	public interface ITweeterFeedSerializer
	{
		public List<Tweets> GetTweets(string[] stringTweets);
		public List<User> GetUser(string[] userFollower);
	}
}

