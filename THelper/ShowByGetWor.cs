using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using THelper.translate;
using THelper.view;
using static THelper.Main;

namespace THelper
{

    public partial class ShowByGetWord : JForm
    {
        const string LICENSEID = "{00000000-0000-0000-0000-000000000000}";
        const int MOD_ALT = 0x0001;
        const int MOD_CONTROL = 0x0002;
        const int MOD_SHIFT = 0x0004;
        const int MOD_WIN = 0x0008;
        const int VK_LBUTTON = 0x01;
        const int VK_RBUTTON = 0x02;
        const int VK_MBUTTON = 0x04;
        const int WM_LBUTTONDBLCLK = 0x0203;
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        bool bGetWordUnloaded = false;
        NotifyCallBack callbackHighlightReady = null;
        NotifyCallBack callbackMouseMonitor = null;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int WMSZ_BOTTOMRIGHT = 0xF008;
        public const int SC_MOVE = 0xF010;

        private string translateApi = "百度Baidu";
        private string pbstr = String.Empty;
        private string translation = String.Empty;

        #region 注册dll
        //屏幕取词
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public extern static bool GetCursorPos(out System.Drawing.Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Auto)]
        public static extern IntPtr WindowFromPoint(System.Drawing.Point point);

        [DllImport("kernel32.dll", EntryPoint = "Sleep")]
        public static extern void Sleep(int uMilliSec);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        public static extern int FreeLibrary(int hLibModule);

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandle")]
        public static extern int GetModuleHandle(string lpModuleName);

        [DllImport("GetWord.dll")]
        public static extern void SetLicenseID([MarshalAs(UnmanagedType.BStr)] string szLicense);

        [DllImport("GetWord.dll")]
        public static extern void SetNotifyWnd(Int32 hWndNotify);

        [DllImport("GetWord.dll")]
        public static extern void UnSetNotifyWnd(Int32 hWndNotify);

        [DllImport("GetWord.dll")]
        public static extern void SetDelay(Int32 uMilliSec);

        [DllImport("GetWord.dll")]
        public static extern bool EnableCursorCapture(bool bEnable);

        [DllImport("GetWord.dll")]
        public static extern bool EnableHotkeyCapture(bool bEnable, Int32 fsModifiers, Int32 vKey);

        [DllImport("GetWord.dll")]
        public static extern bool EnableHighlightCapture(bool bEnable);

        [DllImport("GetWord.dll")]
        public static extern bool GetString(Int32 x, Int32 y, [MarshalAs(UnmanagedType.BStr)] out string str, out Int32 nCursorPos);

        [DllImport("GetWord.dll")]
        public static extern bool GetString2(Int32 x, Int32 y, [MarshalAs(UnmanagedType.BStr)] out string str, out Int32 nCursorPos, out Int32 left, out Int32 top, out Int32 right, out Int32 bottom);

        [DllImport("GetWord.dll")]
        public static extern bool FreeString([MarshalAs(UnmanagedType.BStr)] out string str);

        [DllImport("GetWord.dll")]
        public static extern bool GetRectString(Int32 hWnd, Int32 left, Int32 top, Int32 right, Int32 bottom, [MarshalAs(UnmanagedType.BStr)] out string str);

        [DllImport("GetWord.dll")]
        public static extern Int32 GetRectStringPairs(Int32 hWnd, Int32 left, Int32 top, Int32 right, Int32 bottom, [MarshalAs(UnmanagedType.BStr)] out string str, [MarshalAs(UnmanagedType.BStr)] out string rectList);

        [DllImport("GetWord.dll")]
        public static extern Int32 GetPointStringPairs(Int32 x, Int32 y, [MarshalAs(UnmanagedType.BStr)] out string str, [MarshalAs(UnmanagedType.BStr)] out string rectList);

        [DllImport("GetWord.dll")]
        public static extern bool FreePairs([MarshalAs(UnmanagedType.BStr)] out string str, [MarshalAs(UnmanagedType.BStr)] out string rectList);

        [DllImport("GetWord.dll")]
        public static extern bool GetPairItem(Int32 totalCount, [MarshalAs(UnmanagedType.BStr)] out string str, [MarshalAs(UnmanagedType.BStr)] out string rectList, Int32 index, [MarshalAs(UnmanagedType.BStr)] out string substr, out Int32 substrLen, out Int32 substrLeft, out Int32 substrTop, out Int32 substrRight, out Int32 substrBottom);

        [DllImport("GetWord.dll")]
        public static extern bool GetHighlightText(Int32 hWnd, [MarshalAs(UnmanagedType.BStr)] out string str);

        [DllImport("GetWord.dll")]
        public static extern bool GetHighlightText2(Int32 x, Int32 y, [MarshalAs(UnmanagedType.BStr)] out string str);

        public delegate Int32 NotifyCallBack(Int32 wParam, Int32 lParam);

        [DllImport("GetWord.dll")]
        public static extern bool SetCaptureReadyCallback(NotifyCallBack callback);

        [DllImport("GetWord.dll")]
        public static extern bool RemoveCaptureReadyCallback(NotifyCallBack callback);

        [DllImport("GetWord.dll")]
        public static extern bool SetHighlightReadyCallback(NotifyCallBack callback);

        [DllImport("GetWord.dll")]
        public static extern bool RemoveHighlightReadyCallback(NotifyCallBack callback);

