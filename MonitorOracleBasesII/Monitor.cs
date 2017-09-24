using Monitor.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Monitor.Clases;

namespace Monitor
{
    public partial class Monitor : Form
    {

        public Monitor(MonitorBL monitorBL)
        {
            InitializeComponent();
            buffer = new ContenedorPuntos();
            this.procesoBL = monitorBL;
            timer1.Interval = 1000;
            timer1.Start();
            alerta = "";// "Memoria restante es inferior al 10% en algun intervalo del buffer. Revise ventana log para detalles.";
            alertaOn = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int actual = Int32.Parse(label4.Text);
            if (procesoBL.ConsultaProceso() > -1)
            {
                int siguiente=actual +(int) procesoBL.ConsultaProceso();
                //si se pudo realizar la consulta
                //  label4.Text = siguiente.ToString();
                label4.Text = procesoBL.getSgaSpace().ToString();
            }
            else {
                label4.Text = procesoBL.ConsultaProceso().ToString();

            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
       
           label1.Text = count.ToString();



          
          

            int buffiest;
            //if (count == 5)
            //{
            //    // MessageBox.Show("Alerta, el porcentaje en memoria es superior al 90%", "My application", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            //    buffiest = 10;
            //}
            //else
            //{
            //    if (count == 11)
            //    {
            //        buffiest = 92;
            //    }
            //    else
            //    {
            //        buffiest = (int)procesoBL.getSgaSpace();
            //    }
            //}
            buffiest = (int)procesoBL.getSgaSpace();

            if (buffiest > 90)
            {
                //label3.Text = alerta;
                // MessageBox.Show("Alerta, el porcentaje en memoria es superior al 90%", "My application", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                alertaOn = true;
            }
          
            if (alertaOn == true) {
                if ((count % 2) != 0)
                {
                    alerta = "Memoria restante es inferior al 10% en algun intervalo del buffer. Revise ventana log para detalles.";
                }
                else {
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
        
        private void ActualizaBuffer() {
            chart1.Series[0].Points.Clear();
            buffer.PlusPlus();
            for (int i = 0; i<buffer.getNivel(); i++) {
                chart1.Series[0].Points.AddXY(buffer.getPuntos()[i].getX(), buffer.getPuntos()[i].getY());
            }

        }
            

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void form1_Load(object sender, EventArgs e)
        {
            
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 60;
            chart1.ChartAreas[0].AxisX.Interval = 10;

            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 100;

            //Agregar los valores
            /*chart1.Series[0].Points.AddXY(0, 0);
            chart1.Series[0].Points.AddXY(5, 2);
            chart1.Series[0].Points.AddXY(7, 5);
            chart1.Series[0].Points.AddXY(9, 2);
            chart1.Series[0].Points.AddXY(12, 1);
            chart1.Series[0].Points.AddXY(14, 9);
            chart1.Series[0].Points.AddXY(30, 50);
            chart1.Series[0].Points.AddXY(45, 70);
            chart1.Series[0].Points.AddXY(55, 20);
            chart1.Series[0].Points.AddXY(60, 90);*/


        }

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



        //metodos timer
    }
}
