using System;
using TweetFeedAG.Core.Interfaces;
using TweetFeedAG.Core.Models;

namespace TweetFeedAG.Core
{
	public class TweetFeedEngine
	{
		private readonly ITweeterFeedSerializer _tweeterFeedSerializer;
		private readonly IFileService _fileService;

        public TweetFeedEngine(ITweeterFeedSerializer tweeterFeedSerializer, IFileService fileService)
        {
            _tweeterFeedSerializer = tweeterFeedSerializer;
            _fileService = fileService;
        }

        public void UserTweetsGenerator()
		{
			var stringTweets = _fileService.ReadFile("tweet.txt");
			var userString = _fileService.ReadFile("user.txt");

            var tweets = _tweeterFeedSerializer.GetTweets(stringTweets);
			var users = _tweeterFeedSerializer.GetUser(userString);

			var tweetViewer = new List<Tweets>();

			foreach (var user in users.OrderBy(usr => usr.Name))
			{
                Console.WriteLine(user.Name);

				if (user.Following == null)
				{
					continue;
				}
                foreach (var following in user.Following)
				{
					var myTweets = tweets.Where(x => x.UserName.Trim() == following);
					foreach (var tweet in myTweets)
					{
						Console.WriteLine($"\t @{tweet.UserName}: {tweet.Tweet}");
					}
				}
			}
        }
	}
}

