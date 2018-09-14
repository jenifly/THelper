using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using static THelper.Main;
using System.Windows.Forms;
using THelper.OCR;
using THelper.translate;
using THelper.view;
using System.Text.RegularExpressions;

namespace THelper
{

    public partial class ShowByOCR : TJForm
    {
        private int maxWidth;
        private string translateApi = "百度Baidu";
        public string resultOCR, contentOCR;

        #region 窗体事件
        private void TShowByOCR_Resize(object sender, EventArgs e)
        {
            textBox1.Height = label2.Top - 37;
            textBox1.Width = Width - 44;
            webBrowser1.Height = textBox1.Height + 2;
            webBrowser1.Width = textBox1.Width;
            Invalidate();
        }
        #endregion

        public ShowByOCR()
        {
            InitializeComponent();
        }
        
        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            contentOCR = OCRBaidu.BaiduOCR(Cache.scrImg);
            resultOCR = Translate(contentOCR);
            string[] strs = contentOCR.Split(Environment.NewLine.ToCharArray());
            Graphics g = textBox1.CreateGraphics();
            List<int> list = new List<int>();
            if(strs.Length > 1)
            {
                for (int i = 0; i < strs.Length - 1; i++)
                {
                    if(strs[i].Length > 0)
                    {
                        SizeF sizeF = g.MeasureString(strs[i], textBox1.Font);
                        list.Add((int)sizeF.Width);
                    }
                }
                list.Sort();
                maxWidth = list[list.Count - 1] + 4;
            }
            else
            {
                maxWidth = Math.Max(234, (int)g.MeasureString(contentOCR, textBox1.Font).Width);
            }
        }

        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBox1.Text = contentOCR;
            webBrowser1.Document.InvokeScript("setText", new object[2] { resultOCR, "12px" });
            textBox1.Width = maxWidth;
            webBrowser1.Width = maxWidth;
            Width = maxWidth + 44;
        }

        private string Translate(string str)
        {
            object transLate = new Object();
            Type type = null;
            switch (translateApi)
            {
                case "百度Baidu":
                    transLate = new BaiduTranslate();
                    type = typeof(BaiduTranslate);
                    break;
                case "腾讯Tencent":
                    transLate = new TencentTranslate();
                    type = typeof(TencentTranslate);
                    break;
            }
            MethodInfo mt = type.GetMethod(translateApi);
            return (string)mt.Invoke(transLate, new object[] { str, "auto", lanToVaule }); ;
        }

        private void JClosed()
        {
            Cache.existScreenshot = false;
            Dispose();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (pictureBox6.Tag.ToString() == "1")
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                pictureBox6.Image = Properties.Resources.icon_voice;
                pictureBox6.Tag = 0;
            }
            else
            {
                axWindowsMediaPlayer1.URL = @"http://fanyi.baidu.com/gettts?lan=" + lanToVaule + "&text=" + resultOCR;
                pictureBox6.Image = Properties.Resources.icon_voice_play;
                pictureBox6.Tag = 1;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (resultOCR.Length > 0)
            {
                Clipboard.SetText(resultOCR);
                Cache.mainPoint = Location;
                Cache.mainSize = Size;
                new Tip("翻译内容已复制到剪贴板").Show();
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                pictureBox6.Image = Properties.Resources.icon_voice;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                webBrowser1.Document.InvokeScript("setText", new object[2] { "翻译中...", "12px" });
                BackgroundWorker work = new BackgroundWorker();
                work.DoWork += new DoWorkEventHandler(work_DoWork1);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted1);
                work.RunWorkerAsync(this);
            }
        }

        void work_DoWork1(object sender, DoWorkEventArgs e)
        {
            string[] strs = textBox1.Text.Split(Environment.NewLine.ToCharArray());
            resultOCR = "";
            foreach (string str in strs)
            {
                if (str.Length > 0)
                    resultOCR += Translate(str) + "<br/>";
            }
        }

        void work_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            webBrowser1.Document.InvokeScript("setText", new object[2] { resultOCR, "12px" });
        }

        private void ShowByOCR_Load(object sender, EventArgs e)
        {
            webBrowser1.ObjectForScripting = this;
            webBrowser1.DocumentText = Properties.Resources.text;
            textBox1.BackColor = Cache.MainColor;
            textBox1.ForeColor = label1.ForeColor = label2.ForeColor = Cache.TextColor;
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
            work.RunWorkerAsync(this);
            CloseEvent += new CloseEventHandler(JClosed);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.InvokeScript("setBack", new object[2] { ColorTranslator.ToHtml(Cache.MainColor), ColorTranslator.ToHtml(Cache.TextColor) });
            webBrowser1.Document.InvokeScript("setText", new object[2] { "请稍候...", "12px" });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen pen = new Pen(new SolidBrush(Cache.BorderColor), 1);
            Graphics g = e.Graphics;
            g.DrawRectangle(pen, textBox1.Bounds.Left - 1, textBox1.Bounds.Top - 1, textBox1.Bounds.Width + 1, textBox1.Bounds.Height + 1);
            g.DrawRectangle(pen, webBrowser1.Bounds.Left - 1, webBrowser1.Bounds.Top - 1, webBrowser1.Bounds.Width + 1, webBrowser1.Bounds.Height + 1);
            base.OnPaint(e);
        }
    }
}