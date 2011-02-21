// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICodeStorage.cs" company="">
//   
// </copyright>
// <summary>
//   Interface for code storage
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Company.TwitterStudio.Services
{
    /// <summary>
    /// Interface for code storage
    /// </summary>
    public interface IPasteService
    {
        /// <summary>
        /// Send message to storage
        /// </summary>
        /// <param name="msg">
        /// The msg to sent
        /// </param>
        /// <returns>
        /// Link to the hosting page
        /// </returns>
        string Upload(string msg);
    }
}