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
            this.OverrideBox = new System.Windows.Forms.CheckBox();
            this.save = new System.Windows.Forms.Button();
            this.ziel = new System.Windows.Forms.TextBox();
            this.labelorigin = new System.Windows.Forms.Label();
            this.labeldestination = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxAllPdf = new System.Windows.Forms.CheckBox();
            this.snippet = new System.Windows.Forms.Button();
            this.opendesti = new System.Windows.Forms.Button();
            this.rename = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Position)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(644, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Zerteilen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Pfad
            // 
            this.Pfad.Location = new System.Drawing.Point(12, 29);
            this.Pfad.Name = "Pfad";
            this.Pfad.ReadOnly = true;
            this.Pfad.Size = new System.Drawing.Size(626, 20);
            this.Pfad.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(12, 434);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(710, 23);
            this.progressBar.TabIndex = 2;
            // 
            // from
            // 
            this.from.AutoSize = true;
            this.from.Location = new System.Drawing.Point(641, 131);
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
            this.load.Location = new System.Drawing.Point(644, 25);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(75, 27);
            this.load.TabIndex = 5;
            this.load.Text = "Laden";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.Load_Click);
            // 
            // DB
            // 
            this.DB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DB.Location = new System.Drawing.Point(12, 101);
            this.DB.Multiline = true;
            this.DB.Name = "DB";
            this.DB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
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
            this.Position.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Position.Location = new System.Drawing.Point(573, 384);
            this.Position.Margin = new System.Windows.Forms.Padding(2);
            this.Position.Minimum = 1;
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(150, 45);
            this.Position.TabIndex = 7;
            this.Position.Value = 1;
            this.Position.Scroll += new System.EventHandler(this.Position_Scroll);
            // 
            // trackbarlabel
            // 
            this.trackbarlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackbarlabel.AutoSize = true;
            this.trackbarlabel.Location = new System.Drawing.Point(424, 384);
            this.trackbarlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.trackbarlabel.Name = "trackbarlabel";
            this.trackbarlabel.Size = new System.Drawing.Size(145, 13);
            this.trackbarlabel.TabIndex = 8;
            this.trackbarlabel.Text = "Ungefähre Position der Tags:";
            // 
            // positionlabel
            // 
            this.positionlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.positionlabel.AutoSize = true;
            this.positionlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.positionlabel.Location = new System.Drawing.Point(532, 399);
            this.positionlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.positionlabel.Name = "positionlabel";
            this.positionlabel.Size = new System.Drawing.Size(37, 17);
            this.positionlabel.TabIndex = 9;
            this.positionlabel.Text = "0-15";
            this.positionlabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // percentlabel
            // 
            this.percentlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.percentlabel.AutoSize = true;
            this.percentlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.percentlabel.Location = new System.Drawing.Point(508, 399);
            this.percentlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.percentlabel.Name = "percentlabel";
            this.percentlabel.Size = new System.Drawing.Size(20, 17);
            this.percentlabel.TabIndex = 10;
            this.percentlabel.Text = "%";
            // 
            // OverrideBox
            // 
            this.OverrideBox.AutoSize = true;
            this.OverrideBox.Location = new System.Drawing.Point(336, 122);
            this.OverrideBox.Margin = new System.Windows.Forms.Padding(2);
            this.OverrideBox.Name = "OverrideBox";
            this.OverrideBox.Size = new System.Drawing.Size(72, 17);
            this.OverrideBox.TabIndex = 11;
            this.OverrideBox.Text = "Override?";
            this.OverrideBox.UseVisualStyleBackColor = true;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(644, 64);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 27);
            this.save.TabIndex = 13;
            this.save.Text = "Festlegen";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // ziel
            // 
            this.ziel.Location = new System.Drawing.Point(12, 68);
            this.ziel.Name = "ziel";
            this.ziel.ReadOnly = true;
            this.ziel.Size = new System.Drawing.Size(626, 20);
            this.ziel.TabIndex = 12;
            // 
            // labelorigin
            // 
            this.labelorigin.AutoSize = true;
            this.labelorigin.Location = new System.Drawing.Point(13, 13);
            this.labelorigin.Name = "labelorigin";
            this.labelorigin.Size = new System.Drawing.Size(45, 13);
            this.labelorigin.TabIndex = 14;
            this.labelorigin.Text = "Original:";
            // 
            // labeldestination
            // 
            this.labeldestination.AutoSize = true;
            this.labeldestination.Location = new System.Drawing.Point(13, 52);
            this.labeldestination.Name = "labeldestination";
            this.labeldestination.Size = new System.Drawing.Size(285, 13);
            this.labeldestination.TabIndex = 15;
            this.labeldestination.Text = "Speicherort (entspricht standardmäßig dem Ausgangspfad):";
            // 
            // checkBoxAllPdf
            // 
            this.checkBoxAllPdf.AutoSize = true;
            this.checkBoxAllPdf.Location = new System.Drawing.Point(336, 101);
            this.checkBoxAllPdf.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxAllPdf.Name = "checkBoxAllPdf";
            this.checkBoxAllPdf.Size = new System.Drawing.Size(138, 17);
            this.checkBoxAllPdf.TabIndex = 16;
            this.checkBoxAllPdf.Text = "Für alle Pdfs im Ordner?";
            this.checkBoxAllPdf.UseVisualStyleBackColor = true;
            // 
            // snippet
            // 
            this.snippet.Location = new System.Drawing.Point(337, 384);
            this.snippet.Name = "snippet";
            this.snippet.Size = new System.Drawing.Size(75, 23);
            this.snippet.TabIndex = 17;
            this.snippet.Text = "Test";
            this.snippet.UseVisualStyleBackColor = true;
            this.snippet.Click += new System.EventHandler(this.snippet_Click);
            // 
            // opendesti
            // 
            this.opendesti.Location = new System.Drawing.Point(337, 144);
            this.opendesti.Name = "opendesti";
            this.opendesti.Size = new System.Drawing.Size(137, 23);
            this.opendesti.TabIndex = 18;
            this.opendesti.Text = "Ausgabeordner";
            this.opendesti.UseVisualStyleBackColor = true;
            this.opendesti.Click += new System.EventHandler(this.opendesti_Click);
            // 
            // rename
            // 
            this.rename.Location = new System.Drawing.Point(338, 174);
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(136, 23);
            this.rename.TabIndex = 19;
            this.rename.Text = "Umbenennen";
            this.rename.UseVisualStyleBackColor = true;
            this.rename.Click += new System.EventHandler(this.rename_Click);
            // 
            // StuPlOrgInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(734, 469);
            this.Controls.Add(this.rename);
            this.Controls.Add(this.opendesti);
            this.Controls.Add(this.snippet);
            this.Controls.Add(this.checkBoxAllPdf);
            this.Controls.Add(this.labeldestination);
            this.Controls.Add(this.labelorigin);
            this.Controls.Add(this.save);
            this.Controls.Add(this.ziel);
            this.Controls.Add(this.OverrideBox);
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
            this.Text = "Stundenplan Organiserer v1.0";
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
        private System.Windows.Forms.CheckBox OverrideBox;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.TextBox ziel;
        private System.Windows.Forms.Label labelorigin;
        private System.Windows.Forms.Label labeldestination;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkBoxAllPdf;
        private System.Windows.Forms.Button snippet;
        private System.Windows.Forms.Button opendesti;
        private System.Windows.Forms.Button rename;
    }
}

