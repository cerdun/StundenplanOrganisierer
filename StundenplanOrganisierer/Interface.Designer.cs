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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StuPlOrgInterface));
            this.button1 = new System.Windows.Forms.Button();
            this.Pfad = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.from = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.load = new System.Windows.Forms.Button();
            this.DB = new System.Windows.Forms.TextBox();
            this.DBToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Position = new System.Windows.Forms.TrackBar();
            this.trackbarlabel = new System.Windows.Forms.Label();
            this.positionlabel = new System.Windows.Forms.Label();
            this.percentlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Position)).BeginInit();
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
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Pfad
            // 
            this.Pfad.Location = new System.Drawing.Point(12, 9);
            this.Pfad.Name = "Pfad";
            this.Pfad.ReadOnly = true;
            this.Pfad.Size = new System.Drawing.Size(626, 20);
            this.Pfad.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 365);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(710, 23);
            this.progressBar.TabIndex = 2;
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
            this.load.Click += new System.EventHandler(this.Load_Click);
            // 
            // DB
            // 
            this.DB.Location = new System.Drawing.Point(12, 32);
            this.DB.Multiline = true;
            this.DB.Name = "DB";
            this.DB.Size = new System.Drawing.Size(319, 327);
            this.DB.TabIndex = 6;
            this.DB.Text = resources.GetString("DB.Text");
            // 
            // DBToolTip
            // 
            this.DBToolTip.AutoPopDelay = 50000;
            this.DBToolTip.InitialDelay = 500;
            this.DBToolTip.ReshowDelay = 100;
            this.DBToolTip.ShowAlways = true;
            this.DBToolTip.ToolTipTitle = "Hier könnte ihre Werbung stehen";
            // 
            // Position
            // 
            this.Position.Location = new System.Drawing.Point(569, 314);
            this.Position.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Position.Minimum = 1;
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(150, 45);
            this.Position.TabIndex = 7;
            this.Position.Value = 1;
            this.Position.Scroll += new System.EventHandler(this.Position_Scroll);
            // 
            // trackbarlabel
            // 
            this.trackbarlabel.AutoSize = true;
            this.trackbarlabel.Location = new System.Drawing.Point(422, 297);
            this.trackbarlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.trackbarlabel.Name = "trackbarlabel";
            this.trackbarlabel.Size = new System.Drawing.Size(296, 13);
            this.trackbarlabel.TabIndex = 8;
            this.trackbarlabel.Text = "Hier kann die ungefähre Position der Tags festgelegt werden:";
            // 
            // positionlabel
            // 
            this.positionlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.positionlabel.AutoSize = true;
            this.positionlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.positionlabel.Location = new System.Drawing.Point(523, 314);
            this.positionlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.positionlabel.Name = "positionlabel";
            this.positionlabel.Size = new System.Drawing.Size(37, 17);
            this.positionlabel.TabIndex = 9;
            this.positionlabel.Text = "0-15";
            this.positionlabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // percentlabel
            // 
            this.percentlabel.AutoSize = true;
            this.percentlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.percentlabel.Location = new System.Drawing.Point(500, 314);
            this.percentlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.percentlabel.Name = "percentlabel";
            this.percentlabel.Size = new System.Drawing.Size(20, 17);
            this.percentlabel.TabIndex = 10;
            this.percentlabel.Text = "%";
            // 
            // StuPlOrgInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(734, 398);
            this.Controls.Add(this.percentlabel);
            this.Controls.Add(this.positionlabel);
            this.Controls.Add(this.trackbarlabel);
            this.Controls.Add(this.Position);
            this.Controls.Add(this.DB);
            this.Controls.Add(this.load);
            this.Controls.Add(this.from);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Pfad);
            this.Controls.Add(this.button1);
            this.Name = "StuPlOrgInterface";
            this.Text = "Stundenplan Organiserer v0.2";
            ((System.ComponentModel.ISupportInitialize)(this.Position)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Pfad;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label from;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.TextBox DB;
        private System.Windows.Forms.ToolTip DBToolTip;
        private System.Windows.Forms.TrackBar Position;
        private System.Windows.Forms.Label trackbarlabel;
        private System.Windows.Forms.Label positionlabel;
        private System.Windows.Forms.Label percentlabel;
    }
}

