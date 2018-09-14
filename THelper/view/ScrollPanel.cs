using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace THelper.view
{
    public class ScrollPanel : Panel
    {
        [Browsable(true), Localizable(true), Category("JSkin"), Description("滚动条宽度")]
        public int ScrollBarWidth { set; get; } = 12;
        [Browsable(true), Localizable(true), Category("JSkin"), Description("BasePanel")]
        public Panel BasePanel { set; get; } = null;
        [Browsable(true), Localizable(true), Category("JSkin"), Description("BasePanel")]
        public bool ShowScrollBar { set; get; } = false;
        private ScrollBar scrollBar = null;

        public ScrollPanel()
        {
            SuspendLayout();
            ResumeLayout(false);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (BasePanel == null)
                return;
            if (BasePanel.Height <= Height)
                return;
            BasePanel.Top = Math.Min(Math.Max(Height - BasePanel.Height, BasePanel.Top + e.Delta / 2), 0);
            scrollBar.TopChange();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (!ShowScrollBar)
                return;
            if (scrollBar != null)
                return;
            scrollBar = new ScrollBar(this);
            scrollBar.Size = new Size(ScrollBarWidth, Height);
            scrollBar.Location = new Point(Width - ScrollBarWidth, 0);
            scrollBar.ScrollPanel = BasePanel;
            Controls.Add(scrollBar);
            scrollBar.BringToFront();
            scrollBar.BackColor = Color.Transparent;
        }

        protected override void OnResize(EventArgs e)
        {
            if (scrollBar == null)
                return;
            scrollBar.Size = new Size(ScrollBarWidth, Height);
            scrollBar.Location = new Point(Width - ScrollBarWidth, 0);
            scrollBar.ResizeScrollBar();
        }

        public void ReSS()
        {
            if (scrollBar == null)
                return;
            scrollBar.ResizeScrollBar();
        }
    }
}
