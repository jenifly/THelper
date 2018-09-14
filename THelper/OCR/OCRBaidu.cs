using Codeplex.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace THelper.OCR
{
    public class OCRBaidu
    {
        //内容长度
        public static string Content_Length(Image img)
        {
            return "type=commontext&image=" + HttpUtility.UrlEncode(ImgToBase64String(img)) + "&image_url=";
        }

        /// </summary>
        /// <param name="img">待识别的图片</param>
        /// <returns></returns>
        public static string BaiduOCR(Image img)
        {
            CookieContainer cc = new CookieContainer();
            string BaiduOCRBaseUrl = "http://ai.baidu.com/tech/ocr/general";
            BaiduHead(BaiduOCRBaseUrl, cc);

            string baiduTransUrl = "http://ai.baidu.com/aidemo";
            var ResultHtml = GetBaiduHtml(baiduTransUrl, cc, BaiduOCRBaseUrl, Content_Length(img));
            var json = DynamicJson.Parse(ResultHtml);
            ResultHtml = "";
            foreach (var str in json.data.words_result)
            {
                ResultHtml += str.words + "\r\n";
            }
            return ResultHtml;

        }
        public static void BaiduHead(string url, CookieContainer cookie)
        {
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "HEAD";
            webRequest.CookieContainer = cookie;
            webRequest.Timeout = 16000;
            webRequest.Accept = "*/*";
            webRequest.KeepAlive = false;
            webRequest.Headers.Add("Accept-Encoding: gzip,deflate");
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko)";
            webRequest.GetResponse();
            if (webRequest != null)
            {
                webRequest.Abort();
            }
        }

        public static string GetBaiduHtml(string url, CookieContainer cookie, string refer, string content_length)
        {
            var html = "";
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.CookieContainer = cookie;
            webRequest.Referer = refer;
            webRequest.Timeout = 16000;
            webRequest.Accept = "*/*";
            webRequest.KeepAlive = false;
            webRequest.Headers.Add("Accept-Encoding: gzip,deflate");
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
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
            finally
            {
                if (webRequest != null)
                {
                    webRequest.Abort();
                }
            }


        }

        private static string ImgToBase64String(Image img)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                return "data:image/jpeg;base64," + strbaser64;
            }
            catch
            {
                return "图片转为Base64失败。";
            }
        }
    }
}
