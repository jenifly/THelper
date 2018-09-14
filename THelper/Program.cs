using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using THelper.helper;
using THelper.translate;
using THelper.view;

namespace THelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoadData();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TDictionary.LangDic_initialization();
            Application.Run(new Main());
        }

        static void LoadData()
        {
            Cache.MainColor = ColorTranslator.FromHtml(Properties.Settings.Default.MAINCOLOR);
            Cache.BaseColor = ColorTranslator.FromHtml(Properties.Settings.Default.BASECOLOR);
            Cache.HoverColor = Cache.ChangeColor(Cache.BaseColor, -0.15f);
            Cache.PressColor = Cache.ChangeColor(Cache.BaseColor, -0.3f);
            Cache.BorderColor = Cache.ChangeColor(Cache.MainColor, -0.4f);
            Cache.MNColor = Cache.ChangeColor(Cache.MainColor, -0.15f);
            Cache.MHColor = Cache.ChangeColor(Cache.MainColor, -0.11f);
            Cache.TextColor = Cache.ChangeColor(Cache.MainColor, -0.5f);
        }
    }
}
