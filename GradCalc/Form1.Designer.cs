namespace GradCalc
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.msHauptfenster = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernUnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.druckenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fächerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvAnsicht = new System.Windows.Forms.DataGridView();
            this.dgvColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColumnSurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColumnAbschluss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsDGV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.DelRow = new System.Windows.Forms.ToolStripMenuItem();
            this.neuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msHauptfenster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnsicht)).BeginInit();
            this.cmsDGV.SuspendLayout();
            this.SuspendLayout();
            // 
            // msHauptfenster
            // 
            this.msHauptfenster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.optionenToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.msHauptfenster.Location = new System.Drawing.Point(0, 0);
            this.msHauptfenster.Name = "msHauptfenster";
            this.msHauptfenster.Size = new System.Drawing.Size(1008, 24);
            this.msHauptfenster.TabIndex = 0;
            this.msHauptfenster.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuToolStripMenuItem,
            this.öffnenToolStripMenuItem,
            this.speichernToolStripMenuItem,
            this.speichernUnterToolStripMenuItem,
            this.druckenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.öffnenToolStripMenuItem.Text = "Öffnen...";
            this.öffnenToolStripMenuItem.Click += new System.EventHandler(this.öffnenToolStripMenuItem_Click);
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.speichernToolStripMenuItem.Text = "Speichern...";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.speichernToolStripMenuItem_Click);
            // 
            // speichernUnterToolStripMenuItem
            // 
            this.speichernUnterToolStripMenuItem.Name = "speichernUnterToolStripMenuItem";
            this.speichernUnterToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.speichernUnterToolStripMenuItem.Text = "Speichern Unter...";
            this.speichernUnterToolStripMenuItem.Click += new System.EventHandler(this.speichernUnterToolStripMenuItem_Click);
            // 
            // druckenToolStripMenuItem
            // 
            this.druckenToolStripMenuItem.Name = "druckenToolStripMenuItem";
            this.druckenToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.druckenToolStripMenuItem.Text = "Drucken";
            this.druckenToolStripMenuItem.Click += new System.EventHandler(this.druckenToolStripMenuItem_Click);
            // 
            // optionenToolStripMenuItem
            // 
            this.optionenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fächerToolStripMenuItem});
            this.optionenToolStripMenuItem.Name = "optionenToolStripMenuItem";
            this.optionenToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.optionenToolStripMenuItem.Text = "Optionen";
            // 
            // fächerToolStripMenuItem
            // 
            this.fächerToolStripMenuItem.Name = "fächerToolStripMenuItem";
            this.fächerToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.fächerToolStripMenuItem.Text = "Fächer";
            this.fächerToolStripMenuItem.Click += new System.EventHandler(this.fächerToolStripMenuItem_Click);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // dgvAnsicht
            // 
            this.dgvAnsicht.AllowDrop = true;
            this.dgvAnsicht.AllowUserToResizeColumns = false;
            this.dgvAnsicht.AllowUserToResizeRows = false;
            this.dgvAnsicht.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAnsicht.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnsicht.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColumnName,
            this.dgvColumnSurname,
            this.dgvColumnAbschluss});
            this.dgvAnsicht.ContextMenuStrip = this.cmsDGV;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAnsicht.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAnsicht.Location = new System.Drawing.Point(12, 27);
            this.dgvAnsicht.MultiSelect = false;
            this.dgvAnsicht.Name = "dgvAnsicht";
            this.dgvAnsicht.Size = new System.Drawing.Size(984, 523);
            this.dgvAnsicht.TabIndex = 1;
            this.dgvAnsicht.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellChanged);
            // 
            // dgvColumnName
            // 
            this.dgvColumnName.Frozen = true;
            this.dgvColumnName.HeaderText = "Name";
            this.dgvColumnName.Name = "dgvColumnName";
            this.dgvColumnName.Width = 120;
            // 
            // dgvColumnSurname
            // 
            this.dgvColumnSurname.Frozen = true;
            this.dgvColumnSurname.HeaderText = "Vorname";
            this.dgvColumnSurname.Name = "dgvColumnSurname";
            this.dgvColumnSurname.Width = 120;
            // 
            // dgvColumnAbschluss
            // 
            this.dgvColumnAbschluss.HeaderText = "Abschluss";
            this.dgvColumnAbschluss.Name = "dgvColumnAbschluss";
            this.dgvColumnAbschluss.ReadOnly = true;
            // 
            // cmsDGV
            // 
            this.cmsDGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddRow,
            this.DelRow});
            this.cmsDGV.Name = "cmsDGV";
            this.cmsDGV.Size = new System.Drawing.Size(150, 48);
            // 
            // AddRow
            // 
            this.AddRow.Name = "AddRow";
            this.AddRow.Size = new System.Drawing.Size(149, 22);
            this.AddRow.Text = "Zeile Einfügen";
            // 
            // DelRow
            // 
            this.DelRow.Name = "DelRow";
            this.DelRow.Size = new System.Drawing.Size(149, 22);
            this.DelRow.Text = "Zeile Löschen";
            // 
            // neuToolStripMenuItem
            // 
            this.neuToolStripMenuItem.Name = "neuToolStripMenuItem";
            this.neuToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.neuToolStripMenuItem.Text = "Neue Datei...";
            this.neuToolStripMenuItem.Click += new System.EventHandler(this.neuToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.dgvAnsicht);
            this.Controls.Add(this.msHauptfenster);
            this.MainMenuStrip = this.msHauptfenster;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Abschlussrechner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.msHauptfenster.ResumeLayout(false);
            this.msHauptfenster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnsicht)).EndInit();
            this.cmsDGV.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msHauptfenster;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernUnterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fächerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvAnsicht;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColumnSurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColumnAbschluss;
        private System.Windows.Forms.ContextMenuStrip cmsDGV;
        private System.Windows.Forms.ToolStripMenuItem AddRow;
        private System.Windows.Forms.ToolStripMenuItem DelRow;
        private System.Windows.Forms.ToolStripMenuItem druckenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuToolStripMenuItem;
    }
}