        [DllImport("GetWord.dll")]
        public static extern bool SetMouseMonitorCallback(NotifyCallBack callback);

        [DllImport("GetWord.dll")]
        public static extern bool RemoveMouseMonitorCallback(NotifyCallBack callback);
        #endregion

        public ShowByGetWord()
        {
            InitializeComponent();
            Rectangle screenArea = Screen.GetWorkingArea(this);
            Location = new Point(Math.Min(MousePosition.X, screenArea.Width - Width - 10), 
                Math.Min(MousePosition.Y, screenArea.Height - Height - 10));
        }


        private void TShowByGetWord_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> pair in TDictionary.langDic[translateApi])
            {
                if (!pair.Key.Equals("自动检测")) comboBox2.Items.Add(pair.Key);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            SetLicenseID(LICENSEID); 
            SetDelay(300);
            callbackHighlightReady = new NotifyCallBack(OnHighlightReady);
            SetHighlightReadyCallback(callbackHighlightReady);
            callbackMouseMonitor = new NotifyCallBack(OnMouseMonitor);
            SetMouseMonitorCallback(callbackMouseMonitor);
            EnableHighlightCapture(true);
        }

        private Int32 OnHighlightReady(Int32 wParam, Int32 lParam)
        {
            if (bGetWordUnloaded)
                return 1;
            Int32 returnValue = 0;
            if (bGetWordUnloaded)
                return returnValue;
            int cursorX = (int)(wParam & 0xffff);               // get the low word (two bytes)
            int cursorY = (int)((wParam & 0xffff0000) >> 16);   // get the high word (two bytes)
            bool bOK = GetHighlightText2(cursorX, cursorY, out pbstr);

            if (bOK)
            {
                
                translation = Translate(pbstr);
                webBrowser1.DocumentText = "<style>*{line-height:13px;margin:5px;padding:0;font-family: Microsoft YaHei;font-size:11px}</style><p>" + translation + "</p>";
                FreeString(out pbstr);

                returnValue = 0;
            }
            else
            {
                translation = "is failed";
                returnValue = 1;
            }

            return returnValue;
        }

        private string Translate(string str)
        {
            object transLate = new Object();
            Type type = null;
            switch (translateApi)
            {
                case "百度Baidu":
                    transLate = new BaiduTransLate();
                    type = typeof(BaiduTransLate);
                    break;
                case "腾讯Tencent":
                    transLate = new TencentTransLate();
                    type = typeof(TencentTransLate);
                    break;
            }
            MethodInfo mt = type.GetMethod(translateApi);
            return (string)mt.Invoke(transLate, new object[] { str, "auto", TDictionary.langDic[translateApi][label2.Text]});
        }

        private Int32 OnMouseMonitor(Int32 wParam, Int32 lParam)
        {
            if (bGetWordUnloaded)
                return 1;

            Int32 returnValue = 0;

            if (bGetWordUnloaded)
                return returnValue;

            if (wParam == WM_LBUTTONDBLCLK)
            {
                returnValue = OnHighlightReady(wParam, lParam);
            }

            return returnValue;
        }

        #region ControlBox
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Cache.existGetWord = false;
            bGetWordUnloaded = true;
            RemoveHighlightReadyCallback(callbackHighlightReady);
            RemoveMouseMonitorCallback(callbackMouseMonitor);
            Dispose();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.IndianRed;
        }

        private void ControlBox_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }

        #endregion

        private void label4_Click(object sender, EventArgs e)
        {
            comboBox1.DroppedDown = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            comboBox2.DroppedDown = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = comboBox1.SelectedItem.ToString();
            comboBox2.Items.Clear();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            switch (comboBox1.SelectedItem.ToString())
            {
                case "百度翻译":
                    translateApi = "百度Baidu";
                    break;
                case "腾讯翻译":
                    translateApi = "腾讯Tencent";
                    break;
            }
            dict = TDictionary.langDic[translateApi];
            foreach (KeyValuePair<string, string> pair in dict)
            {
                if (!pair.Key.Equals("自动检测")) comboBox2.Items.Add(pair.Key);
            }
            if (pbstr.Length > 0)
            {
                translation = Translate(pbstr);
                webBrowser1.DocumentText = "<style>*{line-height:13px;margin:5px;padding:0;font-family: Microsoft YaHei;font-size:11px}</style><p>" + translation + "</p>";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = comboBox2.SelectedItem.ToString();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (translation.Length > 0)
            {
                if(pictureBox6.Tag.ToString() == "1")
                {
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    pictureBox6.Image = Properties.Resources.icon_voice;
                    pictureBox6.Tag = 0;
                }
                else
                {
                    axWindowsMediaPlayer1.URL = @"http://fanyi.baidu.com/gettts?lan=" + TDictionary.langDic[translateApi][label2.Text] + "&text=" + translation;
                    pictureBox6.Image = Properties.Resources.icon_voice_play;
                    pictureBox6.Tag = 1;
                }
            }
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

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                pictureBox6.Image = Properties.Resources.icon_voice;
            }
        }

        private void ShowByGetWord_MouseEnter(object sender, EventArgs e)
        {
            EnableHighlightCapture(false);
        }

        private void ShowByGetWord_MouseLeave(object sender, EventArgs e)
        {
            EnableHighlightCapture(true);
        }
    }
}
