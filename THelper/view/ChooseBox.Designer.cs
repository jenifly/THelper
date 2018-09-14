namespace THelper
{
    partial class ChooseBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseBox));
            this.scrollPanel1 = new THelper.view.ScrollPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scrollPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollPanel1
            // 
            this.scrollPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollPanel1.BasePanel = this.panel1;
            this.scrollPanel1.Controls.Add(this.panel1);
            this.scrollPanel1.Location = new System.Drawing.Point(0, 0);
            this.scrollPanel1.Name = "scrollPanel1";
            this.scrollPanel1.ScrollBarWidth = 8;
            this.scrollPanel1.Size = new System.Drawing.Size(22, 19);
            this.scrollPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(70, 10);
            this.panel1.TabIndex = 0;
            // 
            // ChooseBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CanDrag = false;
            this.ClientSize = new System.Drawing.Size(20, 20);
            this.Controls.Add(this.scrollPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1920, 1050);
            this.Name = "ChooseBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowSys_Close = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "THelper";
            this.TopBarColort = System.Drawing.Color.White;
            this.TopBarHeight = 1;
            this.Deactivate += new System.EventHandler(this.Menu_Deactivate);
            this.Load += new System.EventHandler(this.ChooseBox_Load);
            this.Shown += new System.EventHandler(this.Menu_Shown);
            this.Controls.SetChildIndex(this.scrollPanel1, 0);
            this.scrollPanel1.ResumeLayout(false);
            this.scrollPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private view.ScrollPanel scrollPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}

