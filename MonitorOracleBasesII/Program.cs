﻿using Monitor.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Monitor
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MonitorTablespaces(new MonitorBL()));//new Principal());
        }
    }
}
