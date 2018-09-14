using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace THelper.view
{
    public class SysBtn : Control
    {
        [Browsable(true), Category("JSkin"), Description("图标")]
        public Image Image { set; get; }

        private int status = 0;

        private void InitializeComponent()
        {
            SuspendLayout();
            Size = new Size(27, 22);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Image == null)
                return;
            DrawRect(e.Graphics, Image, new Rectangle(new Point(0, 0), Size), status);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            status = 1;
            Invalidate();

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            status = 0;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            status = 2;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Bounds.Contains(e.Location))
                status = 1;
            else
                status = 0;
            Invalidate();
        }

        public static void DrawRect(Graphics g, Image img, Rectangle rect, int index)
        {
            int width = img.Width / 3;
            int height = img.Height;
            int x = index * width;
            int y = 0;
            Rectangle r = new Rectangle(x, y, width, height);
            g.DrawImage(img, rect, r, GraphicsUnit.Pixel);
        }
    }
}
