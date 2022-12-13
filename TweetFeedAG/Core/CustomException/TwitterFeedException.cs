using System;
namespace TweetFeedAG.Core.CustomException;

[Serializable()]
public class TwitterFeedException : Exception
{
    public TwitterFeedException() { }
    public TwitterFeedException(string message) : base(message) { }
    public TwitterFeedException(string message, System.Exception inner) : base(message, inner) { }

    // Constructor needed for serialization 
    // when exception propagates from a remoting server to the client.
    protected TwitterFeedException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}