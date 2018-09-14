using System;
using System.Drawing;
using System.Windows.Forms;
using THelper.view;

namespace THelper
{

    public partial class Explain : JForm
    {

        public Explain()
        {
            InitializeComponent();
        }

        #region ControlBox
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.IndianRed;
        }

        private void ControlBox_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }

        #endregion
    }
}
