using System;
using TweetFeedAG.Core.Interfaces;
using TweetFeedAG.Core.Models;

namespace TweetFeedAG.Infrastructure
{
    public class TweeterFeedSerializer : ITweeterFeedSerializer
    {
        public List<Tweets> GetTweets(string[] stringTweets)
        {
            var tweets = new List<Tweets>();

            foreach (var tweet in stringTweets)
            {
                var kpTweet = tweet.Split(">");
                tweets.Add(new Tweets
                {
                    UserName = kpTweet[0].Trim(),
                    Tweet = kpTweet[1]
                });
            }
            return tweets;
        }

        public List<User> GetUser(string[] usersFollower)
        {
            try
            {
                var users = new List<User>();

                foreach (var user in usersFollower)
                {
                    var userLine = user.Split(" follows ");
                    var following = userLine[1].Split(',');
                    var userName = userLine[0].Trim();

                    if (users.FirstOrDefault(x=>x.Name.Trim() == userName) != null)
                    {
                        if (following.Any())
                        {
                            users.Where(x => x.Name.Trim() == userName).FirstOrDefault(x =>
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

                    // logic to add the people following

                    if (following.Any())
                    {
                        foreach (var follow in following)
                        {
                            if (users.FirstOrDefault(x => x.Name.Trim() == follow.Trim()) == null)
                            {
                                users.Add(new User
                                {
                                    Name = follow.Trim(),
                                });
                            }
                        }
                    }
                }
                
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

