using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using THelper.translate;

namespace THelper.view
{
    public partial class TJForm : JForm
    {
        private Font font;
        private SolidBrush brush1 = new SolidBrush(Cache.TextColor);
        private SolidBrush brush2 = new SolidBrush(Cache.BaseColor);
        private string tranApi = "百度翻译";
        private string lanTo = "英语";
        public string tranApiVaule = "百度Baidu";
        public string lanToVaule = "en";
        private Rectangle rect1, rect2;
        private int index = -1;

        public TJForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            font = new Font(new FontFamily("微软雅黑"), 10, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        protected override void WndProc(ref Message m)
        {
            Point vPoint = new Point(MousePosition.X - Left, MousePosition.Y - Top);
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    if (rect1.Contains(vPoint))
                    {
                        if (Cursor != Cursors.Hand)
                            Cursor = Cursors.Hand;
                        return;
                    }
                    if (rect2.Contains(vPoint))
                    {
                        if (Cursor != Cursors.Hand)
                            Cursor = Cursors.Hand;
                        return;
                    }
                    if (Cursor != Cursors.Default)
                        Cursor = Cursors.Default;
                    break;
                case 0x0201:
                    if (rect1.Contains(vPoint))
                    {
                        index = 1;
                        return;
                    }
                    if (rect2.Contains(vPoint))
                    {
                        index = 2;
                        return;
                    }
                    index = -1;
                    m.Msg = 0x00A1;
                    m.WParam = new IntPtr(2);
                    base.WndProc(ref m);
                    break;
                case 0x202:
                    
                    if (index == 1 && rect1.Contains(vPoint))
                    {
                        ChooseBox chooseBox = new ChooseBox(this,true, TDictionary.BaseDict, 50, 10, false);
                        chooseBox.Location = new Point(Left + rect1.X, Top + rect1.Y + rect1.Height);
                        chooseBox.Show();
                    }
                    if (index == 2 && rect2.Contains(vPoint))
                    {
                        Dictionary<string, string> d = TDictionary.langDic[tranApiVaule];
                        if (d.ContainsKey("自动检测"))
                            d.Remove("自动检测");
                        ChooseBox chooseBox = new ChooseBox(this, false, d, 70, 10, false);
                        chooseBox.Location = new Point(Left + rect2.X, Top + rect2.Y + rect2.Height);
                        chooseBox.Show();
                    }
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Point point = new Point(8, 6);
            Graphics g = e.Graphics;
            g.DrawString("来自", font, brush1, point);
            point.X += (int)g.MeasureString("来自", font).Width + 3;
            g.DrawString(tranApi, font, brush2, point);
            rect1 = new Rectangle(point.X, 6, (int)g.MeasureString(tranApi, font).Width, 14);
            point.X += rect1.Width + 3;
            g.DrawString("目标语言", font, brush1, point);
            point.X += (int)g.MeasureString("目标语言", font).Width + 3;
            g.DrawString(lanTo, font, brush2, point);
            rect2 = new Rectangle(point.X, 6, (int)g.MeasureString(lanTo, font).Width, 14);
            base.OnPaint(e);
        }

        public void TextChang(string str, bool isApi)
        {
            if (isApi)
                tranApi = str;
            else
                lanTo = str;
            tranApiVaule = TDictionary.BaseDict[tranApi];
            lanToVaule = TDictionary.langDic[tranApiVaule][lanTo];
            Invalidate();
        }
    }
}
