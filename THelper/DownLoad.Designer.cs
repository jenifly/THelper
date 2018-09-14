namespace THelper
{
    partial class DownLoad
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownLoad));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.jTextBtn1 = new THelper.view.JTextBtn();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(310, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 26;
            this.label5.Text = "0.0%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(16, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 25;
            this.label4.Text = "0.00 MB/S";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(19, 174);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(364, 24);
            this.panel3.TabIndex = 22;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(0, 24);
            this.panel4.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(16, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(371, 139);
            this.label9.TabIndex = 20;
            this.label9.Text = resources.GetString("label9.Text");
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // jTextBtn1
            // 
            this.jTextBtn1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.jTextBtn1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.jTextBtn1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.jTextBtn1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.jTextBtn1.HoverColor = System.Drawing.Color.SeaGreen;
            this.jTextBtn1.Location = new System.Drawing.Point(158, 203);
            this.jTextBtn1.Name = "jTextBtn1";
            this.jTextBtn1.NormelColor = System.Drawing.Color.DarkSeaGreen;
            this.jTextBtn1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.jTextBtn1.PresslColor = System.Drawing.Color.DarkGreen;
            this.jTextBtn1.Size = new System.Drawing.Size(100, 26);
            this.jTextBtn1.TabIndex = 1006;
            this.jTextBtn1.Text = "下 载";
            this.jTextBtn1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.jTextBtn1.Click += new System.EventHandler(this.jTextBtn1_Click);
            // 
            // DownLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(403, 237);
            this.ControlBox = false;
            this.Controls.Add(this.jTextBtn1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label9);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainText = "下载更新";
            this.MainTextColor = System.Drawing.Color.DimGray;
            this.MainTextFont = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.MainTextLocation = new System.Drawing.Point(172, 2);
            this.MaximumSize = new System.Drawing.Size(1920, 1050);
            this.Name = "DownLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "THelper";
            this.TopBarColort = System.Drawing.Color.DarkSeaGreen;
            this.Load += new System.EventHandler(this.DownLoad_Load);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.jTextBtn1, 0);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label9;
        private view.JTextBtn jTextBtn1;
    }
}

