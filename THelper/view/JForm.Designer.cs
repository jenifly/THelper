namespace THelper.view
{
    partial class JForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sysBtn_Min = new THelper.view.SysBtn();
            this.sysBtn_Menu = new THelper.view.SysBtn();
            this.sysBtn_Close = new THelper.view.SysBtn();
            this.SuspendLayout();
            // 
            // sysBtn_Min
            // 
            this.sysBtn_Min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sysBtn_Min.Image = global::THelper.Properties.Resources.icon_mini;
            this.sysBtn_Min.Location = new System.Drawing.Point(213, -222);
            this.sysBtn_Min.Name = "sysBtn_Min";
            this.sysBtn_Min.Size = new System.Drawing.Size(27, 22);
            this.sysBtn_Min.TabIndex = 3;
            this.sysBtn_Min.Text = "sysBtn4";
            this.sysBtn_Min.Click += new System.EventHandler(this.sysBtn_Min_Click);
            // 
            // sysBtn_Menu
            // 
            this.sysBtn_Menu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sysBtn_Menu.Image = global::THelper.Properties.Resources.icon_menu;
            this.sysBtn_Menu.Location = new System.Drawing.Point(186, -222);
            this.sysBtn_Menu.Name = "sysBtn_Menu";
            this.sysBtn_Menu.Size = new System.Drawing.Size(27, 22);
            this.sysBtn_Menu.TabIndex = 1;
            this.sysBtn_Menu.Text = "sysBtn2";
            this.sysBtn_Menu.Click += new System.EventHandler(this.sysBtn_Menu_Click);
            // 
            // sysBtn_Close
            // 
            this.sysBtn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sysBtn_Close.Image = global::THelper.Properties.Resources.icon_close;
            this.sysBtn_Close.Location = new System.Drawing.Point(267, -222);
            this.sysBtn_Close.Name = "sysBtn_Close";
            this.sysBtn_Close.Size = new System.Drawing.Size(27, 22);
            this.sysBtn_Close.TabIndex = 0;
            this.sysBtn_Close.Text = "sysBtn1";
            this.sysBtn_Close.Click += new System.EventHandler(this.sysBtn_Close_Click);
            // 
            // JForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(294, 222);
            this.Controls.Add(this.sysBtn_Min);
            this.Controls.Add(this.sysBtn_Menu);
            this.Controls.Add(this.sysBtn_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(20, 20);
            this.Name = "JForm";
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JForm_FormClosing);
            this.Load += new System.EventHandler(this.JForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SysBtn sysBtn_Close;
        private SysBtn sysBtn_Menu;
        private SysBtn sysBtn_Min;
    }
}