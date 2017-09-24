namespace Monitor
{
    partial class TableSpace
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
            this.dgvTableSpaces = new System.Windows.Forms.DataGridView();
            this.botonCargar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableSpaces)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTableSpaces
            // 
            this.dgvTableSpaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTableSpaces.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTableSpaces.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTableSpaces.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTableSpaces.Location = new System.Drawing.Point(25, 27);
            this.dgvTableSpaces.Name = "dgvTableSpaces";
            this.dgvTableSpaces.ReadOnly = true;
            this.dgvTableSpaces.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvTableSpaces.Size = new System.Drawing.Size(802, 315);
            this.dgvTableSpaces.TabIndex = 0;
            // 
            // botonCargar
            // 
            this.botonCargar.Location = new System.Drawing.Point(730, 348);
            this.botonCargar.Name = "botonCargar";
            this.botonCargar.Size = new System.Drawing.Size(97, 36);
            this.botonCargar.TabIndex = 1;
            this.botonCargar.Text = "Cargar";
            this.botonCargar.UseVisualStyleBackColor = true;
            this.botonCargar.Click += new System.EventHandler(this.botonCargar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 348);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 35);
            this.button1.TabIndex = 2;
            this.button1.Text = "Monitor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TableSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 396);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.botonCargar);
            this.Controls.Add(this.dgvTableSpaces);
            this.Name = "TableSpace";
            this.Text = "TableSpace";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableSpaces)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTableSpaces;
        private System.Windows.Forms.Button botonCargar;
        private System.Windows.Forms.Button button1;
    }
}