using System;
using TweetFeedAG.Core.CustomException;
using TweetFeedAG.Core.Interfaces;
using TweetFeedAG.Core.Models;
using static System.Net.Mime.MediaTypeNames;

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

        public List<Feed>? DisplayTweets(string userFilePath, string tweetFilePath)
        {
            var userFeed = new List<Feed>();
            var stringTweets = _fileService.ReadFile(tweetFilePath).ToList();
            var userString = _fileService.ReadFile(userFilePath).ToList();

            var tweets = _tweeterFeedSerializer.DeserializeTweets(stringTweets);
            if (tweets == null || tweets.Count == 0)
            {
                return null;
            }
            var users = _tweeterFeedSerializer.DeserializeUsers(userString);
            if (users == null || users.Count == 0)
            {
                return null;
            }

            foreach (var user in users)
            {
                var allTweets = new List<Tweet>();

                if (user.Following == null)
                {
                    userFeed.Add(new Feed
                    {
                        User = user.Name,
                        Tweets = new List<Tweet>()
                    });
                    continue;
                }
                foreach (var followingUserName in user.Following)
                {
                    var userTweets = tweets.Where(x => x.UserName.Trim() == followingUserName);
                    foreach (var tweet in userTweets)
                    {
                        allTweets.Add(tweet);
                    }
                }
                var sortedTweets = allTweets.OrderBy(x => x.Position).ToList();

                userFeed.Add(new Feed
                {
                    User = user.Name,
                    Tweets = sortedTweets
                });
            }

            return userFeed;
        }
    }
}


