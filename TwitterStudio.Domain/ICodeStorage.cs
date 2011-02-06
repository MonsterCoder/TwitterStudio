// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICodeStorage.cs" company="">
//   
// </copyright>
// <summary>
//   Interface for code storage
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TwitterStudio.Domain
{
    /// <summary>
    /// Interface for code storage
    /// </summary>
    public interface ICodeStorage
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