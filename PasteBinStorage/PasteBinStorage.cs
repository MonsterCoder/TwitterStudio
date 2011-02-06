using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Web;
using TwitterStudio.Domain;

namespace PasteBinStorage
{
    /// <summary>
    /// Passte bin code storage
    /// </summary>
    [Export(typeof(ICodeStorage))]
    public class PasteBisnStorage : ICodeStorage  
    {
        /// <summary>
        /// Url to paste bin api
        /// </summary>
        private const string Url = @"http://davux.pastebin.com/pastebin.php";       

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
                var content = "paste_code=" + HttpUtility.HtmlEncode(msg);
                var buffer = System.Text.Encoding.ASCII.GetBytes(content);

                var request = (HttpWebRequest)WebRequest.Create(Url); 
                request.Method = "POST";
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


        public static string postRequest(string url, string postData)
        {
            var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;

            if (webRequest != null)
            {
                webRequest.Method = "Post";
                webRequest.UserAgent = "TwitterStudio2011";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                using (var requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(postData);
                }
  
                return WebResponseGet(webRequest);
            }

            return string.Empty;
        }

        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        private static string WebResponseGet(WebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = string.Empty;
            var donext = false;
            try
            {
                var str = (HttpWebResponse)webRequest.GetResponse();

                if (str != null)
                {
                    var stream = str.GetResponseStream();
                    if (stream != null) responseReader = new StreamReader(stream);
                }

                if (responseReader != null) responseData = responseReader.ReadToEnd();
                else
                    donext = true;
            }
            catch (WebException)
            {
                responseData = "error";
            }
            finally
            {
                if (donext)
                {
                    webRequest.GetResponse().GetResponseStream().Close();
                    responseReader.Close();
                }
            }
            return responseData;
        }


    }
}
