// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasteBinStorage.cs" company="">
//   
// </copyright>
// <summary>
//   Passte bin code storage
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Web;
using Company.TwitterStudio.Services;

namespace PasteBinPlugin
{
    /// <summary>
    /// Passte bin code storage
    /// </summary>
    [Export(typeof(IPasteService))]
    public class PasteBinPlugin : IPasteService  
    {
        /// <summary>
        /// Url to paste bin api
        /// </summary>
        private const string Url = @"http://pastebin.com/api_public.php";       

        /// <summary>
        /// Send message to storage
        /// </summary>
        /// <param name="msg">
        /// The msg to sent
        /// </param>
        /// <returns>
        /// Link to the hosting page
        /// </returns>
        public string Upload(string msg)
        {
            try
            {
                var content = "paste_code=" + HttpUtility.UrlEncode(msg);
                var buffer = System.Text.Encoding.ASCII.GetBytes(content);

                var request = (HttpWebRequest)WebRequest.Create(Url); 
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = buffer.Length;
                request.UserAgent = "TwitterStudio2011";

                using (var response = request.GetRequestStream())
                {
                    response.Write(buffer, 0, buffer.Length);
                }

                using (var res = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = new StreamReader(res.GetResponseStream()))
                    {
                        return stream.ReadToEnd().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
