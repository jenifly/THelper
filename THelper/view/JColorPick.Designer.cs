﻿namespace THelper
{
    partial class JColorPick
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JColorPick));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, -21);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(232, 142);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(232, 142);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // JColorPick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CanDrag = false;
            this.ClientSize = new System.Drawing.Size(232, 123);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(232, 123);
            this.MinimumSize = new System.Drawing.Size(232, 123);
            this.Name = "JColorPick";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowSys_Close = false;
            this.ShowTopBar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "THelper";
            this.TopBarColort = System.Drawing.Color.White;
            this.TopBarHeight = 1;
            this.Deactivate += new System.EventHandler(this.Menu_Deactivate);
            this.Load += new System.EventHandler(this.JColorPick_Load);
            this.Controls.SetChildIndex(this.webBrowser1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

