using System;

namespace Company.TwitterStudio.Services
{
    public interface ITwitterSerive
    {
        bool Update(string link, Action<string> logTweet);
    }
}