using TweetFeedAG.Core.CustomException;
using TweetFeedAG.Core.Interfaces;

namespace TweetFeedAG.Infrastructure;

public class FileService : IFileService
{
    public string[] ReadFile(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (FileNotFoundException fex)
        {
            throw fex;
        }
        catch (Exception ex)
        {
            throw new Exception("There was an error retreiving your file", ex);
        }
    }

    public void WriteFile(string writePath, string content)
    {
        try
        {
            File.WriteAllText(writePath, content);
        }
        catch (Exception ex)
        {
            throw new Exception("There was an error writing to file", ex);
        }
    }
}