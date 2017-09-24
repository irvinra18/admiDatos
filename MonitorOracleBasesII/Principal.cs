using Monitor.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            monitorBL = new MonitorBL();
        }

        private void verTableSpacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new TableSpace(monitorBL).Show();
            new MonitorTablespaces(monitorBL, this).Show();
        }

        private void memoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            new Monitor(monitorBL).Show();
            
        }

        ~Principal() {
            monitorBL.cerrarBase();
        }

        //atributos
        MonitorBL monitorBL;
    }
}
