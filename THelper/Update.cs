using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using THelper.view;
using static THelper.Main;

namespace THelper
{

    public partial class Update : JForm
    {
        private Main main;
        private Dictionary<string, string> dict = new Dictionary<string, string>();

        public Update(Main main)
        {
            this.main = main;
            InitializeComponent();
        }

        private bool GetWebClient()
        {
            try
            {
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(Cache.updateUrl);
                StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("utf-8"));
                string strHTML = sr.ReadToEnd();
                myStream.Close();
                Console.WriteLine(strHTML);
                string[] strs = Regex.Split(strHTML, "-=-");
                for (int i = 0; i < strs.Length; i++)
                {
                    string[] s = Regex.Split(strs[i], "-:-");
                    dict.Add(s[0], s[1]);
                }
                return true;
            }
            catch
            {
                return false;
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
            main.openDownload(dict["file"]);
            Hide();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            jTextBtn1.NormelColor = Cache.BaseColor;
            jTextBtn1.HoverColor = Cache.HoverColor;
            jTextBtn1.PresslColor = Cache.PressColor;
            jTextBtn1.BackColor = Cache.BaseColor;
            panel3.BackColor = Cache.MainColor;
            label4.ForeColor = label6.ForeColor = Cache.BaseColor;
            label2.ForeColor = label8.ForeColor = label9.ForeColor = label3.ForeColor = label5.ForeColor = label10.ForeColor = Cache.TextColor;
            label8.Text = "当前版本：" + Cache.edition;
            label2.Text = "更新时间：" + Cache.updateTime;
            if (GetWebClient())
            {
                if (dict["edition"].Equals(Cache.edition))
                    panel3.Visible = false;
                else
                {
                    panel3.Visible = true;
                    label4.Text = "检查到新版本：" + dict["edition"];
                    label3.Text = "更新时间：" + dict["date"];
                    label5.Text = "大小：" + dict["size"];
                    label9.Text = dict["content"].Replace("-", Environment.NewLine);
                }
            }
            else
                panel3.Visible = false;
        }
    }
}
