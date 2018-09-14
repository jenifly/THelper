using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THelper
{
    public partial class Screenshot : Form
    {
        #region 桌面Z序窗口注册dll
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref RECT lpRect);
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);
        [DllImport("user32")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32")]
        private static extern bool IsWindowVisible(IntPtr hwnd);
        #endregion

        private Point startPoint = Point.Empty, endPoint = Point.Empty;
        private Bitmap CatchBmp;
        private int inBtn = 0;
        public int ZoomBoxWidth = 120;
        public int ZoomBoxHeight = 100;
        private long lastMouseMoveTime;
        private Dictionary<int, Rectangle> blockDic = new Dictionary<int, Rectangle>();
        private Rectangle completRect, cancelRect;
        private List<Rectangle> rects = new List<Rectangle>();
        private enum ScreenshotStatus : uint
        {
            SCREENSHOTSTART = 0,
            SCREENSHOTTRIM = 1,
            SCREENSHOTEND = 2,
        }
        private ScreenshotStatus screenshotStatus;
        private enum TrimType : uint
        {
            NONE = 0,
            MOVE = 1,
            TOP = 2,
            BOTTOM = 3,
            LEFT = 4,
            RIGHTR = 5,
            TOPLEFT = 6,
            TOPRIGHT = 7,
            BOTTOMLEFT = 8,
            BOTTOMRIGHT = 9
        }
        private TrimType trimType;
        private int trim_X = 0, trim_Y = 0;

        public Screenshot()
        {
            Rectangle screenArea = Screen.GetBounds(this);
            IntPtr desktopPtr = GetDesktopWindow();
            IntPtr winPtr = GetWindow(desktopPtr, 5);
            while (winPtr != IntPtr.Zero)
            {
                winPtr = GetWindow(winPtr, 2);
                addRectangle(winPtr, screenArea.Width, screenArea.Height,
                    new List<string> { "Internet Explorer Server", "OrpheusShadow", "DummyDWMListenerWindow",
                        "EdgeUiInputTopWndClass", "Internet Explorer_Hidden", "Internet Explorer_Hidden", "Progman" });
            }
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        private void addRectangle(IntPtr hwnd, int w, int h, List<string> l)
        {
            RECT rect = new RECT();
            StringBuilder s = new StringBuilder(512);
            GetClassName(hwnd, s, s.Capacity);
            if (IsWindowVisible(hwnd) && !l.Contains(s.ToString()))
            {
                GetWindowRect(hwnd, ref rect);
                rect.Left = Math.Max(0, rect.Left);
                rect.Top = Math.Max(0, rect.Top);
                rect.Right = Math.Min(w, rect.Right);
                rect.Bottom = Math.Min(h, rect.Bottom);
                rects.Add(new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);
            Region reg = new Region(new Rectangle(0, 0, Width, Height));
            Pen pen = new Pen(brush, 1);
            if (startPoint != Point.Empty)
            {
                Rectangle srcRect = new Rectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y),
                    Math.Max(1, Math.Abs(endPoint.X - startPoint.X)), Math.Max(1, Math.Abs(endPoint.Y - startPoint.Y)));
                reg.Xor(srcRect);
                g.FillRegion(new SolidBrush(Color.FromArgb(120, 0, 0, 0)), reg);
                g.DrawRectangle(pen, srcRect);
                blockDic.Clear();
                int blockX = srcRect.Width / 2 - 2;
                int blockY = srcRect.Height / 2 - 2;
                Rectangle blockRect = new Rectangle(-2, -2, 5, 5);
                blockRect.Offset(srcRect.Location);
                srcRect.Inflate(-2, -2);
                blockDic.Add(0, srcRect);
                g.FillRectangle(brush, blockRect);
                blockDic.Add(1, blockRect);
                blockRect.Offset(new Point(blockX + 2, 0));
                g.FillRectangle(brush, blockRect);
                blockDic.Add(2, blockRect);
                blockRect.Offset(new Point(blockX + 2, 0));
                g.FillRectangle(brush, blockRect);
                blockDic.Add(3, blockRect);
                blockRect.Offset(new Point(0, blockY + 2));
                g.FillRectangle(brush, blockRect);
                blockDic.Add(4, blockRect);
                blockRect.Offset(new Point(0, blockY + 2));
                g.FillRectangle(brush, blockRect);
                blockDic.Add(5, blockRect);
                blockRect.Offset(new Point(-blockX - 2, 0));
                g.FillRectangle(brush, blockRect);
                blockDic.Add(6, blockRect);
                blockRect.Offset(new Point(-blockX - 2, 0));
                g.FillRectangle(brush, blockRect);
                blockDic.Add(7, blockRect);
                blockRect.Offset(new Point(0, -blockY - 2));
                g.FillRectangle(brush, blockRect);
                blockDic.Add(8, blockRect);

                String wh = Math.Max(0, srcRect.Width) + " X " + Math.Max(0, srcRect.Height);
                Font font = new Font(new FontFamily("微软雅黑"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
                SizeF sizeF = g.MeasureString(wh, font);
                Rectangle whRect = new Rectangle(srcRect.X - 4, srcRect.Y - 10 - (int)sizeF.Height > 0 ? srcRect.Y - 10 - (int)sizeF.Height : srcRect.Y + 1,
                    (int)sizeF.Width + 2, (int)sizeF.Height + 2);
                g.FillRectangle(new SolidBrush(Color.Black), whRect);
                g.DrawString(wh, font, new SolidBrush(Color.WhiteSmoke), whRect.X + 1, whRect.Y + 1);

                if (screenshotStatus == ScreenshotStatus.SCREENSHOTEND)
                {
                    int w = 80, h = 24;
                    srcRect = new Rectangle(new Point(endPoint.X - w, endPoint.Y + 8), new Size(w, h));
                    g.FillRectangle(new SolidBrush(Color.MintCream), srcRect);
                    completRect = cancelRect = new Rectangle(srcRect.Location, new Size(w / 2 - 6, h - 4));
                    cancelRect.Offset(4, 1);
                    completRect.Offset(w / 2 + 1, 1);
                    Rectangle rect = cancelRect;
                    rect.Inflate(-10, -2);
                    rect.Offset(0, 1);
                    g.DrawImage(Properties.Resources.icon_cancle, rect);
                    rect.Inflate(3, 3);
                    rect.Offset(w / 2 - 2, 0);
                    g.DrawImage(Properties.Resources.icon_complete, rect);
                    if (inBtn == 1) g.DrawRectangle(pen, cancelRect);
                    if (inBtn == 2) g.DrawRectangle(pen, completRect);
                }
            }
            else
            {
                pen.Width = 5;
                foreach (Rectangle rect in rects)
                {
                    if (rect.Contains(MousePosition))
                    {
                        reg.Xor(rect);
                        g.FillRegion(new SolidBrush(Color.FromArgb(120, 10, 10, 10)), reg);
                        g.DrawRectangle(pen, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 5, rect.Height - 5));
                        break;
                    }
                }
                
            }
            if (screenshotStatus != ScreenshotStatus.SCREENSHOTEND)
            {
                int srcWidth = ZoomBoxWidth / 6;
                int srcHeight = ZoomBoxHeight / 6;
                Rectangle zoomRect = new Rectangle(Math.Min(MousePosition.X + 8, Width - ZoomBoxWidth - 2),
                    Math.Min(MousePosition.Y + 24, Height - ZoomBoxHeight - 2), ZoomBoxWidth, ZoomBoxHeight);
                g.FillRectangle(new SolidBrush(Color.FromArgb(180, 0, 0, 0)), 
                    new Rectangle(zoomRect.X - 2, zoomRect.Y - 2, zoomRect.Width + 4, zoomRect.Height + 2));
                Bitmap bmp_lbl = new Bitmap(ZoomBoxWidth, ZoomBoxHeight);
                Bitmap bmp = new Bitmap(srcWidth, srcHeight);
                Graphics gr = Graphics.FromImage(bmp);
                gr.DrawImage(CatchBmp, 0, 0, new Rectangle(MousePosition.X, MousePosition.Y, srcWidth, srcHeight), GraphicsUnit.Pixel);
                gr.Dispose();
                int x, y;
                for (int row = 0; row < bmp.Height; row++)
                {
                    for (int col = 0; col < bmp.Width; col++)
                    {
                        Color pc = bmp.GetPixel(col, row);
                        for (int h = 0; h < 6; h++)
                        {
                            for (int w = 0; w < 6; w++)
                            {
                                x = col * 6 + w;
                                y = row * 6 + h;
                                if (x < bmp_lbl.Width && y < bmp_lbl.Height)
                                {
                                    bmp_lbl.SetPixel(x, y, pc);
                                }
                            }
                        }
                    }
                }
                g.DrawImage(bmp_lbl, zoomRect.X + 1, zoomRect.Y + 1);
                pen.Width = 3;
                g.DrawLine(pen, new Point(zoomRect.X + ZoomBoxWidth / 2 - 1, zoomRect.Y), new Point(zoomRect.X + ZoomBoxWidth / 2 - 1, ZoomBoxHeight + zoomRect.Y - 2));
                g.DrawLine(pen, new Point(zoomRect.X, zoomRect.Y + ZoomBoxHeight / 2 - 1), new Point(zoomRect.X + ZoomBoxWidth, zoomRect.Y + ZoomBoxHeight / 2 - 1));
                pen.Brush = new SolidBrush(Color.WhiteSmoke);
                pen.Width = 2;
                zoomRect.Height -= 2;
                g.DrawRectangle(pen, zoomRect);
                bmp.Dispose();
                bmp_lbl.Dispose();
            }
        }

        private void Screenshot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (cancelRect.Contains(e.Location))
                {
                    Cache.existOCR = false;
                    Dispose();
                }
                if (completRect.Contains(e.Location))
                    ExecCutImage();
                if (startPoint == Point.Empty)
                {
                    startPoint = e.Location;
                    screenshotStatus = ScreenshotStatus.SCREENSHOTSTART;
                }
                else if (trimType != TrimType.NONE)
                {
                    screenshotStatus = ScreenshotStatus.SCREENSHOTTRIM;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                Cache.existOCR = false;
                Dispose();
            }
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                ExecCutImage();
            }

        }

        private void Screenshot_MouseMove(object sender, MouseEventArgs e)
        {
            long mouseMoveTimeStep = DateTime.Now.Ticks - lastMouseMoveTime;
            if (mouseMoveTimeStep > 200)
            {
                lastMouseMoveTime = DateTime.Now.Ticks;
                switch (screenshotStatus)
                {
                    case ScreenshotStatus.SCREENSHOTSTART:
                        endPoint = e.Location;
                        break;
                    case ScreenshotStatus.SCREENSHOTTRIM:
                        trim_X = trim_X == 0 ? e.X : trim_X;
                        trim_Y = trim_Y == 0 ? e.Y : trim_Y;
                        switch (trimType)
                        {
                            case TrimType.MOVE:
                                startPoint.Offset(e.X - trim_X, e.Y - trim_Y);
                                endPoint.Offset(e.X - trim_X, e.Y - trim_Y);
                                break;
                            case TrimType.TOP:
                                startPoint.Offset(0, e.Y - trim_Y);
                                break;
                            case TrimType.TOPLEFT:
                                startPoint.Offset(e.X - trim_X, e.Y - trim_Y);
                                break;
                            case TrimType.TOPRIGHT:
                                startPoint.Offset(0, e.Y - trim_Y);
                                endPoint.Offset(e.X - trim_X, 0);
                                break;
                            case TrimType.RIGHTR:
                                endPoint.Offset(e.X - trim_X, 0);
                                break;
                            case TrimType.BOTTOMRIGHT:
                                endPoint.Offset(e.X - trim_X, e.Y - trim_Y);
                                break;
                            case TrimType.BOTTOM:
                                endPoint.Offset(0, e.Y - trim_Y);
                                break;
                            case TrimType.BOTTOMLEFT:
                                startPoint.Offset(e.X - trim_X, 0);
                                endPoint.Offset(0, e.Y - trim_Y);
                                break;
                            case TrimType.LEFT:
                                startPoint.Offset(e.X - trim_X, 0);
                                break;
                        }
                        trim_X = e.X;
                        trim_Y = e.Y;
                        break;
                    case ScreenshotStatus.SCREENSHOTEND:
                        if (cancelRect.Contains(e.Location))
                            inBtn = 1;
                        else if (completRect.Contains(e.Location))
                            inBtn = 2;
                        else
                            inBtn = 0;
                        if (blockDic.Count == 9)
                        {
                            foreach (KeyValuePair<int, Rectangle> pair in blockDic)
                            {
                                if ((pair.Value).Contains(e.Location))
                                {
                                    switch (pair.Key)
                                    {
                                        case 0:
                                            if (Cursor != Cursors.SizeAll) Cursor = Cursors.SizeAll;
                                            trimType = TrimType.MOVE;
                                            break;
                                        case 1:
                                            if (Cursor != Cursors.SizeNWSE) Cursor = Cursors.SizeNWSE;
                                            trimType = TrimType.TOPLEFT;
                                            break;
                                        case 2:
                                            if (Cursor != Cursors.SizeNS) Cursor = Cursors.SizeNS;
                                            trimType = TrimType.TOP;
                                            break;
                                        case 3:
                                            if (Cursor != Cursors.SizeNESW) Cursor = Cursors.SizeNESW;
                                            trimType = TrimType.TOPRIGHT;
                                            break;
                                        case 4:
                                            if (Cursor != Cursors.SizeWE) Cursor = Cursors.SizeWE;
                                            trimType = TrimType.RIGHTR;
                                            break;
                                        case 5:
                                            if (Cursor != Cursors.SizeNWSE) Cursor = Cursors.SizeNWSE;
                                            trimType = TrimType.BOTTOMRIGHT;
                                            break;
                                        case 6:
                                            if (Cursor != Cursors.SizeNS) Cursor = Cursors.SizeNS;
                                            trimType = TrimType.BOTTOM;
                                            break;
                                        case 7:
                                            if (Cursor != Cursors.SizeNESW) Cursor = Cursors.SizeNESW;
                                            trimType = TrimType.BOTTOMLEFT;
                                            break;
                                        case 8:
                                            if (Cursor != Cursors.SizeWE) Cursor = Cursors.SizeWE;
                                            trimType = TrimType.LEFT;
                                            break;
                                    }
                                    break;
                                }
                                else
                                {
                                    if (Cursor != Cursors.Default) Cursor = Cursors.Default;
                                    trimType = TrimType.NONE;
                                }
                            }
                        }
                        break;
                }
                Invalidate();
            }
        }

        private void Screenshot_MouseUp(object sender, MouseEventArgs e)
        {
            screenshotStatus = ScreenshotStatus.SCREENSHOTEND;
            trim_X = 0;
            trim_Y = 0;
            foreach (KeyValuePair<int, Rectangle> pair in blockDic)
            {
                if (pair.Key > 0)
                {
                    pair.Value.Inflate(2, 2);
                }
            }
        }

        //按键执行事件
        private void Screenshot_KeyDown(object sender, KeyEventArgs e)
        {
            if (screenshotStatus == ScreenshotStatus.SCREENSHOTEND)
            {
                if (e.KeyCode == Keys.Up)
                {
                    startPoint.Offset(0, -1);
                    endPoint.Offset(0, -1);
                    Invalidate();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    startPoint.Offset(0, 1);
                    endPoint.Offset(0, 1);
                    Invalidate();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    startPoint.Offset(-1, 0);
                    endPoint.Offset(-1, 0);
                    Invalidate();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    startPoint.Offset(1, 0);
                    endPoint.Offset(1, 0);
                    Invalidate();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    Dispose();
                }
            }
        }

        private void Screenshot_Load(object sender, EventArgs e)
        {
            CatchBmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(CatchBmp);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));
            BackgroundImage = CatchBmp;
        }

        private void ExecCutImage()
        {
            Rectangle srcRect = new Rectangle(startPoint, new Size(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y));
            Rectangle destRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);
            Bitmap bmp = new Bitmap(srcRect.Width, srcRect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(CatchBmp, destRect, srcRect, GraphicsUnit.Pixel);
            Cache.scrImg = GetPicThumbnail(bmp, 60);
            Cache.scrImg = bmp;
            if (Cache.showByOCR != null)
            {
                Cache.showByOCR.Dispose();
            }
            Cache.showByOCR = new ShowByOCR();
            Cache.showByOCR.Show();
            Cache.existScreenshot = true;
            Cache.existOCR = false;
            Dispose();
        }

        public static Image GetPicThumbnail(Image iSource, int flag)
        {
            ImageFormat tFormat = iSource.RawFormat;
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100  
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                return iSource;
            }
            catch
            {
                List<string> list = new List<string>{ "Internet Explorer Server" , "OrpheusShadow", "DummyDWMListenerWindow", "EdgeUiInputTopWndClass", "Internet Explorer_Hidden","Internet Explorer_Hidden", "Progman"};
                return iSource;
            }
        }
    }
}
