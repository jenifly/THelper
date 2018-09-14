using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THelper.view
{
    public partial class JForm : Form
    {
        #region private class
        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        private const int Guying_HTLEFT = 10;
        private const int Guying_HTRIGHT = 11;
        private const int Guying_HTTOP = 12;
        private const int Guying_HTTOPLEFT = 13;
        private const int Guying_HTTOPRIGHT = 14;
        private const int Guying_HTBOTTOM = 15;
        private const int Guying_HTBOTTOMLEFT = 0x10;
        private const int Guying_HTBOTTOMRIGHT = 17;
        #endregion

        #region register dll
        ///阴影
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        #endregion

        #region  Config
        [Browsable(true), Category("JSkin"), Description("是否开启窗体阴影")]
        public bool ShowShadow { set; get; } = true;
        [Browsable(true), Category("JSkin"), Description("是否开启顶部色条")]
        public bool ShowTopBar { set; get; } = true;
        [Browsable(true), Category("JSkin"), Description("顶部色条高度")]
        public int TopBarHeight { set; get; } = 4;
        [Browsable(true), Category("JSkin"), Description("顶部色条颜色")]
        public Color TopBarColort { set; get; } = Color.WhiteSmoke;
        [Browsable(true), Category("JSkin"), Description("系统按钮大小")]
        public Size SysBtnSize { set; get; } = new Size(27, 22);
        [Browsable(true), Category("JSkin"), Description("是否显示关闭按钮")]
        public bool ShowSys_Close { set; get; } = true;
        [Browsable(true), Category("JSkin"), Description("是否显示最小化按钮")]
        public bool ShowSys_Min { set; get; } = false;
        [Browsable(true), Category("JSkin"), Description("是否显示菜单按钮")]
        public bool ShowSys_Menu { set; get; } = false;
        [Browsable(true), Category("JSkin"), Description("是否鼠标拖动边框改变窗体大小")]
        public bool CanDragReSize { set; get; } = false;
        [Browsable(true), Category("JSkin"), Description("是否拖动")]
        public bool CanDrag { set; get; } = true;
        [Browsable(true), Category("JSkin"), Description("窗体标题")]
        public string MainText { set; get; } = String.Empty;
        [Browsable(true), Category("JSkin"), Description("窗体标题字体")]
        public Font MainTextFont { set; get; } = new Font(new FontFamily("微软雅黑"), 10, FontStyle.Regular, GraphicsUnit.Pixel);
        [Browsable(true), Category("JSkin"), Description("窗体标题位置")]
        public Point MainTextLocation { set; get; } = new Point(26, 1);
        [Browsable(true), Category("JSkin"), Description("窗体标题颜色")]
        public Color MainTextColor { set; get; } = Color.Black;
        [Browsable(true), Category("JSkin"), Description("窗体图标")]
        public Image MainIcon { set; get; } = null;
        [Browsable(true), Category("JSkin"), Description("窗体图标位置")]
        public Point MainIconLocationt { set; get; } = new Point(4, 2);
        [Browsable(true), Category("JSkin"), Description("窗体图标大小")]
        public Size MainIconSize { set; get; } = new Size(20, 20);
        #endregion

        #region Message
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled && ShowShadow)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 0,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }
                    base.WndProc(ref m);
                    break;
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (CanDragReSize && WindowState != FormWindowState.Maximized)
                    {
                        if (vPoint.X <= 4)
                            if (vPoint.Y <= 4)
                                m.Result = (IntPtr)Guying_HTTOPLEFT;
                            else if (vPoint.Y >= ClientSize.Height - 4)
                                m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                            else
                                m.Result = (IntPtr)Guying_HTLEFT;
                        else if (vPoint.X >= ClientSize.Width - 4)
                            if (vPoint.Y <= 4)
                                m.Result = (IntPtr)Guying_HTTOPRIGHT;
                            else if (vPoint.Y >= ClientSize.Height - 4)
                                m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                            else
                                m.Result = (IntPtr)Guying_HTRIGHT;
                        else if (vPoint.Y <= 4)
                            m.Result = (IntPtr)Guying_HTTOP;
                        else if (vPoint.Y >= ClientSize.Height - 4)
                            m.Result = (IntPtr)Guying_HTBOTTOM;
                    }
                    break;
                case 0x0201:
                    if (CanDrag)
                    {
                        m.Msg = 0x00A1; //更改消息为非客户区按下鼠标
                        m.WParam = new IntPtr(2);//鼠标放在标题栏内
                    }
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        #region delegate
        public delegate void ShowMenuEventHandler();
        public event ShowMenuEventHandler ShowMenuEvent;
        public delegate void CloseEventHandler();
        public event CloseEventHandler CloseEvent;
        #endregion

        public JForm()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
        }

        private void JForm_Load(object sender, EventArgs e)
        {
            Point p = new Point(Width - SysBtnSize.Width, TopBarHeight);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            if (ShowSys_Close)
            {
                TopBarColort = Cache.BaseColor;
                BackColor = Cache.MainColor;
                sysBtn_Close.Size = SysBtnSize;
                sysBtn_Close.Location = p;
                sysBtn_Close.BackColor = BackColor;
            }
            else
                TopBarColort = Color.WhiteSmoke;
            if (ShowSys_Min)
            {
                p.Offset(-SysBtnSize.Width, 0);
                sysBtn_Min.Size = SysBtnSize;
                sysBtn_Min.Location = p;
                sysBtn_Min.BackColor = BackColor;
            }
            if (ShowSys_Menu)
            {
                p.Offset(-SysBtnSize.Width, 0);
                sysBtn_Menu.Size = SysBtnSize;
                sysBtn_Menu.Location = p;
                sysBtn_Menu.BackColor = BackColor;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (ShowTopBar)
            {
                g.FillRectangle(new SolidBrush(TopBarColort), new Rectangle(0, 0, Width, TopBarHeight));
                g.TranslateTransform(0, TopBarHeight);
            }
            else
            {
                g.FillRectangle(new SolidBrush(TopBarColort), new Rectangle(0, 0, Width, 1));
            }
            if (MainIcon != null)
            {
                g.DrawImage(MainIcon, new Rectangle(MainIconLocationt, MainIconSize));
            }
                
            if (!MainText.Equals(String.Empty))
                g.DrawString(MainText, MainTextFont,new SolidBrush(Cache.TextColor), MainTextLocation);
            base.OnPaint(e);
        }

        public void TopBarColorChanged(Color color)
        {
            TopBarColort = color;
            Invalidate();
        }

        private void sysBtn_Close_Click(object sender, EventArgs e)
        {
            if (CloseEvent == null)
                Dispose();
            else
                CloseEvent();
        }

        private void sysBtn_Min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void sysBtn_Menu_Click(object sender, EventArgs e)
        {
            ShowMenuEvent();
        }

        private void JForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseEvent != null)
            {
                e.Cancel = true;
                CloseEvent();
            }
        }
    }
}
