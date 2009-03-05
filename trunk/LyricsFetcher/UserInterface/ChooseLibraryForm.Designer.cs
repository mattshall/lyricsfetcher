namespace LyricsFetcher
{
    partial class ChooseLibraryForm
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
            if (disposing && (components != null)) {
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.radioButtonITunes = new System.Windows.Forms.RadioButton();
            this.radioButtonWmp = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(236, 113);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(143, 113);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // radioButtonITunes
            // 
            this.radioButtonITunes.AutoSize = true;
            this.radioButtonITunes.Location = new System.Drawing.Point(18, 45);
            this.radioButtonITunes.Name = "radioButtonITunes";
            this.radioButtonITunes.Size = new System.Drawing.Size(62, 19);
            this.radioButtonITunes.TabIndex = 2;
            this.radioButtonITunes.TabStop = true;
            this.radioButtonITunes.Text = "iTunes";
            this.radioButtonITunes.UseVisualStyleBackColor = true;
            this.radioButtonITunes.CheckedChanged += new System.EventHandler(this.radioButtonITunes_CheckedChanged);
            // 
            // radioButtonWmp
            // 
            this.radioButtonWmp.AutoSize = true;
            this.radioButtonWmp.Location = new System.Drawing.Point(18, 70);
            this.radioButtonWmp.Name = "radioButtonWmp";
            this.radioButtonWmp.Size = new System.Drawing.Size(157, 19);
            this.radioButtonWmp.TabIndex = 3;
            this.radioButtonWmp.TabStop = true;
            this.radioButtonWmp.Text = "Windows Media Player";
            this.radioButtonWmp.UseVisualStyleBackColor = true;
            this.radioButtonWmp.CheckedChanged += new System.EventHandler(this.radioButtonWmp_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(311, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose the library for which library you want lyrics:";
            // 
            // ChooseLibraryForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(339, 153);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonWmp);
            this.Controls.Add(this.radioButtonITunes);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseLibraryForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Choose Library";
            this.Load += new System.EventHandler(this.ChooseLibraryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RadioButton radioButtonITunes;
        private System.Windows.Forms.RadioButton radioButtonWmp;
        private System.Windows.Forms.Label label1;
    }
}