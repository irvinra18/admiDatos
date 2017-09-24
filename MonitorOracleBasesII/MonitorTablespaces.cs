using Monitor.BL;
using Monitor.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Monitor
{
    public partial class MonitorTablespaces : Form
    {
        public MonitorTablespaces(MonitorBL monitorBL)
        {
            InitializeComponent();
            this.monitorBL = monitorBL;
            this.procesoBL = monitorBL;
            //this.anterr = anterr;
            // this.timer1.Start();
            buffer = new ContenedorPuntos();
            ds = monitorBL.ConsultaTableSpace();
            timer1.Interval = 1000;
            timer1.Start();
            alerta = "";// "Memoria restante es inferior al 10% en algun intervalo del buffer. Revise ventana log para detalles.";
            alertaOn = false;

        }

        public MonitorTablespaces(MonitorBL monitorBL, Form anterr)
        {
            InitializeComponent();
            this.monitorBL = monitorBL;
            this.anterr = anterr;
            this.timer1.Start();
            ds = monitorBL.ConsultaTableSpace();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int actual = Int32.Parse(label4.Text);
            if (procesoBL.ConsultaProceso() > -1)
            {
                int siguiente = actual + (int)procesoBL.ConsultaProceso();
                //si se pudo realizar la consulta
                //  label4.Text = siguiente.ToString();
                label4.Text = procesoBL.getSgaSpace().ToString();
            }
            else
            {
                label4.Text = procesoBL.ConsultaProceso().ToString();

            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text = count.ToString();






            int buffiest;
        
            buffiest = (int)procesoBL.getSgaSpace();

            if (buffiest > 90)
            {
                //label3.Text = alerta;
                // MessageBox.Show("Alerta, el porcentaje en memoria es superior al 90%", "My application", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                alertaOn = true;
            }

            if (alertaOn == true)
            {
                if ((count % 2) != 0)
                {
                    alerta = "Memoria restante es inferior al 10% en algun intervalo del buffer. Revise ventana log para detalles.";
                }
                else
                {
                    alerta = "";
                }
            }
            label3.Text = alerta;
            buffer.Add(buffiest);
            //actualizar todo el conjunto de datos
            label8.Text = procesoBL.getFreeSpaceSgaSharedPool().ToString();
            label10.Text = procesoBL.getFreeSpace().ToString();
            label4.Text = procesoBL.getSgaSpace().ToString();
            this.ActualizaBuffer();
            count++;

        }


        //creandoTimer

        private void ActualizaBuffer()
        {
            chart2.Series[0].Points.Clear();
            buffer.PlusPlus();
            for (int i = 0; i < buffer.getNivel(); i++)
            {
                chart2.Series[0].Points.AddXY(buffer.getPuntos()[i].getX(), buffer.getPuntos()[i].getY());
            }

        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        //private void form1_Load(object sender, EventArgs e)
        //{

        //    chart1.ChartAreas[0].AxisX.Minimum = 0;
        //    chart1.ChartAreas[0].AxisX.Maximum = 60;
        //    chart1.ChartAreas[0].AxisX.Interval = 10;

        //    chart1.ChartAreas[0].AxisY.Minimum = 0;
        //    chart1.ChartAreas[0].AxisY.Maximum = 100;

        //    //Agregar los valores
        //    /*chart1.Series[0].Points.AddXY(0, 0);
        //    chart1.Series[0].Points.AddXY(5, 2);
        //    chart1.Series[0].Points.AddXY(7, 5);
        //    chart1.Series[0].Points.AddXY(9, 2);
        //    chart1.Series[0].Points.AddXY(12, 1);
        //    chart1.Series[0].Points.AddXY(14, 9);
        //    chart1.Series[0].Points.AddXY(30, 50);
        //    chart1.Series[0].Points.AddXY(45, 70);
        //    chart1.Series[0].Points.AddXY(55, 20);
        //    chart1.Series[0].Points.AddXY(60, 90);*/


        //}

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            alertaOn = false;
            label3.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



        private void MonitorTablespaces_Load(object sender, EventArgs e)
        {
            //chart2
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 60;
            chart2.ChartAreas[0].AxisX.Interval = 10;

            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Maximum = 100;
            //fin chart2

            chart1.ChartAreas[0].AxisX.Maximum = 10;
            chart1.ChartAreas[0].AxisX.Minimum = 0;

            DataSet inicial = new DataSet();
            bool banderaIni = false;
            inicial = monitorBL.consultaInicial();
            if (inicial.Tables[0].Rows.Count == 0)
            {
                banderaIni = true;
            }
                foreach (DataTable thisTable in ds.Tables)
            {

                foreach (DataRow row in thisTable.Rows)
                {



                    string temp = row[0].ToString();
                    string rest = row[1].ToString();
                    string rest1 = row[2].ToString();
                    double restI = Double.Parse(rest) - Double.Parse(rest1);
                    double restII = (Double.Parse(rest)*0.8)-restI;
                

                    DataSet final_get_rows = monitorBL.final_get_rows(temp);
                    DataSet final_size = monitorBL.DatalengthTb(temp);


                    if (banderaIni)
                    {
                        //banderaIni = false;

                        foreach (DataRow finalRows in final_get_rows.Tables[0].Rows)
                        {
                            if (final_get_rows.Tables[0].Rows.Count > 0)
                            {

                                monitorBL.insertarRegistroSQLite(temp, finalRows["TABLE_NAME"].ToString(), finalRows["NUM_ROWS"].ToString(), "0", "0", "0");

                            }

                        }
                        foreach (DataRow finalSize in final_size.Tables[0].Rows)
                        {
                            if (final_size.Tables[0].Rows.Count > 0)
                            {

                                monitorBL.updateRegistroSQLite(temp, finalSize["TABLE_NAME"].ToString(), finalSize["SUM(DATA_LENGTH)"].ToString());

                            }

                        }
                    }
                    else
                    {
                      //  hacer update para tomar la tasa de transferencia
                        //DataSet dst = monitorBL.consultaInicial();

                        //foreach (DataRow registroMon in inicial.Tables[0].Rows)
                        //{
                        //    if (temp == registroMon["TABLESPACE"].ToString())
                        //    {
                        //        foreach (DataRow finalRows in final_get_rows.Tables[0].Rows)
                        //        {
                        //            if (registroMon["TABLA"].ToString() == finalRows["TABLE_NAME"].ToString())
                        //            {
                        //                int diferencia = Int32.Parse(finalRows["NUM_ROWS"].ToString()) - Int32.Parse(registroMon["REGISTRO"].ToString());
                        //                monitorBL.updateRegistroSQLite(temp, finalRows["TABLE_NAME"].ToString(), finalRows["NUM_ROWS"].ToString(), diferencia.ToString(), registroMon["SIZE"].ToString());
                        //            }
                        //        }
                        //    }
                        //    // monitorBL.updateRegistroSQLite(temp, finalSize["TABLE_NAME"].ToString(),)
                        //}


                    }









                    chart1.Series[0].Points.AddXY(temp, restI);
                  

                    if (restI < (Double.Parse(rest) * 0.8))
                    {
                        chart1.Series[1].Points.AddXY(temp, restII);
                        chart1.Series[2].Points.AddXY(temp, Double.Parse(rest1) - restII);
                        //HACER ACÁ LOS TABLESPACES QUE ESTÁN ARRIBA DEL HWM
                      //  chart1.Series[0].Points[].Color = Color.Red;

                    }
                    else {
                        chart1.Series[2].Points.AddXY(temp, Double.Parse(rest1));
                    }
                    

              
                }

            }

            // DataSet dss = monitorBL.DatalengthTb("SYSTEM");

              dataGridView1.DataSource = ds.Tables[0];
            //int rowI = 0;
            //foreach (DataTable thisTable in ds.Tables)
            //{

            //    foreach (DataRow row in thisTable.Rows)
            //    {
                    


            //        //dataGridView1["Tablespace"].Value = col[""].ToString();
            //        this.dataGridView1.Rows[rowI].Cells["Tablespace"].Value = row[0].ToString();
            //        this.dataGridView1.Rows[rowI].Cells["MaxSize"].Value = row[1].ToString();
            //        this.dataGridView1.Rows[rowI].Cells["FreeSpace"].Value = row[2].ToString();
            //        this.dataGridView1.Rows[rowI].Cells["FreeSpace2"].Value = row[3].ToString();
            //        this.dataGridView1.Rows[rowI].Cells["SpaceUsed"].Value = row[4].ToString();
            //        this.dataGridView1.Rows[rowI].Cells["HWM"].Value = "0";
            //        rowI += 1;

            //    }
            //}

            //  MessageBox.Show(monitorBL.DatalengthTb("SYSTEM").ToString(), "My Application",
            //  MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);


        }


        private string calculoDiasRest(string tb, string freeSpace)
        {

            int total = monitorBL.sumaTotal(tb);
            string fecha = monitorBL.getFecha(tb);


           DateTime today= DateTime.Now;
            DateTime dt= DateTime.Now; 
            // string dateTime = "01/08/2008 14:50:50.42";
            try
            {
                 dt = Convert.ToDateTime(fecha);
            }
            catch (Exception)
            {

            }


            // Difference in days, hours, and minutes.
            TimeSpan ts = today - dt;

            // Difference in days.
            int differenceInDays = ts.Days;
            int res = 1;
            long freeSpaceBytes =(long)(float.Parse(freeSpace)* 1048576);

            if (differenceInDays != 0)
            {
                res = total / differenceInDays;
                if (res != 0)
                {
                    return ((long)(freeSpaceBytes / res)).ToString();

                }
                else { return "00"; }

            }
            else {
                return "Sin registros anteriores.";
            }


            

        }


        //private
        MonitorBL monitorBL;
        //formulario anterior
        Form anterr;
        private void MonitorTablespaces_FormClosed(object sender, FormClosedEventArgs e)
        {
            monitorBL.updateRegistroSQLiteFecha();

          //  anterr.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             int index = dataGridView1.CurrentRow.Index;  
            MessageBox.Show("To full tablespace: "+calculoDiasRest( ds.Tables[0].Rows[index][0].ToString(), ds.Tables[0].Rows[index][2].ToString())+"\n To full HWM: "+10 , "Days remaining",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

        }

        //atributos
        private DataSet ds;

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            DataPoint curPoint = null;
            ChartArea ca = chart1.ChartAreas[0];
            Axis ax = ca.AxisX;
            Axis ay = ca.AxisY;
            HitTestResult hit = chart1.HitTest(e.X, e.Y);
            if (hit.PointIndex >= 0) curPoint = hit.Series.Points[hit.PointIndex];
            if (curPoint != null) {
                Series s = hit.Series;
                double dx = ax.PixelPositionToValue(e.X);
                double dy = ax.PixelPositionToValue(e.Y);
                MessageBox.Show(dx+" "+dy, "CACA",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }
    }
}
