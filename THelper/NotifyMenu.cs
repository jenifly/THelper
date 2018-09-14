using System;
using System.Drawing;
using System.Windows.Forms;
using THelper.view;

namespace THelper
{
    public partial class NotifyMenu : JForm
    {

        public NotifyMenu()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x0008)
            {
                Console.WriteLine("111");
            }
            base.WndProc(ref m);
        }

        private void item_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Menu_Deactivate(object sender, EventArgs e)
        {
            Dispose();
            Console.WriteLine("111");
        }

        private void Menu_Shown(object sender, EventArgs e)
        {
            Width = 80;
        }

        private void jTextBtn1_MouseEnter(object sender, EventArgs e)
        {
            TopBarColorChanged(jTextBtn1.HoverColor);
        }

        private void jTextBtn1_MouseLeave(object sender, EventArgs e)
        {
            TopBarColorChanged(jTextBtn1.NormelColor);
        }

        private void jTextBtn1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TopBarColorChanged(jTextBtn1.PresslColor);
        }

        private void jTextBtn1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(jTextBtn1.Bounds.Contains(e.Location))
                TopBarColorChanged(jTextBtn1.HoverColor);
            else
                TopBarColorChanged(jTextBtn1.NormelColor);
        }

        private void NotifyMenu_Load(object sender, EventArgs e)
        {
            Location = new Point(MousePosition.X, MousePosition.Y - Height);
        }
    }
}