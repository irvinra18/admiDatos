﻿namespace Monitor
{
    partial class Principal
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
            this.verTableSpacesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verTableSpacesToolStripMenuItem,
            this.memoriaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(772, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // verTableSpacesToolStripMenuItem
            // 
            this.verTableSpacesToolStripMenuItem.Name = "verTableSpacesToolStripMenuItem";
            this.verTableSpacesToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.verTableSpacesToolStripMenuItem.Text = "Ver TableSpaces";
            this.verTableSpacesToolStripMenuItem.Click += new System.EventHandler(this.verTableSpacesToolStripMenuItem_Click);
            // 
            // memoriaToolStripMenuItem
            // 
            this.memoriaToolStripMenuItem.Name = "memoriaToolStripMenuItem";
            this.memoriaToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.memoriaToolStripMenuItem.Text = "Ver Memoria";
            this.memoriaToolStripMenuItem.Click += new System.EventHandler(this.memoriaToolStripMenuItem_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 323);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Principal";
            this.Text = "Principal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem verTableSpacesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoriaToolStripMenuItem;
    }
}