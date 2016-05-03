namespace DRMS.EditorBox
{
    partial class OpenForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.checkBox_delxml = new System.Windows.Forms.CheckBox();
            this.label_Close = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.White;
            this.lblMsg.Location = new System.Drawing.Point(10, 35);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(53, 12);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "文件信息";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(259, 35);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(23, 12);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "0/0";
            // 
            // buttonOpen
            // 
            this.buttonOpen.BackColor = System.Drawing.SystemColors.Highlight;
            this.buttonOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpen.ForeColor = System.Drawing.Color.White;
            this.buttonOpen.Location = new System.Drawing.Point(396, 50);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(54, 23);
            this.buttonOpen.TabIndex = 4;
            this.buttonOpen.Text = "打 开";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // checkBox_delxml
            // 
            this.checkBox_delxml.AutoSize = true;
            this.checkBox_delxml.ForeColor = System.Drawing.Color.White;
            this.checkBox_delxml.Location = new System.Drawing.Point(12, 12);
            this.checkBox_delxml.Name = "checkBox_delxml";
            this.checkBox_delxml.Size = new System.Drawing.Size(270, 16);
            this.checkBox_delxml.TabIndex = 1;
            this.checkBox_delxml.Text = "删除本地xml文件（上次未提交，建议不勾选）";
            this.checkBox_delxml.UseVisualStyleBackColor = true;
            // 
            // label_Close
            // 
            this.label_Close.AutoSize = true;
            this.label_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_Close.Location = new System.Drawing.Point(421, 9);
            this.label_Close.Name = "label_Close";
            this.label_Close.Size = new System.Drawing.Size(29, 12);
            this.label_Close.TabIndex = 6;
            this.label_Close.Text = "关闭";
            this.label_Close.Click += new System.EventHandler(this.label_Close_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 50);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(378, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(462, 80);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_Close);
            this.Controls.Add(this.checkBox_delxml);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OpenForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打开编辑器";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OpenForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.CheckBox checkBox_delxml;
        private System.Windows.Forms.Label label_Close;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

