using Codeplex.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace THelper.translate
{
    public class TencentTranslate
    {
        #region 腾讯翻译

        //内容长度
        public string Content_Length(string text, string fromlang, string tolang)
        {
            return "source=" + fromlang + "&target=" + tolang + "&sourceText=" + HttpUtility.UrlEncode(text).Replace("+", "%20") + "&sessionUuid=translate_uuid" + DateTimeOffset.Now.ToString();
        }

        public string CookieStr
        {
            get;
            set;
        }

        public string 腾讯Tencent(string text, string fromLanguage, string toLanguage)
        {
            CookieContainer cc = new CookieContainer();
            string QQTransBaseUrl = "http://fanyi.qq.com/";
            var BaseHtml = TencentGET(QQTransBaseUrl, cc);

            //在TencentGET中正则匹配cookie的fy_guid值
            Regex re1 = new Regex("fy_guid.*?;");
            var fy_guid = re1.Match(CookieStr);

            //在返回的HTML中正则匹配qtv和qtk值
            MatchCollection colls = Regex.Matches(BaseHtml, "(?<=document.cookie = \")(.*?)(?=\";)");
            string qt = "";
            for (int i = 0; i < colls.Count; i++)
            {
                qt += colls[i].Value.ToString() + "; ";
            }

            CookieStr = fy_guid + " " + qt;
            string tencentTransUrl = "http://fanyi.qq.com/api/translate";
            string ResultHtml = TencentPOST(tencentTransUrl, CookieStr, QQTransBaseUrl, Content_Length(text, fromLanguage, toLanguage));
            if(ResultHtml.Contains("errCode\":-9"))
                return "待翻译内容是否为空？<br/>返回结果错误，请重试。";
            var json = DynamicJson.Parse(ResultHtml);
            return json.translate.records[0].targetText;
        }

        public string TencentGET(string url, CookieContainer cookie)
        {
            var html = "";
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.CookieContainer = cookie;
            webRequest.Timeout = 20000;
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Accept-Encoding: gzip,deflate");
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";

            try
            {
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    Stream st = webResponse.GetResponseStream();
                    if (webResponse.ContentEncoding.ToLower().Contains("gzip"))
                        st = new GZipStream(st, CompressionMode.Decompress);
                    using (var reader = new StreamReader(st, Encoding.UTF8))
                    {

                        html = reader.ReadToEnd();
                        reader.Close();
                        CookieStr = webResponse.Headers.Get("Set-Cookie");
                        webResponse.Close();
                    }
                }
                return html;
            }
            catch
            {
                return null;
            }

        }
        public string TencentPOST(string url, string cookiestr, string refer, string content_length)
        {
            var html = "";
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.Referer = refer;
            webRequest.Timeout = 10000;
            webRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            webRequest.Headers.Add("Cookie", CookieStr);
            webRequest.Headers.Add("Origin: http://fanyi.qq.com");
            webRequest.Headers.Add("X-Requested-With: XMLHttpRequest");
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
            webRequest.Headers.Add("Accept-Encoding: gzip,deflate");
            webRequest.Headers.Add("Accept-Language: zh-CN,zh;q=0.9");
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.ProtocolVersion = new Version(1, 1);

            byte[] con_len_byte = Encoding.UTF8.GetBytes(content_length);
            webRequest.ContentLength = con_len_byte.Length;
            Stream requsetSteam = webRequest.GetRequestStream();
            requsetSteam.Write(con_len_byte, 0, con_len_byte.Length);
            requsetSteam.Close();

            try
            {
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
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


        }

        public class Records
        {
            public string SourceText { get; set; }
            public string TargetText { get; set; }
        }
        public class Translate
        {
            public string source { get; set; }
            public string target { get; set; }
            public List<Records> records { get; set; }
        }
        public class TransResult
        {
            public Translate translate
            {
                get;
                set;
            }
        }

        #endregion
    }
}