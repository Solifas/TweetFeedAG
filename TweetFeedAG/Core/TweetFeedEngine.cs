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

        public void DisplayTweets(string userFilePath, string tweetFilePath)
        {
                var stringTweets = _fileService.ReadFile(tweetFilePath).ToList();
                var userString = _fileService.ReadFile(userFilePath);

                var tweets = _tweeterFeedSerializer.GetTweets(stringTweets);
                var users = _tweeterFeedSerializer.GetUser(userString);

                foreach (var user in users)
                {
                    var allTweets = new List<Tweet>();

                    Console.WriteLine(user.Name);

                    if (user.Following == null)
                    {
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
                    foreach (var sortedTweet in sortedTweets)
                    {
                        Console.WriteLine($"\t @{sortedTweet.UserName}: {sortedTweet.TweetText} {sortedTweet.Position}");
                    }
                }
            }
        }
    }


