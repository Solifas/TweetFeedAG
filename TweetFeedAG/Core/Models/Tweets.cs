using System;
using System.ComponentModel.DataAnnotations;

namespace TweetFeedAG.Core.Models
{
	public class Tweet
	{
		public string UserName { get; set; }
		[MaxLength(140)]
		public string TweetText { get; set; }
		public int Position { get; set; }
	}
}
