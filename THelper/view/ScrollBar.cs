using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace THelper.view
{
    public class ScrollBar : Panel
    {
        private int scrollBlockWidth = -1;
        private int scrollBlockHeight = -1;
        private static Dictionary<string, Color> BrushColor = new Dictionary<string, Color> {
            { "mouseEnter", Color.LightGray }, { "default", Color.Gainsboro },
            { "mouseAllEnter", Color.Silver },{ "mouseDown", Color.DarkGray } };
        private Brush brush = new SolidBrush(BrushColor["default"]);
        private GraphicsPath gp = null;
        private bool mouseDown = false, mouseAllEnter = false;
        private int scrollTop = 0;
        private int last_Y = -1;
        private double sacle;

        #region  Config
        [Browsable(true), Localizable(true), Category("JSkin"), Description("要滚动的Panel"), DefaultValue(null)]
        public Panel ScrollPanel { set; get; }
        private Panel BasePanel { set; get; }
        #endregion  

        public ScrollBar(Panel BasePane)
        {
            BasePanel = BasePanel;
            SuspendLayout();
            ResumeLayout(false);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        public void TopChange()
        {
            scrollTop = -(int)(ScrollPanel.Top / sacle);
            Invalidate();
        }

        public void ResizeScrollBar()
        {
            if (ScrollPanel == null)
                return;
            sacle = (double)ScrollPanel.Height / Height;
            scrollBlockWidth = Width - 2;
            scrollBlockHeight = Math.Max((int)(Height / sacle), 36);
            scrollTop = -(int)(ScrollPanel.Top / sacle);
            gp = new GraphicsPath();
            Rectangle rectangle = new Rectangle(0, 0, scrollBlockWidth, scrollBlockWidth);
            gp.AddArc(rectangle, 0, -180);
            gp.AddLine(rectangle.Right, rectangle.Bottom - scrollBlockWidth / 2, rectangle.Right, rectangle.Top + scrollBlockHeight - scrollBlockWidth);
            rectangle.Y += scrollBlockHeight - scrollBlockWidth;
            gp.AddArc(rectangle, 0, 180);
            gp.AddLine(rectangle.Left, rectangle.Bottom - scrollBlockWidth / 2, rectangle.Left, rectangle.Bottom - scrollBlockHeight + scrollBlockWidth / 2);
            gp.CloseFigure();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (scrollBlockWidth == -1)
                ResizeScrollBar();
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TranslateTransform(0, scrollTop);
            g.FillPath(brush, gp);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            brush = new SolidBrush(BrushColor["mouseEnter"]);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            brush = new SolidBrush(BrushColor["default"]);
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (mouseDown)
            {
                scrollTop = Math.Max(0, Math.Min(Height - scrollBlockHeight, e.Y - last_Y));
                AdjustPanelVScroll();
                Invalidate();
            }
            else
            {
                if (0 < e.Y - scrollTop && e.Y - scrollTop < scrollBlockHeight)
                {
                    if (!mouseAllEnter)
                    {
                        mouseAllEnter = true;
                        brush = new SolidBrush(BrushColor["mouseAllEnter"]);
                        Invalidate();
                    }
                }
                else
                {
                    if (mouseAllEnter)
                    {
                        brush = new SolidBrush(BrushColor["mouseEnter"]);
                        mouseAllEnter = false;
                        Invalidate();
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Bounds.Contains(e.Location))
                brush = new SolidBrush(BrushColor["mouseEnter"]);
            else if (0 < e.Y - scrollTop && e.Y - scrollTop < scrollBlockHeight)
                brush = new SolidBrush(BrushColor["mouseEnter"]);
            else
                brush = new SolidBrush(BrushColor["default"]);
            mouseDown = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (0 < e.Y - scrollTop && e.Y - scrollTop < scrollBlockHeight)
            {
                brush = new SolidBrush(BrushColor["mouseDown"]);
                mouseDown = true;
                last_Y = e.Y - scrollTop;
            }
            else
            {
                if (e.Y - scrollTop > scrollBlockHeight)
                    scrollTop += scrollBlockHeight * 4 / 5;
                if (e.Y - scrollTop < 0)
                    scrollTop -= scrollBlockHeight * 4 / 5;
                scrollTop = Math.Max(0, Math.Min(scrollTop, Height - scrollBlockHeight));
                AdjustPanelVScroll();
            }
            Invalidate();
        }

        private void AdjustPanelVScroll()
        {
            ScrollPanel.Top = Math.Max(Height - ScrollPanel.Height, -(int)(scrollTop * sacle));
        }
    }
}
