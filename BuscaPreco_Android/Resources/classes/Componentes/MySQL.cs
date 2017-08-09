using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Diagnostics;

namespace BuscaPreco_Android.Resources.classes
{
    public class MySQL
    {
        private MySqlConnection myConn;
        //private MySqlDataAdapter bdAdapter;
        //private DataSet bdDataSet;

        private string serverName { get; set; }
        private int port { get; set; }
        private string userName { get; set; }
        private string password { get; set; }
        private string databaseName { get; set; }
        private string connString { get; set; }

        public MySQL()
        {
            SetStringConexao("intellis.ddns.net", 3000, "root", "intel55systems", "teste");
        }

        public void SetStringConexao(string sIpServidor, int iPorta, string sUsuario, string sSenha, string sBdNome)
        {

            serverName = sIpServidor;
            port = iPorta;
            userName = sUsuario;
            password = (sSenha.Length == 0) ? "''" : sSenha;
            databaseName = sBdNome;

            connString = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverName, port, databaseName, userName, password);
        }
/*
        public MySqlDataReader ExecutarSelect(string sql)
        {
            var mConn = new MySqlConnection(connString);

            try
            {
                mConn.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            if (mConn.State == ConnectionState.Open)
            {
                var myCmd = new MySqlCommand(sql, mConn);
                MySqlDataReader dr = myCmd.ExecuteReader();

                return dr;
            }

            return null;
        }
        */
        public MySqlCommand ExecutarSelect(string sql)
        {
            var mConn = new MySqlConnection(connString);

            try
            {
                mConn.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            if (mConn.State == ConnectionState.Open)
            {
                var myCmd = new MySqlCommand(sql, mConn);
                myCmd.CommandTimeout = 10000000;
                return myCmd;
            }

            return null;
        }

        public bool execCommand(string sSQL)
        {

            try
            {
                myConn = new MySqlConnection(connString);
                myConn.Open();

                MySqlCommand myCmd = new MySqlCommand(sSQL, myConn);

                myCmd.ExecuteNonQuery();

                myConn.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }

        /*
         TESTE
         */

    }
}
