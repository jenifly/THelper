using Codeplex.Data;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace THelper.translate
{
    public class BingTranslate
    {
        public String 必应Bing(string word)
        {
            string encodedStr = HttpUtility.UrlEncode(word);
            var ResultHtml = GetSource(string.Format("http://xtk.azurewebsites.net/BingDictService.aspx?Word={0}&&Samples=false", encodedStr));
            Console.WriteLine(ResultHtml);
            if (ResultHtml.Contains("An error occurs."))
            {
                return "原文句意不通，请修改后重试。";
            }
            if (ResultHtml.Contains("操作超时"))
            {
                return "必应翻译暂时采用第三方接口，不稳定。<br/>请稍后重试或使用其他翻译。";
            }
            var json = DynamicJson.Parse(ResultHtml);
            return "译文一：<br/>" + json.defs[0].def + "<br/>" + "译文二：<br/>" + json.defs[1].def;
        }

        private static string GetSource(string PageUrl)
        {
            try
            {
                WebRequest request = WebRequest.Create(PageUrl);
                request.Timeout = 5000;
                WebResponse response = request.GetResponse();
                Stream resStream = response.GetResponseStream();
                Encoding enc = Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(resStream, enc);
                string source = sr.ReadToEnd();
                resStream.Close();
                sr.Close();
                return source;
            }
            catch (Exception ex)
            {
                return "操作超时" + ex.Message;
            }

        }
    }
}
