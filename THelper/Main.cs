using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using THelper.translate;
using THelper.view;

namespace THelper
{

    public partial class Main : JForm
    {
        private const int WM_HOTKEY = 0x0312;
        private string TeanslateApi, lanTo, translation = String.Empty;
        private NotifyMenu notifyMenu = null;

        #region 窗体事件
        private void label3_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.SeaGreen;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.DarkSeaGreen;
        }
        #endregion

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:
                            if (WindowState != FormWindowState.Normal)
                                WindowState = FormWindowState.Normal;
                            if (Visible == false)
                                Show();
                            break;
                        case 101:
                            if (WindowState != FormWindowState.Minimized)
                                WindowState = FormWindowState.Minimized;
                            if (!Cache.existGetWord)
                            {
                                new ShowByGetWord().Show();
                                Cache.existGetWord = true;
                            }
                            break;
                        case 102:
                            if (WindowState != FormWindowState.Minimized)
                                WindowState = FormWindowState.Minimized;
                            if (!Cache.existOCR)
                            {
                                new Screenshot().Show();
                                Cache.existOCR = true;
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #region Load HotKey
        private void Main_Load(object sender, EventArgs e)
        {
            if (!PingIpOrDomainName("www.baidu.com"))
            {
                new JMessageBox("提示", "无网络连接，本工具无法使用！", 0).ShowDialog();
                Application.Exit();
            }
            RefeshColor();
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Ctrl, Keys.Q);
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.W);
            HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Ctrl, Keys.E);
            TeanslateApi = "百度Baidu";
            webBrowser1.ObjectForScripting = this;
            webBrowser1.DocumentText = Properties.Resources.text;
            ShowMenuEvent += new ShowMenuEventHandler(ShowMenu);
            CloseEvent += new CloseEventHandler(JClosed);
            textBox1.BackColor = Cache.MainColor;
            label2.ForeColor = label4.ForeColor = textBox1.ForeColor = radioButton1.ForeColor = 
                radioButton2.ForeColor = radioButton3.ForeColor = radioButton4.ForeColor = Cache.TextColor;
        }

        private void RefeshColor()
        {
            jTextBtn1.NormelColor = Cache.BaseColor;
            jTextBtn1.HoverColor = Cache.HoverColor;
            jTextBtn1.PresslColor = Cache.PressColor;
            jTextBtn1.BackColor = Cache.BaseColor;
        }
        //检查网络
        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = "";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 5000;
                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                if (strInfo == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        public Main()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            Console.WriteLine(Properties.Settings.Default);
        }

        private void JClosed()
        {
            if (Properties.Settings.Default.NOTIFYICON)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
            else
            {
                Cache.mainPoint = Location;
                Cache.mainSize = Size;
                Console.WriteLine("dsds");
                new JMessageBox("提示", "确定关闭本工具吗？\n\r关闭之后将无法为您提供翻译服务。", 1).ShowDialog();
                if (Cache.exist)
                    Dispose();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (TeanslateApi.Equals("必应Bing"))
                return;
            ChooseBox chooseBox = new ChooseBox(this, true, TDictionary.langDic[TeanslateApi], 97, 12, true);
            chooseBox.Location = new Point(Left + label2.Left, Top + label2.Top + label2.Height + 2);
            chooseBox.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (TeanslateApi.Equals("必应Bing"))
                return;
            Dictionary<string, string> d = TDictionary.langDic[TeanslateApi];
            if (d.ContainsKey("自动检测"))
                d.Remove("自动检测");
            ChooseBox chooseBox = new ChooseBox(this, false, d, 97, 12, true);
            chooseBox.Location = new Point(Left + label4.Left, Top + label4.Top + label4.Height + 2);
            chooseBox.Show();
        }

        public void TextChang(string str, bool isApi)
        {
            if (isApi)
                label2.Text = str;
            else
                label4.Text = str;
        }

        private void jTextBtn1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("setText", new object[2] { "翻译中...", "12px" });
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += new DoWorkEventHandler(work_DoWork);
            work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
            work.RunWorkerAsync(this);
        }

        void work_DoWork(object sender, DoWorkEventArgs e)
        {
            object transLate = new Object();
            Type type = null;
            switch (TeanslateApi)
            {
                case "百度Baidu":
                    transLate = new BaiduTranslate();
                    type = typeof(BaiduTranslate);
                    break;
                case "谷歌Google":
                    transLate = new GoogleTranslate();
                    type = typeof(GoogleTranslate);
                    break;
                case "腾讯Tencent":
                    transLate = new TencentTranslate();
                    type = typeof(TencentTranslate);
                    break;
                case "必应Bing":
                    translation = (new BingTranslate()).必应Bing(textBox1.Text);
                    return;
            }
            string 原文语言 = TDictionary.langDic[TeanslateApi][label2.Text];
            lanTo = TDictionary.langDic[TeanslateApi][label4.Text];
            MethodInfo mt = type.GetMethod(TeanslateApi);
            translation = (string)mt.Invoke(transLate, new object[] { textBox1.Text, 原文语言, lanTo });
        }

        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            webBrowser1.Document.InvokeScript("setText", new object[2]{translation , "12px"});
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            TeanslateApi = ((RadioButton)sender).Tag.ToString();
            if (TeanslateApi.Equals("必应Bing"))
            {
                label2.Text = "中英文自检";
                label4.Text = "中英文自检";
                return;
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (!label2.Text.Equals("自动检测"))
            {
                string str = label2.Text;
                label2.Text = label4.Text;
                label4.Text = str;
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            textBox1.Width = Width - 30;
            textBox1.Height = jTextBtn1.Top - 91;
            webBrowser1.Width = Width - 28;
            webBrowser1.Height = Height - jTextBtn1.Height - jTextBtn1.Top - 22;
            Invalidate();
        }

        private void ShowMenu()
        {
            Menu menu = new Menu(this);
            menu.Location = new Point(Width + Location.X - 120, Location.Y + 26);
            menu.Show();
        }
        
        public void menuOpen(String i)
        {
            switch (i)
            {
                case "0":
                    WindowState = FormWindowState.Minimized;
                    if (!Cache.existGetWord)
                    {
                        new ShowByGetWord().Show();
                        Cache.existGetWord = true;
                    }
                    break;
                case "1":
                    WindowState = FormWindowState.Minimized;
                    if (!Cache.existOCR)
                    {
                        new Screenshot().Show();
                        Cache.existOCR = true;
                    }
                    break;
                case "2":
                    new Explain().ShowDialog(this);
                    break;
                case "3":
                    new Update(this).ShowDialog(this);
                    break;
                case "4":
                    new Config().ShowDialog(this);
                    break;
                case "5":
                    new About().ShowDialog(this);
                    break;
            }
        }

        public void openDownload(string fileFullPath)
        {
            new DownLoad(fileFullPath).ShowDialog(this);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (translation.Length > 0)
            {
                if (pictureBox6.Tag.ToString() == "1")
                {
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    pictureBox6.Image = Properties.Resources.icon_voice;
                    pictureBox6.Tag = 0;
                }
                else
                {
                    axWindowsMediaPlayer1.URL = @"http://fanyi.baidu.com/gettts?lan=" + lanTo + "&text=" + translation;
                    pictureBox6.Image = Properties.Resources.icon_voice_play;
                    pictureBox6.Tag = 1;
                }
            }
                
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                notifyMenu = new NotifyMenu();
                notifyMenu.Show();
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                pictureBox6.Image = Properties.Resources.icon_voice;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.InvokeScript("setBack", new object[2] { ColorTranslator.ToHtml(Cache.MainColor), ColorTranslator.ToHtml(Cache.TextColor)});
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (translation.Length > 0)
            {
                Clipboard.SetText(translation);
                Cache.mainPoint = Location;
                Cache.mainSize = Size;
                new Tip("翻译内容已复制到剪贴板").Show();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(new SolidBrush(Cache.BorderColor), 1);
            g.DrawRectangle(pen, label2.Bounds);
            g.DrawRectangle(pen, label4.Bounds);
            g.DrawRectangle(pen, textBox1.Bounds.Left - 1, textBox1.Bounds.Top -1, textBox1.Bounds.Width + 1, textBox1.Bounds.Height + 1);
            g.DrawRectangle(pen, webBrowser1.Bounds.Left - 1, webBrowser1.Bounds.Top - 1, webBrowser1.Bounds.Width + 1, webBrowser1.Bounds.Height + 1);
            base.OnPaint(e);
        }
    }
}
