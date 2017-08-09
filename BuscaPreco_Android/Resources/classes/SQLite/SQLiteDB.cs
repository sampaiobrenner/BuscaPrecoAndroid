using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Database.Sqlite;
using System.IO;
using System.Data.SqlClient;

namespace BuscaPreco_Android.Resources.classes
{
    public class SQLiteDB
    {
        private SQLiteDatabase pSqlDB;
        private SQLiteTabelas comandosSQL;
        private string pNomeDB;
        private bool BancoDeDadosDisponivel = false;
        private string strSQL = string.Empty;

        public string Mensagem { get; private set;  }

        public SQLiteDB(string p_sNomeDB)
        {
            this.pNomeDB = p_sNomeDB;

            try
            {
                comandosSQL = new SQLiteTabelas();
                AbrirConexao(true);
            }
            catch (SQLiteException ex)
            {

                throw ex;
            }
        }

        public bool ExecCommand(string p_comandoSQL)
        {
            try
            {
                if (!BancoDeDadosDisponivel)
                    AbrirConexao();

                pSqlDB.ExecSQL(p_comandoSQL);
                Mensagem = "Script executado com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                Mensagem = ex.Message;
                return false;
            }
            finally
            {
                if (BancoDeDadosDisponivel)
                    FinalizarConexao();
            }       
        }

        public void AbrirConexao(bool bCriarTabelas = false)
        {
            try
            {
                string sDbLocal = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string sPasta = Path.Combine(sDbLocal, pNomeDB);

                bool bDbExiste = File.Exists(sPasta);

                if(!bDbExiste)
                    pSqlDB = SQLiteDatabase.OpenOrCreateDatabase(sPasta, null);        
                else
                    pSqlDB = SQLiteDatabase.OpenDatabase(sPasta, null, DatabaseOpenFlags.OpenReadwrite);

                BancoDeDadosDisponivel = true;

                if(bCriarTabelas)
                    CriarTabelas();

                Mensagem = "Banco de dados criado com sucesso.";

            }
            catch (SQLiteException ex)
            {
                Mensagem = ex.Message;
                BancoDeDadosDisponivel = false;
                throw ex;
            }
        }
        public void FinalizarConexao()
        {
            if (BancoDeDadosDisponivel)
            {
                pSqlDB.Close();
                BancoDeDadosDisponivel = false;
            }
        }

        private void CriarTabelas()
        {
            ExecCommand(comandosSQL.SqlCreateTable_Conexao);
            ExecCommand(comandosSQL.SqlCreateTable_Produto);
            ExecCommand(comandosSQL.SqlCreateIndex_Produto);
        }

        public Android.Database.ICursor GetRecordCursor(string p_sSQL)
        {
            Android.Database.ICursor sqlCursor = null;

            if (!BancoDeDadosDisponivel)
                AbrirConexao();

            try
            {
                sqlCursor = pSqlDB.RawQuery(p_sSQL, null);

                if(sqlCursor == null)
                {
                    Mensagem = "Nenhum registro encontrado.";
                }
            }
            catch (SQLiteException ex)
            {
                Mensagem = ex.Message;
            }

            return sqlCursor;
        }

    }
}