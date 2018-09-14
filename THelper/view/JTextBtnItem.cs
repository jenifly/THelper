using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace THelper.view
{
    public class JTextBtnItem : Label
    {
        #region Config
        [Browsable(true), Category("JSkin"), Description("NormelColor")]
        public Color NormelColor { set; get; } = Color.White;
        [Browsable(true), Category("JSkin"), Description("HoverColor")]
        public Color HoverColor { set; get; } = Color.WhiteSmoke;
        [Browsable(true), Category("JSkin"), Description("PresslColor")]
        public Color PresslColor { set; get; } = Color.Gainsboro;
        #endregion

        private JForm jForm;
        private bool isApi;

        public JTextBtnItem(JForm jForm, bool isApi)
        {
            this.isApi = isApi;
            this.jForm = jForm;
            SuspendLayout();
            TextAlign = ContentAlignment.MiddleCenter;
            AutoSize = false;
            BackColor = NormelColor;
            Click += new EventHandler(this.JTextBtnItem_Click);
            ResumeLayout(false);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = HoverColor;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            BackColor = PresslColor;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if(Bounds.Contains(new Point(e.X + Left, e.Y + Top)))
                BackColor = HoverColor;
            else
                BackColor = NormelColor;
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = NormelColor;
            base.OnMouseLeave(e);
        }

        private void JTextBtnItem_Click(object sender, EventArgs e)
        {
            ((ChooseBox)jForm).TextChange(((JTextBtnItem)sender).Text, isApi);
            jForm.Dispose();
        }
    }
}
