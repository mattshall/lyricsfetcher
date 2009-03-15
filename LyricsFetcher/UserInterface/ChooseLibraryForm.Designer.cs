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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseLibraryForm));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.radioButtonITunes = new System.Windows.Forms.RadioButton();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.radioButtonWmp = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(219, 122);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(97, 29);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(115, 122);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(97, 29);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // radioButtonITunes
            // 
            this.radioButtonITunes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButtonITunes.ImageKey = "iTunes";
            this.radioButtonITunes.ImageList = this.imageList32;
            this.radioButtonITunes.Location = new System.Drawing.Point(17, 30);
            this.radioButtonITunes.Name = "radioButtonITunes";
            this.radioButtonITunes.Size = new System.Drawing.Size(299, 40);
            this.radioButtonITunes.TabIndex = 2;
            this.radioButtonITunes.TabStop = true;
            this.radioButtonITunes.Text = "iTunes";
            this.radioButtonITunes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radioButtonITunes.UseVisualStyleBackColor = true;
            this.radioButtonITunes.CheckedChanged += new System.EventHandler(this.radioButtonITunes_CheckedChanged);
            // 
            // imageList32
            // 
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32.Images.SetKeyName(0, "WMP");
            this.imageList32.Images.SetKeyName(1, "iTunes");
            // 
            // radioButtonWmp
            // 
            this.radioButtonWmp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButtonWmp.ImageKey = "WMP";
            this.radioButtonWmp.ImageList = this.imageList32;
            this.radioButtonWmp.Location = new System.Drawing.Point(17, 76);
            this.radioButtonWmp.Name = "radioButtonWmp";
            this.radioButtonWmp.Size = new System.Drawing.Size(298, 40);
            this.radioButtonWmp.TabIndex = 3;
            this.radioButtonWmp.TabStop = true;
            this.radioButtonWmp.Text = "Windows Media Player";
            this.radioButtonWmp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radioButtonWmp.UseVisualStyleBackColor = true;
            this.radioButtonWmp.CheckedChanged += new System.EventHandler(this.radioButtonWmp_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose the library for which you want to find lyrics:";
            // 
            // ChooseLibraryForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(327, 164);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonWmp);
            this.Controls.Add(this.radioButtonITunes);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
        private System.Windows.Forms.ImageList imageList32;
    }
}