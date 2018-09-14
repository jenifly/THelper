using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace THelper.view
{
    public class ConfigItem : Label
    {
        public bool actived = false;
        private bool j = true;

        public ConfigItem()
        {
            SuspendLayout();
            TextAlign = ContentAlignment.MiddleCenter;
            ResumeLayout(false);
        }

        public void SetStatus(bool actived)
        {
            this.actived = actived;
            if (actived)
                BackColor = Cache.MainColor;
            else
                BackColor = Cache.MNColor;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (j)
            {
                if (actived)
                    BackColor = Cache.MainColor;
                else
                    BackColor = Cache.MNColor;
                j = false;
            }
            if (actived)
                e.Graphics.FillRectangle(new SolidBrush(Cache.BaseColor), 0, 0, 5, Height);
            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!actived)
            {
                BackColor = Cache.MHColor;
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!actived)
            {
                BackColor = Cache.MNColor;
            }
            base.OnMouseLeave(e);
        }
    }
}
