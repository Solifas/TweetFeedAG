using System;
namespace TweetFeedAG.Core.Models
{
	public class User
	{
		public string Name { get; set; }
		public IEnumerable<string> Following { get; set; }
	}
}

