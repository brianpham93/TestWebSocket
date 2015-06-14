using GSEP_DLL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GSEP_DLL.Connectors
{
    public static class AuthenticationConnector
    {
        /// <summary>
        /// Connect to WebAPI to login the system
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>token: An access key to access other functions 
        /// without sending username/password again</returns>
        public static string Login(string username, string password)
        {
            var request = (HttpWebRequest)WebRequest.Create(Constant.ApiURL + "token");

            var postData = "grant_type=password";
            postData += "&username=" + username;
            postData += "&password=" + password;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //GET TOKEN FROM JSON
            var jsonData = (JObject)JsonConvert.DeserializeObject(responseString);
            string token = jsonData["access_token"].Value<string>();
            return token;
        }
        public static String Logout(String token)
        {
            var request = WebRequest.Create(Constant.ApiURL + "api/Account/Logout");
            request.Method = "POST";
            request.Headers["Authorization"] = "Bearer " + token;
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (responseString == null || responseString == "")
            {
                return "OK";
            }
            return responseString;
        }
        public static String ChangePassword(String username, SocialUser account) { return null; }
        public static String ResetPassword(String username) { return null; }
        public static String Register(RegisterBindingModel account)
        {

            var request = (HttpWebRequest)WebRequest.Create(Constant.ApiURL + "api/Account/Register");

            var postData = "Email=" + account.Email;
            postData += "&Username=" + account.Username;
            postData += "&Password=" + account.Password;
            postData += "&ConfirmPassword=" + account.ConfirmPassword;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }
    }
}
