using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using THelper.view;
using static THelper.Main;

namespace THelper
{

    public partial class DownLoad : JForm
    {
        private string url;
        private long lasttotalDownloadedByte = 0;
        private long totalDownloadedByte = 0;
        Timer timer = new Timer();

        public DownLoad(string url)
        {
            this.url = url;
            InitializeComponent();
            Console.WriteLine(url);
        }

        private void DownLoad_Load(object sender, EventArgs e)
        {
            label9.ForeColor = label4.ForeColor = label5.ForeColor = Cache.TextColor;
            panel4.BackColor = Cache.BaseColor;
            jTextBtn1.NormelColor = Cache.BaseColor;
            jTextBtn1.HoverColor = Cache.HoverColor;
            jTextBtn1.PresslColor = Cache.PressColor;
            jTextBtn1.BackColor = Cache.BaseColor;
            timer1.Enabled = true;
            timer1.Interval = 500;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           long speed = (totalDownloadedByte - lasttotalDownloadedByte) * 2;
            lasttotalDownloadedByte = totalDownloadedByte;
            if (speed.ToString().Length > 6)
            {
                label4.Text = (speed / 1048576).ToString("f2") + " MB/S";
                return;
            }
            if(speed.ToString().Length > 3)
            {
                label4.Text = (speed / 1024).ToString("f2") + " KB/S";
                return;
            }
            label4.Text = speed.ToString("f2") + " B/S";
        }

        public void DownloadFile(string URL, string filename)
        {
            float percent = 0;
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(URL);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                Stream st = myrp.GetResponseStream();
                Stream so = new FileStream(filename, FileMode.Create);
                
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    Application.DoEvents();
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, by.Length);

                    percent = totalDownloadedByte / (float)totalBytes;
                    panel4.Width = (int)(panel3.Width * percent);
                    percent *= 100;
                    label5.Text = percent.ToString("f1") + "%";
                    Application.DoEvents();
                }
                so.Close();
                st.Close();
            }
            catch (Exception)
            {
                throw;
            }
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
        private void jTextBtn1_Click(object sender, EventArgs e)
        {
            string s = Application.ExecutablePath;
            if (jTextBtn1.Text.Equals("下载完成"))
            {
                Application.Exit();
                string bat2 = "@echo 来自THelper的消息：更新成功！请按任意键退出&pause>nul\r\ndel %0\r\nexit";
                File.WriteAllText("show.bat", bat2, Encoding.GetEncoding(936));
                string bat1 = string.Format(
                    ":del\r\n" +
                    " del \"{0}\"\r\n" +
                    "if exist \"{0}\" goto del\r\n" +
                    "ren THelper_Download_file.exe {0}\r\n" +
                    "start \"\" {0}\r\n" +
                    "del %0", Path.GetFileName(s));
                File.WriteAllText("run.bat", bat1, Encoding.GetEncoding(936));
                Process.Start("run.bat");
                Process.Start("show.bat");
            }
            else
            {
                jTextBtn1.Text = "下载中...";
                timer1.Start();
                DownloadFile(url, Path.GetDirectoryName(s) + "\\THelper_Download_file.exe");
                jTextBtn1.Text = "下载完成";
                timer1.Stop();
            }
        }
    }
}
