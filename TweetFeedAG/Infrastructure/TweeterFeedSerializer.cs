using System;
using TweetFeedAG.Core.CustomException;
using TweetFeedAG.Core.Interfaces;
using TweetFeedAG.Core.Models;

namespace TweetFeedAG.Infrastructure
{
    public class TweeterFeedSerializer : ITweeterFeedSerializer
    {
        public List<Tweet> GetTweets(List<string> stringTweets)
        {
            try
            {
                var tweets = new List<Tweet>();
                int tweetPosition = 0;

                foreach (var tweet in stringTweets)
                {
                    var userTweet = tweet.Split(">");
                    tweets.Add(new Tweet
                    {
                        UserName = userTweet[0].Trim(),
                        TweetText = userTweet[1],
                        Position = tweetPosition++
                    });
                }

                return tweets.OrderBy(x => x.Position).ToList();
            }
            catch (TwitterFeedException ex)
            {
                throw new TwitterFeedException("Something went wrong deserializing tweets", ex);
            }
        }

        public List<User> GetUser(string[] usersFollower)
        {
            try
            {
                var users = new List<User>();

                foreach (var user in usersFollower)
                {
                    var userLine = user.Split(" follows ");
                    var userName = userLine[0].Trim();
                    var following = userLine[1].Split(',');

                    if (users.FirstOrDefault(x => x.Name.Trim() == userName) != null)
                    {
                        if (following.Any())
                        {
                            _ = users.Where(x => x.Name.Trim() == userName).FirstOrDefault(x =>
                            {
                                if (x.Following == null)
                                {
                                    x.Following = following.Append(userName);
                                }
                                else
                                {
                                    x.Following = x.Following.Union(following);
                                }
                                return true;
                            });
                        }
                    }
                    else
                    {
                        users.Add(new User
                        {
                            Name = userName,
                            Following = following.Append(userName)
                        });
                    }

                    //logic to add the people the user is following
                    if (following.Any())
                    {
                        foreach (var follow in following)
                        {
                            if (users.Any(x => x.Name == follow.Trim()))
                            {
                                users.Add(new User
                                {
                                    Name = follow.Trim(),
                                });
                            }
                        }
                    }
                }

                return users.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong deserializing the user file, please ensure that the file is in a correct format", ex);
            }
        }
    }
}

