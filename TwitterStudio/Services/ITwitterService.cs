using System;

namespace Company.TwitterStudio.Services
{
    public interface ITwitterService
    {
        bool Update(string link, Action<string> logTweet);
    }
}