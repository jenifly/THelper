namespace THelper
{
    partial class JMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JMessageBox));
            this.label2 = new System.Windows.Forms.Label();
            this.jTextBtn1 = new THelper.view.JTextBtn();
            this.jTextBtn2 = new THelper.view.JTextBtn();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 46);
            this.label2.TabIndex = 1;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // jTextBtn1
            // 
            this.jTextBtn1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.jTextBtn1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.jTextBtn1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.jTextBtn1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.jTextBtn1.HoverColor = System.Drawing.Color.SeaGreen;
            this.jTextBtn1.Location = new System.Drawing.Point(45, 84);
            this.jTextBtn1.Name = "jTextBtn1";
            this.jTextBtn1.NormelColor = System.Drawing.Color.DarkSeaGreen;
            this.jTextBtn1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.jTextBtn1.PresslColor = System.Drawing.Color.DarkGreen;
            this.jTextBtn1.Size = new System.Drawing.Size(80, 24);
            this.jTextBtn1.TabIndex = 1005;
            this.jTextBtn1.Text = "确定";
            this.jTextBtn1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.jTextBtn1.Click += new System.EventHandler(this.jTextBtn1_Click);
            // 
            // jTextBtn2
            // 
            this.jTextBtn2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.jTextBtn2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.jTextBtn2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.jTextBtn2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.jTextBtn2.HoverColor = System.Drawing.Color.SeaGreen;
            this.jTextBtn2.Location = new System.Drawing.Point(160, 84);
            this.jTextBtn2.Name = "jTextBtn2";
            this.jTextBtn2.NormelColor = System.Drawing.Color.DarkSeaGreen;
            this.jTextBtn2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.jTextBtn2.PresslColor = System.Drawing.Color.DarkGreen;
            this.jTextBtn2.Size = new System.Drawing.Size(80, 24);
            this.jTextBtn2.TabIndex = 1006;
            this.jTextBtn2.Text = "取消";
            this.jTextBtn2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.jTextBtn2.Click += new System.EventHandler(this.jTextBtn2_Click);
            // 
            // JMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(291, 117);
            this.ControlBox = false;
            this.Controls.Add(this.jTextBtn2);
            this.Controls.Add(this.jTextBtn1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainText = "提示";
            this.MainTextColor = System.Drawing.Color.DimGray;
            this.MainTextFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.MainTextLocation = new System.Drawing.Point(128, 2);
            this.MaximumSize = new System.Drawing.Size(1920, 1050);
            this.Name = "JMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "THelper";
            this.TopBarColort = System.Drawing.Color.DarkSeaGreen;
            this.Load += new System.EventHandler(this.JMessageBox_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.jTextBtn1, 0);
            this.Controls.SetChildIndex(this.jTextBtn2, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private view.JTextBtn jTextBtn1;
        private view.JTextBtn jTextBtn2;
    }
}

