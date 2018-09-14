using System;
using System.Drawing;
using THelper.view;

namespace THelper
{
    public partial class Menu : JForm
    {
        private Main main;

        public Menu(Main main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void item_Click(object sender, EventArgs e)
        {
            main.menuOpen(((JTextBtn)sender).Tag.ToString());
            Dispose();
        }

        private void Menu_Deactivate(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Menu_Shown(object sender, EventArgs e)
        {
            Width = 100;
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
    }
}