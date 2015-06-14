using GSEP_DLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GSEP_DLL.Connectors
{
    public class HttpCommon
    {
        public T HttpGet<T>(string url, string token)
        {
            HttpWebRequest myHttpWebRequest = null;     //Declare an HTTP-specific implementation of the WebRequest class.
            HttpWebResponse myHttpWebResponse = null;   //Declare an HTTP-specific implementation of the WebResponse class
            myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "text/json; encoding='utf-8'";
            myHttpWebRequest.Headers["Authorization"] = "Bearer " + token;
            //Get Response
            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            var responseString = new StreamReader(myHttpWebResponse.GetResponseStream()).ReadToEnd();
            try
            {
                var data = JsonConvert.DeserializeObject<T>(responseString);
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(responseString);
            }            
        }

        public IEnumerable<T> HttpGetList<T>(string url, string token)
        {
            HttpWebRequest myHttpWebRequest = null;     //Declare an HTTP-specific implementation of the WebRequest class.
            HttpWebResponse myHttpWebResponse = null;   //Declare an HTTP-specific implementation of the WebResponse class
            myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "text/json; encoding='utf-8'";
            myHttpWebRequest.Headers["Authorization"] = "Bearer " + token;
            //Get Response
            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            var responseString = new StreamReader(myHttpWebResponse.GetResponseStream()).ReadToEnd();
            try
            {
                var data = JsonConvert.DeserializeObject<IEnumerable<T>>(responseString);
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(responseString);
            }

        }

        public Boolean HttpDelete(string url, string token)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "DELETE";
            request.Headers["Authorization"] = "Bearer " + token;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            try
            {
                var data = JsonConvert.DeserializeObject<Boolean>(responseString);
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(responseString);
            }
            /*var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Authorization"] = "Bearer " + token;
            var stream = request.GetRequestStream();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            try
            {
                var data = JsonConvert.DeserializeObject<Boolean>(responseString);
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(responseString);
            }*/
        }
        /// <summary>
        /// AnhNT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public T HttpPost<T, V>(string url, V postInfo, string token)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            string postData = "";
            if (postInfo != null)
            {
                int length = typeof(V).GetProperties().Length;
                for (int i = 0; i < length - 1; i++)
                {
                    var param = typeof(V).GetProperties()[i];
                    postData = postData + param.Name + "=" + param.GetValue(postInfo, null) + "&";
                }
                var param2 = typeof(V).GetProperties()[length - 1];
                postData = postData + param2.Name + "=" + param2.GetValue(postInfo, null);
            }
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Headers["Authorization"] = "Bearer " + token;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            try
            {
                var responseData = JsonConvert.DeserializeObject<T>(responseString);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception(responseString);
            }
        }

        public T HttpPost<T>(String url, String token)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            //var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;
            request.Headers["Authorization"] = "Bearer " + token;
            request.ContentLength = 0;
            //var stream = request.GetRequestStream();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            try
            {                
                var responseData = JsonConvert.DeserializeObject<T>(responseString);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception(responseString);
            }
        }


        /// <summary>
        /// AnhNT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="postID"></param>
        /// <param name="editInfo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public T HttpPut<T, V>(string url, V editInfo, string token)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            string putData = "";
            if (editInfo != null)
            {
                int length = typeof(V).GetProperties().Length;
                for (int i = 0; i < length - 1; i++)
                {
                    var param = typeof(V).GetProperties()[i];
                    putData = putData + param.Name + "=" + param.GetValue(editInfo, null) + "&";
                }
                var param2 = typeof(V).GetProperties()[length - 1];
                putData = putData + param2.Name + "=" + param2.GetValue(editInfo, null);
            }
            var data = Encoding.ASCII.GetBytes(putData);

            request.Method = "PUT";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Headers["Authorization"] = "Bearer " + token;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            try
            {                
                var respones = JsonConvert.DeserializeObject<T>(responseString);
                return respones;
            }
            catch (Exception e)
            {
                throw new Exception(responseString);
            }
        }
    }
}
