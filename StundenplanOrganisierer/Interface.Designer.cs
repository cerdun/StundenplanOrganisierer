namespace StundenplanOrganisierer
{
    partial class StuPlOrgInterface
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.Pfad = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.from = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.load = new System.Windows.Forms.Button();
            this.DB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(644, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Zerteilen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Pfad
            // 
            this.Pfad.Location = new System.Drawing.Point(12, 9);
            this.Pfad.Name = "Pfad";
            this.Pfad.ReadOnly = true;
            this.Pfad.Size = new System.Drawing.Size(626, 20);
            this.Pfad.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 328);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(707, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // from
            // 
            this.from.AutoSize = true;
            this.from.Location = new System.Drawing.Point(644, 61);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(13, 13);
            this.from.TabIndex = 4;
            this.from.Text = "0";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(644, 9);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(75, 20);
            this.load.TabIndex = 5;
            this.load.Text = "Laden";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // DB
            // 
            this.DB.Location = new System.Drawing.Point(12, 32);
            this.DB.Multiline = true;
            this.DB.Name = "DB";
            this.DB.Size = new System.Drawing.Size(319, 287);
            this.DB.TabIndex = 6;
            this.DB.Text = "DBM Maschinenbau\r\nDBW Werkstoffwissenschaft\r\nDBV Verfahrens- und Naturstofftechni" +
    "k";
            
            // 
            // StuPlOrgInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(715, 363);
            this.Controls.Add(this.DB);
            this.Controls.Add(this.load);
            this.Controls.Add(this.from);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Pfad);
            this.Controls.Add(this.button1);
            this.Name = "StuPlOrgInterface";
            this.Text = "Stundenplan Organiserer v0.1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Pfad;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label from;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.TextBox DB;
    }
}

