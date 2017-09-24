using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monitor.DB;
using System.Data;

namespace Monitor.BL
{
    public class MonitorBL
    {
        MonitorDB procesoDB = new MonitorDB();

        #region TableSpaces
        public DataSet ConsultaTableSpace()
        {
            return procesoDB.ConsultaTableSpace();
        }
        public DataSet DatalengthTb(string tb_name) {
            return procesoDB.DataLengthTablespace(tb_name);
        }
        public long NumRowTb(string tb_name) {
            return procesoDB.NumRowsByTablespace(tb_name);
        }

        public DataSet final_get_rows(string tb_name) { 

           return  procesoDB.final_get_rows(tb_name);
        }


    #endregion

    #region Memoria
    public long ConsultaProceso()
        {
            return procesoDB.ConsultaProceso();
        }

        public double getSgaSpace()
        {
            return procesoDB.getSgaSpace();
        }

        public long getFreeSpaceSgaSharedPool()
        {
            return procesoDB.getFreeSpaceSgaSharedPool();
        }

        public long getFreeSpace()
        {
            return procesoDB.getFreeSpaceSga();

        }
        #endregion

        public void cerrarBase() {
            procesoDB.CloseConection();
        }

        public void createDBSQLlite() {
            procesoDB.createDBSQLite();
        }

        public void insertarRegistroSQLite(string tablespace, string tabla, string registro, string size, string diferencia, string total) {

            procesoDB.InsertarRegistroSQLite(tablespace, tabla, registro, size, diferencia, total);
        }

        public void updateRegistroSQLite(string tablespace, string tabla, string size) { 

            procesoDB.updateRegistroSQLite(tablespace, tabla, size);
        }
        public void updateRegistroSQLite(string tablespace, string tabla, string registros, string diferencia, string size)
        {

            procesoDB.updateRegistroSQLite(tablespace, tabla, registros,diferencia,size);
        }
        public DataSet consultaInicial() {
            return procesoDB.ConsultaInicial();
        }

        public int sumaTotal(string tableSpace)
        {
            return procesoDB.sumaTotal(tableSpace);
        }
        public void updateRegistroSQLiteFecha() {
            procesoDB.updateRegistroSQLiteFecha();
        }
        public string getFecha(string tableSpace)
        {
            return procesoDB.getFecha(tableSpace);
        }


    }
}
