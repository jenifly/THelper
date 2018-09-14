using System;
using System.Collections.Generic;
using System.Drawing;
using THelper.view;

namespace THelper
{
    public partial class ChooseBox : JForm
    {
        private Dictionary<string, string> dict;
        private int width, height;
        private int fontSize;
        private Font font;
        private JForm jForm;
        private bool isApi, type;

        public ChooseBox(JForm jForm,bool isApi, Dictionary<string, string> dict, int width, int fontSize, bool type)
        {
            this.jForm = jForm;
            this.isApi = isApi;
            this.type = type;
            this.dict = dict;
            this.width = width;
            this.fontSize = fontSize;
            InitializeComponent();
            if (dict.Count > 8)
                scrollPanel1.ShowScrollBar = true;
        }

        private void item_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Menu_Deactivate(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Menu_Shown(object sender, EventArgs e)
        {
            Width = width;
            Height = height * Math.Min(dict.Count, 8);
        }

        private void ChooseBox_Load(object sender, EventArgs e)
        {
            panel1.Width = width;
            font = new Font(new FontFamily("微软雅黑"), fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            height = (int)CreateGraphics().MeasureString("微", font).Height + 3;
            int index = 0;
            foreach (KeyValuePair<string, string> pair in dict)
            {
                JTextBtnItem jTextBtn = new JTextBtnItem(this, isApi);
                jTextBtn.TextAlign = ContentAlignment.MiddleLeft;
                jTextBtn.Font = font;
                jTextBtn.ForeColor = Color.DimGray;
                jTextBtn.Size = new Size(width, height);
                jTextBtn.NormelColor = Color.Transparent;
                jTextBtn.HoverColor = Color.Gainsboro;
                jTextBtn.PresslColor = Color.LightGray;
                jTextBtn.Text = pair.Key;
                jTextBtn.Top = height * index;
                panel1.Controls.Add(jTextBtn);
                index++;
            }
            panel1.Height = height * index;
            scrollPanel1.ReSS();
        }

        public void TextChange(string str,bool isApi)
        {
            if (type)
                ((Main)jForm).TextChang(str, isApi);
            else
                ((TJForm)jForm).TextChang(str, isApi);
        }
    }
}