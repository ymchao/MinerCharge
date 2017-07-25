using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace MinerLampMangement.HttpClient
{
    public class HttpClient
    {
        private const string Urlhead = "http://localhost:8080";

        /// <summary>
        /// GET请求与获取结果方法
        /// </summary>
        /// <param name="urlend"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string HttpGet(string urlend, string contentType)
        {
            try
            {
                var url = Urlhead + urlend;
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = contentType;
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string result = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return result;
            }
            catch (Exception)
            {
                MessageBox.Show("服务器连接异常，请稍后重试！");
                return null;
            }
        }

        /// <summary>
        /// POST请求及获取回复方法
        /// </summary>
        /// <param name="urlend"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string HttpPost(string urlend, string postDataStr)
        {
            try
            {
                var url = Urlhead + urlend;
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.UTF8);
                writer.Write(postDataStr);
                writer.Flush();
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                return retString;
            }
            catch (Exception)
            {
                MessageBox.Show("服务器连接异常，请稍后重试！");
                return null;
            }
        }
    }
}