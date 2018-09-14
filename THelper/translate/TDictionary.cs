using System.Collections.Generic;

namespace THelper.translate
{
    public class TDictionary
    {
        #region 翻译接口和目标语言字典
        public static Dictionary<string, string> BaseDict0 = new Dictionary<string, string> { { "百度翻译", "百度Baidu" }, { "腾讯翻译", "腾讯Tencent" }, { "谷歌翻译", "谷歌Google" } };
        public static Dictionary<string, string> BaseDict = new Dictionary<string, string> { { "百度翻译", "百度Baidu" }, { "腾讯翻译", "腾讯Tencent" } };
        public static Dictionary<string, Dictionary<string, string>> langDic = new Dictionary<string, Dictionary<string, string>>();
        public static void LangDic_initialization()
        {
            langDic.Add("谷歌Google", new Dictionary<string, string>());
            langDic.Add("百度Baidu", new Dictionary<string, string>());
            langDic.Add("腾讯Tencent", new Dictionary<string, string>());

            langDic["谷歌Google"].Add("简体中文", "zh-CN");
            langDic["谷歌Google"].Add("繁体中文", "zh-TW");
            langDic["谷歌Google"].Add("英语", "en");
            langDic["谷歌Google"].Add("韩语", "ko");
            langDic["谷歌Google"].Add("日语", "ja");
            langDic["谷歌Google"].Add("法语", "fr");
            langDic["谷歌Google"].Add("德语", "de");
            langDic["谷歌Google"].Add("俄语", "ru");
            langDic["谷歌Google"].Add("泰语", "th");
            langDic["谷歌Google"].Add("瑞典语", "sv");
            langDic["谷歌Google"].Add("越南语", "vi");
            langDic["谷歌Google"].Add("意大利语", "it");
            langDic["谷歌Google"].Add("西班牙语", "es");
            langDic["谷歌Google"].Add("葡萄牙语", "pt");
            langDic["谷歌Google"].Add("阿拉伯语", "ar");

            langDic["百度Baidu"].Add("自动检测", "auto");
            langDic["百度Baidu"].Add("简体中文", "zh");
            langDic["百度Baidu"].Add("英语", "en");
            langDic["百度Baidu"].Add("日语", "jp");
            langDic["百度Baidu"].Add("韩语", "kor");
            langDic["百度Baidu"].Add("法语", "fra");
            langDic["百度Baidu"].Add("匈牙利语", "hu");
            langDic["百度Baidu"].Add("繁体中文", "cht");
            langDic["百度Baidu"].Add("文言文", "wyw");
            langDic["百度Baidu"].Add("粤语", "yue");
            langDic["百度Baidu"].Add("阿拉伯语", "ara");
            langDic["百度Baidu"].Add("爱沙尼亚语", "est");
            langDic["百度Baidu"].Add("保加利亚语", "bul");
            langDic["百度Baidu"].Add("波兰语", "pl");
            langDic["百度Baidu"].Add("丹麦语", "dan");
            langDic["百度Baidu"].Add("德语", "de");
            langDic["百度Baidu"].Add("俄语", "ru");
            langDic["百度Baidu"].Add("芬兰语", "fin");
            langDic["百度Baidu"].Add("荷兰语", "nl");
            langDic["百度Baidu"].Add("捷克语", "cs");
            langDic["百度Baidu"].Add("罗马尼亚语", "rom");
            langDic["百度Baidu"].Add("葡萄牙语", "pt");
            langDic["百度Baidu"].Add("瑞典语", "swe");
            langDic["百度Baidu"].Add("斯洛文尼亚语", "slo");
            langDic["百度Baidu"].Add("泰语", "th");
            langDic["百度Baidu"].Add("西班牙语", "spa");
            langDic["百度Baidu"].Add("希腊语", "el");
            langDic["百度Baidu"].Add("意大利语", "it");
            langDic["百度Baidu"].Add("越南语", "vie");

            langDic["腾讯Tencent"].Add("自动检测", "auto");
            langDic["腾讯Tencent"].Add("简体中文", "zh");
            langDic["腾讯Tencent"].Add("英语", "en");
            langDic["腾讯Tencent"].Add("日语", "jp");
            langDic["腾讯Tencent"].Add("韩语", "kr");
            langDic["腾讯Tencent"].Add("法语", "fr");
            langDic["腾讯Tencent"].Add("西班牙语", "es");
            langDic["腾讯Tencent"].Add("意大利语", "it");
            langDic["腾讯Tencent"].Add("德语", "de");
            langDic["腾讯Tencent"].Add("土耳其语", "tr");
            langDic["腾讯Tencent"].Add("俄语", "ru");
            langDic["腾讯Tencent"].Add("葡萄牙语", "pt");
            langDic["腾讯Tencent"].Add("越南语", "vi");
            langDic["腾讯Tencent"].Add("印尼语", "id");
            langDic["腾讯Tencent"].Add("泰语", "th");
            langDic["腾讯Tencent"].Add("马来西亚语", "ms");
        }
        #endregion
    }
}
