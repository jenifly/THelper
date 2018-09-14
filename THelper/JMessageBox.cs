using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using THelper.view;
using static THelper.Main;

namespace THelper
{

    public partial class JMessageBox : JForm
    {
        private int type;

        public JMessageBox(String title, String context, int type)
        {
            this.type = type;
            InitializeComponent();
            MainText = title;
            label2.Text = context;
            switch (type)
            {
                case 0:
                    jTextBtn2.Visible = false;
                    jTextBtn1.Left = 114;
                    break;
                case 1:
                    break;
            }
        }

        #region Button
        private void jTextBtn2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void jTextBtn1_Click(object sender, EventArgs e)
        {
            HotKey.UnregisterHotKey(Handle, 100);
            HotKey.UnregisterHotKey(Handle, 101);
            HotKey.UnregisterHotKey(Handle, 102);
            Cache.exist = true;
            Dispose();
        }
       
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.SeaGreen;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.DarkSeaGreen;
        }
        #endregion

        private void JMessageBox_Load(object sender, EventArgs e)
        {
            jTextBtn1.NormelColor = Cache.BaseColor;
            jTextBtn1.HoverColor = Cache.HoverColor;
            jTextBtn1.PresslColor = Cache.PressColor;
            jTextBtn1.BackColor = Cache.BaseColor;
            jTextBtn2.NormelColor = Cache.BaseColor;
            jTextBtn2.HoverColor = Cache.HoverColor;
            jTextBtn2.PresslColor = Cache.PressColor;
            jTextBtn2.BackColor = Cache.BaseColor;
            switch (type)
            {
                case 0:
                    Rectangle screenArea = Screen.GetWorkingArea(this);
                    Location = (Point)new Size(screenArea.Width / 2 - Width / 2, screenArea.Height / 2 - Height / 2);
                    break;
                case 1:
                    Location = new Point(Cache.mainPoint.X + Cache.mainSize.Width / 2 - Width / 2, Cache.mainPoint.Y + Cache.mainSize.Height / 2 - Height / 2);
                    break;
            }
            
        }
    }
}
