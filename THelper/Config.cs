using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;
using THelper.view;

namespace THelper
{
    public partial class Config : JForm
    {
        public Config()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            scrollPanel1.ShowScrollBar = true;
            configItem1.actived = true;
            panel1.BackColor = Cache.MNColor;
            panel2.BackColor = Cache.MainColor;
            scrollPanel1.BackColor = Cache.MainColor;
            checkBox1.Checked = Properties.Settings.Default.ATUOSTART;
            checkBox4.Checked = Properties.Settings.Default.MAINTOP;
            checkBox5.Checked = Properties.Settings.Default.GETWORDTOP;
            checkBox3.Checked = Properties.Settings.Default.ORCTOP;
            checkBox6.Checked = Properties.Settings.Default.NOTIFYICON;
            label16.BackColor = Cache.BaseColor;
            label18.BackColor = Cache.MainColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gainsboro), 1), 0, 26, 360, 26);
            base.OnPaint(e);
        }

        private void configItem(object sender, EventArgs e)
        {
            switch (((ConfigItem)sender).TabIndex)
            {
                case 0:
                    panel2.Top = -0;
                    break;
                case 1:
                    panel2.Top = -330;
                    break;
                case 2:
                    panel2.Top = -490;
                    break;
                case 3:
                    panel2.Top = -660;
                    break;
                case 4:
                    panel2.Top = -712;
                    break;
            }
            ChangeItemStatus(((ConfigItem)sender).TabIndex);
            scrollPanel1.ReSS();
        }

        private void panel2_LocationChanged(object sender, EventArgs e)
        {
            if (panel2.Top < -690)
            {
                if (!configItem5.actived)
                    ChangeItemStatus(4);
            }
            else if (panel2.Top < -650)
            {
                if (!configItem4.actived)
                    ChangeItemStatus(3);
            }
            else if (panel2.Top < -490)
            {
                if (!configItem3.actived)
                    ChangeItemStatus(2);

            }
            else if(panel2.Top < -330)
            {
                if (!configItem2.actived)
                    ChangeItemStatus(1);
            }
            else
            {
                if (!configItem1.actived)
                    ChangeItemStatus(0);
            }
        }

        private void ChangeItemStatus(int index)
        {
            ClearItemStatus();
            switch (index)
            {
                case 0:
                    configItem1.SetStatus(true);
                    break;
                case 1:
                    configItem2.SetStatus(true);
                    break;
                case 2:
                    configItem3.SetStatus(true);
                    break;
                case 3:
                    configItem4.SetStatus(true);
                    break;
                case 4:
                    configItem5.SetStatus(true);
                    break;
            }
        }

        private void ClearItemStatus()
        {
            if (configItem1.actived)
            {
                configItem1.SetStatus(false);
                return;
            }
            if (configItem2.actived)
            {
                configItem2.SetStatus(false);
                return;
            }
            if (configItem3.actived)
            {
                configItem3.SetStatus(false);
                return;
            }
            if (configItem4.actived)
            {
                configItem4.SetStatus(false);
                return;
            }
            if (configItem5.actived)
                configItem5.SetStatus(false);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.NOTIFYICON = checkBox6.Checked;
            Properties.Settings.Default.Save();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            ShowColorPick(label17, label16, 572);
        }

        private void ShowColorPick(Label label1, Label label, int offsetY)
        {
            JColorPick jColorPick = new JColorPick(label1, label);
            jColorPick.Location = new Point(Left + label.Left + 80, Top + panel2.Top + label.Height + offsetY);
            jColorPick.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            ShowColorPick(label19, label18, 603);
        }

        private void label23_Click(object sender, EventArgs e)
        {
            ShowColorPick(label24, label23, 699);
        }

        private void label25_Click(object sender, EventArgs e)
        {
            ShowColorPick(label26, label25, 740);
        }

        private void label17_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BASECOLOR = label17.Text;
            Cache.BaseColor = ColorTranslator.FromHtml(Properties.Settings.Default.BASECOLOR);
            Properties.Settings.Default.Save();
        }

        private void label19_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MAINCOLOR = label19.Text;
            Cache.MainColor = ColorTranslator.FromHtml(Properties.Settings.Default.MAINCOLOR);
            Properties.Settings.Default.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ATUOSTART = checkBox1.Checked;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.ATUOSTART)
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("JcShutdown", path);
                rk2.Close();
                rk.Close();
            }
            else
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.DeleteValue("JcShutdown", false);
                rk2.Close();
                rk.Close();
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void topMost_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void update_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void hotKey_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void defaultApi_Click(object sender, EventArgs e)
        {

        }

        private void delayed_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
