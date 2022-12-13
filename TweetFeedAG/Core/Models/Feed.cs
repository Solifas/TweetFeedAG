using System;
namespace TweetFeedAG.Core.Models;

public class Feed
{
	public string User { get; set; }
	public List<Tweet> Tweets { get; set; }
}