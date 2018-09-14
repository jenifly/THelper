using System;
using System.Drawing;
using System.Windows.Forms;
using THelper.view;

namespace THelper
{
    public partial class JColorPick : JForm
    {
        private Label value, label;

        public JColorPick(Label value, Label label)
        {
            this.value = value;
            this.label = label;
            InitializeComponent();
        }

        private void Menu_Deactivate(object sender, EventArgs e)
        {
            Dispose();
        }

        private void JColorPick_Load(object sender, EventArgs e)
        {
            webBrowser1.ObjectForScripting = this;
            webBrowser1.DocumentText = Properties.Resources.index;
        }

        public void Message(string str)
        {
            label.BackColor = ColorTranslator.FromHtml("#" + str);
            value.Text = "#" + str.ToUpper();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            object[] objects = new object[1];
            objects[0] = value.Text.Replace("#", "");
            webBrowser1.Document.InvokeScript("setMessage", objects);
        }
    }
}