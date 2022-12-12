using TweetFeedAG.Core.Interfaces;

namespace TweetFeedAG.Infrastructure;

public class FileService : IFileService
{
    public string[] ReadFile(string filePath)
    {
        return File.ReadAllLines(filePath);
    }
}

