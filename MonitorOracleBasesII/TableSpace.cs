using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Monitor.BL;

namespace Monitor
{
    public partial class TableSpace : Form
    {
        MonitorBL monitorBL;

        public TableSpace(MonitorBL monitorBL)
        {
            InitializeComponent();
            this.monitorBL = monitorBL;
        }

        private void botonCargar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds=monitorBL.ConsultaTableSpace();
            
            this.CargarTabla(ds);
        }

        private void CargarTabla(DataSet ds) 
        {
            dgvTableSpaces.DataSource = ds.Tables[0];
//            MessageBox.Show("The calculations \"are\" complete", "My Application",
//MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MonitorTablespaces(monitorBL,this).Show();
            
            this.Hide();
        }

    }
}
