using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Data.SQLite;
using System.IO;

namespace Monitor.DB
{
    public class MonitorDB
    {
        //atributos
        private OracleConnection conn;
        private string oradb;
        private OracleCommand cmd;
        //DataSet ds;
        string sqlQuery = string.Empty;
        string ConnectionString = "Data Source=(DESCRIPTION="
      + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521)))"
      + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));"
      + "User Id=system;Password=root;";

        #region Constructor

        public MonitorDB()
        {
            oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521)))"
             + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));"
             + "User Id=sys;Password=root;DBA Privilege=SYSDBA";
            conn = new OracleConnection();
            

            conn.ConnectionString = oradb;
            conn.Open();
            
            conn.ClientId = "sys as sysdba";
            this.createDBSQLite();


        }

        #endregion

        #region TableSapaces

        public DataSet ConsultaTableSpace()
        {
            //este no está en funcionamiento
            sqlQuery = "SELECT TABLESPACE_NAME,BLOCK_SIZE,INITIAL_EXTENT,NEXT_EXTENT,MAX_EXTENTS,MAX_SIZE,PCT_INCREASE,MIN_EXTLEN FROM DBA_TABLESPACES";
   
            //         sqlQuery = "select (select decode(extent_management,'LOCAL','*',' ') ||"+
   //               "decode(segment_space_management,'AUTO','a ','m ')"+
   //               "from dba_tablespaces where tablespace_name = b.tablespace_name) || nvl(b.tablespace_name,"+
   //                          "nvl(a.tablespace_name,'UNKOWN')) name,"+
   //            "kbytes_alloc kbytes,"+
   //            "kbytes_alloc-nvl(kbytes_free,0) used,"+

   //            "nvl(kbytes_free,0) free,"+
   //            "((kbytes_alloc-nvl(kbytes_free,0))/"+
   //         " kbytes_alloc)*100 pct_used,"+
   //           "nvl(largest,0) largest,"+
   //           "nvl(kbytes_max,kbytes_alloc) Max_Size,"+
   //           "decode( kbytes_max, 0, 0, (kbytes_alloc/kbytes_max)*100) pct_max_used"+
   //"from ( select sum(bytes)/1024 Kbytes_free,"+
   //                          "max(bytes)/1024 largest,"+
   //                          "tablespace_name"+
   //           "from  sys.dba_free_space"+
   //           "group by tablespace_name ) a,"+
   //     "( select sum(bytes)/1024 Kbytes_alloc,"+
   //                          "sum(maxbytes)/1024 Kbytes_max,"+
   //                          "tablespace_name"+
   //           "from sys.dba_data_files"+
   //          "group by tablespace_name"+
   //           "union all"+
   //      "select sum(bytes)/1024 Kbytes_alloc,"+
   //                          "sum(maxbytes)/1024 Kbytes_max,"+
   //                          "tablespace_name"+
   //           "from sys.dba_temp_files"+
   //           "group by tablespace_name )b"+
   //"where a.tablespace_name (+) = b.tablespace_name ";
            //          string  SsqlQuery = "SELECT /* + RULE */  df.tablespace_name \"Tablespace\"," +
            //       "df.bytes / (1024 * 1024) \"Size (MB)\"," +
            //       "SUM(fs.bytes) / (1024 * 1024)\"Free (MB)\"," +
            //       "Nvl(Round(SUM(fs.bytes) * 100 / df.bytes), 1) \"% Free\"," +
            //       "Round((df.bytes - SUM(fs.bytes)) * 100 / df.bytes) \"% Used\"" +
            //  "FROM dba_free_space fs," +
            //       "(SELECT tablespace_name, SUM(bytes) bytes" +
            //           "FROM dba_data_files" +
            //          "GROUP BY tablespace_name) df" +
            //  "WHERE fs.tablespace_name(+) = df.tablespace_name" +
            // "GROUP BY df.tablespace_name,df.bytes" +
            //"UNION ALL" +
            //"SELECT /* + RULE */ df.tablespace_name tspace," +
            //       "fs.bytes / (1024 * 1024)," +
            //       "SUM(df.bytes_free) / (1024 * 1024)," +
            //       "Nvl(Round((SUM(fs.bytes) - df.bytes_used) * 100 / fs.bytes), 1)," +
            //       "Round((SUM(fs.bytes) - df.bytes_free) * 100 / fs.bytes)" +
            //  "FROM dba_temp_files fs," +
            //       "(SELECT tablespace_name, bytes_free, bytes_used" +
            //          "FROM v$temp_space_header" +
            //         "GROUP BY tablespace_name, bytes_free, bytes_used) df" +
            //   "WHERE fs.tablespace_name(+) = df.tablespace_name" +
            // "GROUP BY df.tablespace_name,fs.bytes,df.bytes_free,df.bytes_used" +
            // "ORDER BY 4 DESC";

            /*string SsqlQuery = "Select t.tablespace_name  \"Tablespace\",  t.status \"Estado\"," +
      "ROUND(MAX(d.bytes) / 1024 / 1024, 2) \"MB Tamaño\"," +
      "ROUND((MAX(d.bytes) / 1024 / 1024) -" +
      "(SUM(decode(f.bytes, NULL, 0, f.bytes)) / 1024 / 1024), 2) \"MB Usados\", " +
      "ROUND(SUM(decode(f.bytes, NULL, 0, f.bytes)) / 1024 / 1024, 2) \"MB Libres\"," +
      "t.pct_increase \"% incremento\" , " +
      "SUBSTR(d.file_name, 1, 80) \"Fichero de datos\" " +
  "FROM DBA_FREE_SPACE f, DBA_DATA_FILES d, DBA_TABLESPACES t" +
  "WHERE t.tablespace_name = d.tablespace_name  AND" +
      "f.tablespace_name(+) = d.tablespace_name" +
      "AND f.file_id(+) = d.file_id GROUP BY t.tablespace_name,   " +
      "d.file_name, t.pct_increase, t.status ORDER BY 1,3 DESC";
            */
            //            MessageBox.Show(SsqlQuery, "My Application",
            //MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);


            return GetDataSet(sqlQuery);
        }


        public DataSet GetDataSet(string SQL)
        {

            /*  // OracleConnection conn = new OracleConnection(ConnectionString);
               OracleDataAdapter da = new OracleDataAdapter();
               OracleCommand cmd = conn.CreateCommand();
               cmd.CommandText = SQL;
               da.SelectCommand = cmd;
               DataSet ds = new DataSet();
               // conn.Open();

               //da.Fill(ds);
               // conn.Close();*/
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "pkg_Asc.multi";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("rf", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            
            cmd.ExecuteNonQuery();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
    

        public DataSet DataLengthTablespace(string tb_name) {

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "pkg_Asc.sizeTB";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("tb", OracleDbType.Varchar2).Value=tb_name;
            cmd.Parameters.Add("rf", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);      

            return ds;


        }
        public DataSet final_get_rows(string tb_name) {

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "pkg_Asc.final_get_rows";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("tb", OracleDbType.Varchar2).Value = tb_name;
            cmd.Parameters.Add("rf", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;


        }
        public long NumRowsByTablespace(string tb_name) {
            string sql = "select NUM_ROWS from ALL_ALL_TABLES where tablespace_name= '" + tb_name + "'";
            OracleCommand cmd = new OracleCommand(sql, conn);
            long tb_length = 0;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr1 = cmd.ExecuteReader();
            dr1.Read();

            //if (dr1.HasRows)
            //  {
            //   while (dr1.Read())
            //   {
            try
            {
                tb_length = dr1.GetInt64(0);
            }
            catch (Exception e) {
                return tb_length;

            }
               // }
           // }
            ////////if (dr1 != null)
            ////////{
            ////////     tb_length = dr1.GetInt64(0);
            ////////}

            return tb_length;
        }


        #endregion

        #region Memoria
      
        public long ConsultaProceso() {
            //long xx=0;
            try
            {
                string resultado = String.Empty;
            
                cmd = new OracleCommand("funcion_jose22", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(":resultado", OracleDbType.Int32);
                cmd.Parameters[":resultado"].Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(":NUM1", OracleDbType.Int32);
                cmd.Parameters[":NUM1"].Value = 22;

                cmd.Parameters.Add(":NUM2", OracleDbType.Int32);
                cmd.Parameters[":NUM2"].Value = 22;

                cmd.ExecuteNonQuery();

                string res = cmd.Parameters[":resultado"].Value.ToString(); //El resultado que venga se pasa a string
                  return Int32.Parse(res);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
                
            }

            //verificar si retorna -1 falso
            return -1;
        }
        public double getSgaSpace() {
            string sql ="select BYTES from v$sgainfo where name = 'Maximum SGA Size'";
            string sql1= "select BYTES from v$sgainfo where name= 'Free SGA Memory Available'";
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            long maximo = dr.GetInt64(0);//.GetString(0).ToString();
                                     //  Console.WriteLine(xx);
                                     // Console.ReadLine();
           OracleCommand cmd1 =new OracleCommand (sql1,conn);
            cmd1.CommandType = CommandType.Text;
            OracleDataReader dr1 = cmd1.ExecuteReader();
            dr1.Read();
            long disponible = dr1.GetInt64(0);
             double xxx = 100 * (((double)maximo - (double)disponible)/(double)maximo);
            return  Math.Round(xxx,0);

        }

        public long getFreeSpaceSga() {
            string sql = "select BYTES from v$sgainfo where name= 'Free SGA Memory Available'";
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            long free = dr.GetInt64(0);
            return free;
        }
        
        public long getFreeSpaceSgaSharedPool()
        {
            string sql = "select Bytes from v$sgastat where pool = 'shared pool' and name = 'free memory'";
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            long free = dr.GetInt64(0);
            return free;
        }


        public void CloseConection() {
            conn.Dispose();
            cmd.Connection.Close();
        }

        #endregion

        #region SQLite
        public void createDBSQLite()
        {
            try
            {

                SQLiteConnection cnnSQLite = new SQLiteConnection("Data Source = monitoreo.sqlite3");
                SQLiteCommand SQLiteQuery = new System.Data.SQLite.SQLiteCommand(cnnSQLite);

                if (!File.Exists("./monitoreo.sqlite3"))
                {
                    SQLiteConnection.CreateFile("monitoreo.sqlite3");
                    MessageBox.Show("Se ha creado el archivo necesario para el manejo de la base de datos de monitoreo.");

                    string query = @"CREATE TABLE IF NOT EXISTS"
                                    + "[MONITOR] ("
                                    + "[ID]INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,"
                                    + "[TABLESPACE] NVARCHAR (20) NULL,"
                                    + "[TABLA] NVARCHAR(20) NULL,"
                                    + "[REGISTRO] NVARCHAR (20) NULL,"
                                    + "[SIZE] NVARCHAR (20) NULL,"
                                    + "[DIFERENCIA] NVARCHAR (20) NULL,"
                                    + "[TOTAL] NVARCHAR (20) NULL,"
                                    + "[FECHA] NVARCHAR (8))";
                    cnnSQLite.Open();
                    SQLiteQuery.CommandText = query;
                    SQLiteQuery.ExecuteNonQuery();
                    cnnSQLite.Close();
                }

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SQLite:" + ex);
            }
        }

        public void InsertarRegistroSQLite(string tablespace, string tabla,string registro, string size, string diferencia, string total) {

            SQLiteConnection cnnSQLite = new SQLiteConnection("Data Source = monitoreo.sqlite3");
            SQLiteCommand SQLiteQuery = new System.Data.SQLite.SQLiteCommand(cnnSQLite);

            cnnSQLite.Open();
            SQLiteQuery.CommandText = "INSERT INTO MONITOR (TABLESPACE,TABLA,REGISTRO,SIZE,DIFERENCIA,TOTAL,FECHA) VALUES ('"+ tablespace + "','"+ tabla+ "','" + registro + "','"+ size + "','"+ diferencia + "','"+ total + "','"+ DateTime.Now.ToString("dd/MM/yyyy") + "')";
            SQLiteQuery.ExecuteNonQuery();
            cnnSQLite.Close();

        }

        public void updateRegistroSQLite(string tablespace, string tabla,string size)
        {

            SQLiteConnection cnnSQLite = new SQLiteConnection("Data Source = monitoreo.sqlite3");
            SQLiteCommand SQLiteQuery = new System.Data.SQLite.SQLiteCommand(cnnSQLite);

            cnnSQLite.Open();
            SQLiteQuery.CommandText = "UPDATE MONITOR SET SIZE= '"+size+"'      WHERE  TABLESPACE ='" + tablespace + "'and TABLA = '" + tabla + "'";
            SQLiteQuery.ExecuteNonQuery();
            cnnSQLite.Close();

        }
        public void updateRegistroSQLiteFecha()
        {

            SQLiteConnection cnnSQLite = new SQLiteConnection("Data Source = monitoreo.sqlite3");
            SQLiteCommand SQLiteQuery = new System.Data.SQLite.SQLiteCommand(cnnSQLite);

            cnnSQLite.Open();
            SQLiteQuery.CommandText = "UPDATE MONITOR SET FECHA= '" + DateTime.Now.ToString("dd/MM/yyyy")+"'";
            SQLiteQuery.ExecuteNonQuery();
            cnnSQLite.Close();

        }

        public void updateRegistroSQLite(string tablespace, string tabla, string registros,string diferencia, string size)
        {

            int total = Int32.Parse(diferencia) * Int32.Parse(size);

            SQLiteConnection cnnSQLite = new SQLiteConnection("Data Source = monitoreo.sqlite3");
            SQLiteCommand SQLiteQuery = new System.Data.SQLite.SQLiteCommand(cnnSQLite);

            cnnSQLite.Open();
            SQLiteQuery.CommandText = "UPDATE MONITOR SET REGISTRO= '" + registros +"', DIFERENCIA='"+ diferencia + "' , TOTAL='" + total.ToString() + "'      WHERE  TABLESPACE ='" + tablespace + "'and TABLA = '" + tabla + "'";
            SQLiteQuery.ExecuteNonQuery();
            cnnSQLite.Close();

        }
        //public DataSet 

        public DataSet ConsultaInicial()
        {


            SQLiteConnection cnnSQLite = new SQLiteConnection("Data Source = monitoreo.sqlite3");
            SQLiteCommand SQLiteQuery = new System.Data.SQLite.SQLiteCommand(cnnSQLite);

            SQLiteDataAdapter da = new SQLiteDataAdapter();
            SQLiteQuery.CommandText = "Select * from monitor";
            da.SelectCommand = SQLiteQuery;
            DataSet ds = new DataSet();

            cnnSQLite.Open();
            da.Fill(ds);
            cnnSQLite.Close();

           

            return ds;
        }


        public int sumaTotal(string tb_name)
        {
            SQLiteConnection cnnSQLite = new SQLiteConnection("Data Source = monitoreo.sqlite3");
            string sql = "select sum(total) from monitor where tablespace= '" + tb_name + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, cnnSQLite);
            int tb_length = 0;
            cmd.CommandType = CommandType.Text;
            cnnSQLite.Open();

            SQLiteDataReader dr1 = cmd.ExecuteReader();

            dr1.Read();
            try
            {
                tb_length = dr1.GetInt32(0);
            }
            catch (Exception e)
            {
                return tb_length;

            }
            cnnSQLite.Close();


            return tb_length;
        }


        public string getFecha(string tb_name)
        {
            SQLiteConnection cnnSQLite = new SQLiteConnection("Data Source = monitoreo.sqlite3");
            string sql = "select distinct FECHA from monitor where tablespace= '" + tb_name + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, cnnSQLite);
            string fecha = string.Empty;
            cmd.CommandType = CommandType.Text;
            cnnSQLite.Open();

            SQLiteDataReader dr1 = cmd.ExecuteReader();
            dr1.Read();
            try
            {
                fecha = dr1.GetString(0);
            }
            catch (Exception e)
            {
                return fecha;

            }
            cnnSQLite.Close();


            return fecha;
        }
        #endregion

        #region Pruebas


        /*
        public void GetFunction()
        {
            try
            {
                DataSet ds = new DataSet();
                string ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id=system;Password=admin;";
                OracleConnection conn = new OracleConnection(ConnectionString);
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();

                cmd.Connection = conn;
                cmd.CommandText = "funcion_jose22";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(":resultado", OracleDbType.Int32);
                cmd.Parameters[":resultado"].Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(":NUM1", OracleDbType.Int32);
                cmd.Parameters[":NUM1"].Value = 22;

                cmd.Parameters.Add(":NUM2", OracleDbType.Int32);
                cmd.Parameters[":NUM2"].Value = 22;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
 
                string resultado = cmd.Parameters[":resultado"].Value.ToString(); //El resultado que venga se pasa a string
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
         */
        #endregion





    }
}

