using System;
using System.ComponentModel.DataAnnotations;

namespace TweetFeedAG.Core.Models
{
	public class Tweets
	{
		public string UserName { get; set; }
		[MaxLength(140)]
		public string Tweet { get; set; }
	}
}
