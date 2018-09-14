using System;
using System.Drawing;
using static THelper.Main;
using System.Windows.Forms;
using THelper.view;

namespace THelper
{

    public partial class Tip : JForm
    {
        private int count = 0;

        public Tip(string content)
        {
            InitializeComponent();
            Location = new Point(Cache.mainPoint.X + Cache.mainSize.Width / 2 - Width / 2, Cache.mainPoint.Y + Cache.mainSize.Height / 2 - Height / 2);
            label9.Text = content;
            label9.ForeColor = Cache.TextColor;
            Width = label9.Width + 8;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            if(count > 8)
            {
                Opacity -= 0.1;
                if (Opacity <= 0.1)
                {
                    timer1.Stop();
                    Dispose();
                }
            }
        }
    }
}
