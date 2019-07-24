namespace FileLinesCompare
{
    partial class Compare
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
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnMaintainIgnore = new System.Windows.Forms.Button();
            this.txtProgress = new System.Windows.Forms.TextBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblCheckoutFolder = new System.Windows.Forms.Label();
            this.txtCheckoutFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(408, 12);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(116, 23);
            this.btnCompare.TabIndex = 4;
            this.btnCompare.Text = "Run Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnMaintainIgnore
            // 
            this.btnMaintainIgnore.Location = new System.Drawing.Point(33, 12);
            this.btnMaintainIgnore.Name = "btnMaintainIgnore";
            this.btnMaintainIgnore.Size = new System.Drawing.Size(116, 23);
            this.btnMaintainIgnore.TabIndex = 5;
            this.btnMaintainIgnore.Text = "Maintain Ignore List";
            this.btnMaintainIgnore.UseVisualStyleBackColor = true;
            this.btnMaintainIgnore.Click += new System.EventHandler(this.btnMaintainIgnore_Click);
            // 
            // txtProgress
            // 
            this.txtProgress.Location = new System.Drawing.Point(33, 139);
            this.txtProgress.Multiline = true;
            this.txtProgress.Name = "txtProgress";
            this.txtProgress.ReadOnly = true;
            this.txtProgress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtProgress.Size = new System.Drawing.Size(491, 344);
            this.txtProgress.TabIndex = 7;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(30, 123);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(51, 13);
            this.lblProgress.TabIndex = 8;
            this.lblProgress.Text = "Progress:";
            // 
            // lblCheckoutFolder
            // 
            this.lblCheckoutFolder.AutoSize = true;
            this.lblCheckoutFolder.Location = new System.Drawing.Point(30, 72);
            this.lblCheckoutFolder.Name = "lblCheckoutFolder";
            this.lblCheckoutFolder.Size = new System.Drawing.Size(85, 13);
            this.lblCheckoutFolder.TabIndex = 9;
            this.lblCheckoutFolder.Text = "Checkout folder:";
            // 
            // txtCheckoutFolder
            // 
            this.txtCheckoutFolder.Location = new System.Drawing.Point(122, 69);
            this.txtCheckoutFolder.Name = "txtCheckoutFolder";
            this.txtCheckoutFolder.Size = new System.Drawing.Size(402, 20);
            this.txtCheckoutFolder.TabIndex = 10;
            // 
            // Compare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 583);
            this.Controls.Add(this.txtCheckoutFolder);
            this.Controls.Add(this.lblCheckoutFolder);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.txtProgress);
            this.Controls.Add(this.btnMaintainIgnore);
            this.Controls.Add(this.btnCompare);
            this.Name = "Compare";
            this.Text = "Compare";
            this.Load += new System.EventHandler(this.Compare_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnMaintainIgnore;
        private System.Windows.Forms.TextBox txtProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblCheckoutFolder;
        private System.Windows.Forms.TextBox txtCheckoutFolder;
    }
}