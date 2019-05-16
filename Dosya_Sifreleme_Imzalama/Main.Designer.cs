namespace Dosya_Sifreleme_Imzalama
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dosyaAc = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.user = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.make = new System.Windows.Forms.Button();
            this.crypto = new System.Windows.Forms.CheckBox();
            this.signature = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dosyaIslem = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyaAc});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(407, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyaAc
            // 
            this.dosyaAc.Name = "dosyaAc";
            this.dosyaAc.Size = new System.Drawing.Size(68, 20);
            this.dosyaAc.Text = "Dosya Aç";
            this.dosyaAc.Click += new System.EventHandler(this.dosyaAc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kullanıcı Seç :";
            // 
            // user
            // 
            this.user.FormattingEnabled = true;
            this.user.Items.AddRange(new object[] {
            "A",
            "B"});
            this.user.Location = new System.Drawing.Point(86, 29);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(121, 21);
            this.user.TabIndex = 2;
            this.user.Text = "A";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.make);
            this.panel1.Controls.Add(this.crypto);
            this.panel1.Controls.Add(this.signature);
            this.panel1.Location = new System.Drawing.Point(12, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 78);
            this.panel1.TabIndex = 7;
            this.panel1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "İşlem          :";
            // 
            // make
            // 
            this.make.Location = new System.Drawing.Point(71, 33);
            this.make.Name = "make";
            this.make.Size = new System.Drawing.Size(172, 23);
            this.make.TabIndex = 10;
            this.make.Text = "Yap";
            this.make.UseVisualStyleBackColor = true;
            this.make.Click += new System.EventHandler(this.make_Click);
            // 
            // crypto
            // 
            this.crypto.AutoSize = true;
            this.crypto.Location = new System.Drawing.Point(176, 6);
            this.crypto.Name = "crypto";
            this.crypto.Size = new System.Drawing.Size(55, 17);
            this.crypto.TabIndex = 9;
            this.crypto.Text = "Şifrele";
            this.crypto.UseVisualStyleBackColor = true;
            // 
            // signature
            // 
            this.signature.AutoSize = true;
            this.signature.Checked = true;
            this.signature.CheckState = System.Windows.Forms.CheckState.Checked;
            this.signature.Location = new System.Drawing.Point(74, 6);
            this.signature.Name = "signature";
            this.signature.Size = new System.Drawing.Size(56, 17);
            this.signature.TabIndex = 8;
            this.signature.Text = "İmzala";
            this.signature.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "İşlem Seç     :";
            // 
            // dosyaIslem
            // 
            this.dosyaIslem.FormattingEnabled = true;
            this.dosyaIslem.Items.AddRange(new object[] {
            "Dosya Al",
            "Dosya Gönder"});
            this.dosyaIslem.Location = new System.Drawing.Point(86, 56);
            this.dosyaIslem.Name = "dosyaIslem";
            this.dosyaIslem.Size = new System.Drawing.Size(121, 21);
            this.dosyaIslem.TabIndex = 8;
            this.dosyaIslem.Text = "Dosya Gönder";
            this.dosyaIslem.SelectedValueChanged += new System.EventHandler(this.dosyaIslem_SelectedValueChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 377);
            this.Controls.Add(this.dosyaIslem);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.user);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Dosya Şifreleme VE İmzalama";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaAc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox user;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button make;
        private System.Windows.Forms.CheckBox crypto;
        private System.Windows.Forms.CheckBox signature;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox dosyaIslem;
    }
}

