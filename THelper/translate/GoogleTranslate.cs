using Codeplex.Data;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace THelper.translate
{
    public class GoogleTranslate
    {
        public static string 谷歌Google(string text, string fromLanguage, string toLanguage)
        {
            CookieContainer cc = new CookieContainer();
            var tk = new CalculationTK().vq(text, "422388", "3876711001");
            string googleTransUrl = "https://translate.google.cn/translate_a/single?client=t&sl=" + fromLanguage + "&tl=" + toLanguage + "&hl=en&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&ie=UTF-8&oe=UTF-8&otf=1&ssel=0&tsel=0&kc=1&tk=" + tk + "&q=" + HttpUtility.UrlEncode(text);
            var ResultHtml = GetResultHtml(googleTransUrl, cc);
            var json = DynamicJson.Parse(ResultHtml);
            ResultHtml = json[0][0][0];
            if (ResultHtml.Length == 0)
                return "待翻译内容是否为空？<br/>返回结果错误，请重试。";
            return ResultHtml;
        }

        public static string GetResultHtml(string url, CookieContainer cc)
        {
            string html = "";
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.Timeout = 10000;
            webRequest.Headers.Add("X-Requested-With:XMLHttpRequest");
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                    webResponse.Close();
                }
            }
            return html;
        }
    }
}
