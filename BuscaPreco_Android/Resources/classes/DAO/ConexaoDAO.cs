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

namespace BuscaPreco_Android.Resources.classes
{
    public class ConexaoDAO : DbBuscaPrecoInfo
    {
        SQLiteDB pSqlLite;

        public ConexaoDAO()
        {
            pSqlLite = new SQLiteDB(base.NomeDB);
        }

        public bool Inserir(ConexaoDTO cDTO)
        {
            string sqlInserir = string.Format(
                "Insert Into conexao (NomeDB, IP, Porta, Usuario, Senha) Values ('{0}', '{1}', '{2}', '{3}', '{4}');"
                , cDTO.NomeDB
                , cDTO.IP
                , cDTO.Porta
                , cDTO.Usuario
                , cDTO.Senha
                );

            return pSqlLite.ExecCommand(sqlInserir);
        }

        public bool Deletar(int p_iID)
        {
            string sqlDeletar = string.Format("Delete From conexao Where Id = {0};", p_iID);
            return pSqlLite.ExecCommand(sqlDeletar);
        }

        public bool Deletar()
        {
            string sqlDeletar = "Delete From conexao";
            return pSqlLite.ExecCommand(sqlDeletar);
        }

        public ConexaoDTO ObterConexao()
        {
            ConexaoDTO conexaoDTO = null;
            Android.Database.ICursor sqlCursor = null;
            string sSQL = "Select * From conexao";

            try
            {
                sqlCursor = pSqlLite.GetRecordCursor(sSQL);

                if (sqlCursor == null || sqlCursor.Count == 0)
                    return null;

                conexaoDTO = new ConexaoDTO();

                sqlCursor.MoveToFirst();

                //if(sqlCursor.MoveToNext())
                // {
                conexaoDTO.ID = Convert.ToInt32(sqlCursor.GetString(sqlCursor.GetColumnIndex("_id")));
                conexaoDTO.NomeDB = sqlCursor.GetString(sqlCursor.GetColumnIndex("NomeDB"));
                conexaoDTO.IP = sqlCursor.GetString(sqlCursor.GetColumnIndex("IP"));
                conexaoDTO.Porta = sqlCursor.GetString(sqlCursor.GetColumnIndex("Porta"));
                conexaoDTO.Usuario = sqlCursor.GetString(sqlCursor.GetColumnIndex("Usuario"));
                conexaoDTO.Senha = sqlCursor.GetString(sqlCursor.GetColumnIndex("Senha"));
                // }


            }
            catch (SQLiteException e)
            {
                throw e;
            }

            return conexaoDTO;
        }

    }
}