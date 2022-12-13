using TweetFeedAG.Core;
using TweetFeedAG.Core.Interfaces;
using TweetFeedAG.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using TweetFeedAG.Core.Models;
using TweetFeedAG.Core.CustomException;

namespace TweetFeedAG.UI;
class Program
{
    static void Main(string[] args)
    {
        var fileService = new FileService();
        var configuration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile($"appsettings.json")
                                        .Build();
        try
        {
            var twitterFeedEngine = new TweetFeedEngine(new TweeterFeedSerializer(), fileService);
            twitterFeedEngine.DisplayTweets(userFilePath: configuration["UserFilePath"] ?? "user.txt", tweetFilePath: configuration["TweetPath"] ?? "tweet.txt");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            fileService.WriteFile(configuration["ErrorFile"] ?? "error.txt", $"{ex.Message} \n Source: {JsonSerializer.Serialize(ex.Source)} \n {ex.StackTrace} {ex.InnerException}");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }
}
