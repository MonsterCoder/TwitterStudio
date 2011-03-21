using System;

namespace Company.TwitterStudio.Services
{
    /// <summary>
    /// Interface for twitter service
    /// </summary>
    public interface ITwitterService
    {
        /// <summary>
        /// Gets or sets the access Pin
        /// </summary>
        string AccessPin { get; set; }

        /// <summary>
        /// Gets or sets the maximum msg length
        /// </summary>
        int MaximumMsgLength { get; set; }

        /// <summary>
        /// Updates a message
        /// </summary>
        /// <param name="link">link to code past</param>
        /// <param name="updateCallback">logging method</param>
        /// <returns>if the update success</returns>
        bool Update(string link, Action<string> updateCallback);
    }
}