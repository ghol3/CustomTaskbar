namespace CustomTaskbar
{
    partial class Form1
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
            this.StartButtonPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // StartButtonPanel
            // 
            this.StartButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.StartButtonPanel.BackgroundImage = global::CustomTaskbar.Properties.Resources.StartButton_img;
            this.StartButtonPanel.Location = new System.Drawing.Point(2, 0);
            this.StartButtonPanel.Name = "StartButtonPanel";
            this.StartButtonPanel.Size = new System.Drawing.Size(59, 25);
            this.StartButtonPanel.TabIndex = 0;
            this.StartButtonPanel.Click += new System.EventHandler(this.StartButtonPanel_Click);
            this.StartButtonPanel.MouseLeave += new System.EventHandler(this.StartButtonPanel_MouseLeave);
            this.StartButtonPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StartButtonPanel_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 25);
            this.Controls.Add(this.StartButtonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel StartButtonPanel;
    }
}

