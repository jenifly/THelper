using Codeplex.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web;

namespace THelper.translate
{
    public class BaiduTranslate
    {
        #region 百度Baidu翻译transapi，不需要sign和token值
        //内容长度
        public string Content_Length(string text, string fromlang, string tolang)
        {
            return "from=" + fromlang + "&to=" + tolang + "&query=" + HttpUtility.UrlEncode(text).Replace("+", "%20");
        }

        public string 百度Baidu(string text, string fromLanguage, string toLanguage)
        {
            CookieContainer cc = new CookieContainer();
            string baiduTransUrl = "http://fanyi.baidu.com/transapi";
            var ResultHtml = GetBaiduHtml(baiduTransUrl, cc, "http://fanyi.baidu.com/", Content_Length(text, fromLanguage, toLanguage));
            if(ResultHtml.Contains("\"error\":8"))
                return "待翻译内容是否为空？<br/>返回结果错误，请重试。";
            if (ResultHtml.Contains("\"error\":999"))
                return "原文语言和目标语言是否相同？<br/>返回结果错误，请重试。";
            var json = DynamicJson.Parse(ResultHtml);
            ResultHtml = string.Empty;
            foreach (var data in json.data)
            {
                ResultHtml += data.dst + "<br/>";
            }
            return ResultHtml.Substring(0, ResultHtml.Length - 6);
        }

        public string GetBaiduHtml(string url, CookieContainer cookie, string refer, string content_length)
        {
            var html = "";
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.CookieContainer = cookie;
            webRequest.Referer = refer;
            webRequest.Timeout = 10000;
            webRequest.Accept = "*/*";
            webRequest.KeepAlive = false;
            webRequest.Headers.Add("Accept-Language: zh-CN;q=0.8,en-US;q=0.6,en;q=0.4");
            webRequest.Headers.Add("Accept-Encoding: gzip,deflate");
            webRequest.Headers.Add("Accept-Charset: utf-8");
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko)";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.ProtocolVersion = new Version(1, 1);

            byte[] con_len_byte = Encoding.UTF8.GetBytes(content_length);
            webRequest.ContentLength = con_len_byte.Length;
            Stream requsetSteam = webRequest.GetRequestStream();
            requsetSteam.Write(con_len_byte, 0, con_len_byte.Length);
            requsetSteam.Close();

            try
            {
                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    Stream st = webResponse.GetResponseStream();
                    if (webResponse.ContentEncoding.ToLower().Contains("gzip"))
                        st = new GZipStream(st, CompressionMode.Decompress);
                    using (var reader = new StreamReader(st, Encoding.UTF8))
                    {

                        html = reader.ReadToEnd();
                        reader.Close();
                        webResponse.Close();
                    }
                }

                return html;
            }
            catch
            {
                return null;
            }
            finally
            {
                if(webRequest != null)
                {
                    webRequest.Abort();
                }
            }
        }
        #endregion
    }
}