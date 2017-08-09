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

using MySql.Data.MySqlClient;
using Android.Database.Sqlite;

namespace BuscaPreco_Android.Resources.classes
{
    public enum TipoDB { MySQL, SQLite }

    public class ProdutoDAO : DbBuscaPrecoInfo
    {

        MySQL mySQL;
        SQLiteDB pSqlLite;
        private string sqlBuscaProduto_MySQL;
        private string sqlBuscaProduto_SQLite;

        public ProdutoDAO()
        {
            pSqlLite = new SQLiteDB(base.NomeDB);
            mySQL = new MySQL();

            ConexaoDAO conDAO = new ConexaoDAO();
            ConexaoDTO conDTO = conDAO.ObterConexao();

            if (conDTO != null)
                mySQL.SetStringConexao(conDTO.IP, Convert.ToInt32(conDTO.Porta), conDTO.Usuario, conDTO.Senha, conDTO.NomeDB);
                        
            DefinirSQLs();
        }

        private void DefinirSQLs()
        {
            sqlBuscaProduto_MySQL = string.Empty;
            sqlBuscaProduto_MySQL += " Select ";
            sqlBuscaProduto_MySQL += " 	Codigo                              AS Codigo ";
            sqlBuscaProduto_MySQL += " 	, uk_codigo                         AS Uk_Codigo ";
            sqlBuscaProduto_MySQL += " 	, upper(nome)                              AS Nome ";
            sqlBuscaProduto_MySQL += " 	, CodigoBarra                       AS CodigoBarra ";
            sqlBuscaProduto_MySQL += " 	, Referencia                        AS Referencia ";
            sqlBuscaProduto_MySQL += " 	, ValorDeCusto                      AS ValorDeCusto ";
            sqlBuscaProduto_MySQL += " 	, valor                             AS ValorDeVenda ";
            sqlBuscaProduto_MySQL += " 	, vlValorPrazo                      AS ValorDeVenda2 ";
            sqlBuscaProduto_MySQL += " 	, vlPreco3                          AS ValorDeVenda3 ";
            sqlBuscaProduto_MySQL += " 	, Ativo                             AS Ativo";
            sqlBuscaProduto_MySQL += " 	, Estoque                           AS Estoque";
            sqlBuscaProduto_MySQL += " From ";
            sqlBuscaProduto_MySQL += " 	produto ";
            sqlBuscaProduto_MySQL += " Where 1 = 1 ";
            sqlBuscaProduto_MySQL += " 	$$(condicao) ";


            sqlBuscaProduto_SQLite = string.Empty;
            sqlBuscaProduto_SQLite += " Select ";
            sqlBuscaProduto_SQLite += " 	_id ";
            sqlBuscaProduto_SQLite += " 	, Uk_codigo ";
            sqlBuscaProduto_SQLite += " 	, Nome ";
            sqlBuscaProduto_SQLite += " 	, CodigoBarra";
            sqlBuscaProduto_SQLite += " 	, PrecoDeCusto ";
            sqlBuscaProduto_SQLite += " 	, PrecoDeVenda1 ";
            sqlBuscaProduto_SQLite += " 	, PrecoDeVenda2 ";
            sqlBuscaProduto_SQLite += " 	, Estoque ";
            sqlBuscaProduto_SQLite += " From ";
            sqlBuscaProduto_SQLite += " 	produto ";
            sqlBuscaProduto_SQLite += " Where 1 = 1 ";
            sqlBuscaProduto_SQLite += " 	$$(condicao) ";
        }

        /// <summary>
        /// Retorna uma lista de produtos
        /// As strings são testadas com Like e não com =
        /// </summary>
        /// <param name="p_sNome">Nome do produto9</param>
        /// <param name="p_sAtivo">Situação do produto</param>
        /// <param name="p_sCodigoBarra">Código de barras</param>
        /// <param name="p_iUkCodigo">Código único</param>
        /// <param name="p_iLimit_APartir">Limit a partir do registro x. Ex: Limit x, 10</param>
        /// <param name="p_iLimit_Qtd">Limite </param>
        /// <returns>Retorna uma lista de ProdutoDTO</returns>
        public List<ProdutoDTO> ObterProdutos(TipoDB enumTipoDB, string p_sNome = null, string p_sAtivo = null, string p_sCodigoBarra = null, int? p_iUkCodigo = null, int? p_iLimit_APartir = null, int? p_iLimit_Qtd = null)
        {
            string sSQL = string.Empty;
            string sFiltro = string.Empty;

            if (!string.IsNullOrEmpty(p_sNome))
                sFiltro += string.Format(" And nome Like '%{0}%' ", p_sNome);

            if (!string.IsNullOrEmpty(p_sAtivo))
                sFiltro += string.Format(" And ativo = '{0}' ", p_sAtivo);

            if (!string.IsNullOrEmpty(p_sCodigoBarra))
                sFiltro += string.Format(" And CodigoBarra = '{0}' ", p_sCodigoBarra);

            if (p_iUkCodigo != null)
                sFiltro += string.Format(" And uk_codigo = '{0}' ", p_iUkCodigo);

            sFiltro += (" order by trim(nome) ");

            if (p_iLimit_Qtd != null)
            {
                if (p_iLimit_APartir != null)
                    sFiltro += string.Format(" Limit {0}, {1} ", p_iLimit_APartir, p_iLimit_Qtd);
                else
                    sFiltro += string.Format(" Limit {0} ", p_iLimit_Qtd);
            }

            if (enumTipoDB == TipoDB.MySQL)
            {
                sSQL = this.sqlBuscaProduto_MySQL.Replace("$$(condicao)", sFiltro);
                return this.ObterProdutos(sSQL);
            }
            else if (enumTipoDB == TipoDB.SQLite)
            {
                sSQL = this.sqlBuscaProduto_SQLite.Replace("$$(condicao)", sFiltro);
                return this.ObterProdutos_SQLite(sSQL);
            }

            return null;
        }

        private List<ProdutoDTO> ObterProdutos_SQLite(string p_sql)
        {
            List<ProdutoDTO> lstProdDTO = null;
            Android.Database.ICursor sqlCursor = null;

            try
            {
                sqlCursor = pSqlLite.GetRecordCursor(p_sql);

                if (sqlCursor == null || sqlCursor.Count == 0)
                    return null;

                lstProdDTO = new List<ProdutoDTO>(); 
                sqlCursor.MoveToFirst();

                do
                {
                    ProdutoDTO DTO = new ProdutoDTO();
                    DTO.Codigo = sqlCursor.GetString(sqlCursor.GetColumnIndex("_id"));
                    DTO.CodigoBarra = sqlCursor.GetString(sqlCursor.GetColumnIndex("CodigoBarra"));
                    DTO.Uk_Codigo = sqlCursor.GetInt(sqlCursor.GetColumnIndex("Uk_codigo"));
                    DTO.Nome = sqlCursor.GetString(sqlCursor.GetColumnIndex("Nome"));
                    DTO.ValorDeCusto = sqlCursor.GetDouble(sqlCursor.GetColumnIndex("PrecoDeCusto"));
                    DTO.ValorDeVenda = sqlCursor.GetDouble(sqlCursor.GetColumnIndex("PrecoDeVenda1"));
                    DTO.ValorDeVenda2 =sqlCursor.GetDouble(sqlCursor.GetColumnIndex("PrecoDeVenda2"));
                    DTO.Estoque = sqlCursor.GetDouble(sqlCursor.GetColumnIndex("Estoque"));

                    lstProdDTO.Add(DTO);
                } while (sqlCursor.MoveToNext());
            }
            catch (SQLiteException ex)
            {

                throw ex;
            }

            return lstProdDTO;

        }

        /// <summary>
        /// Obtém uma listagem de produtos, do banco MySQL
        /// </summary>
        /// <param name="p_sSQL">Script a ser executado</param>
        /// <returns></returns>
        private List<ProdutoDTO> ObterProdutos(string p_sSQL)
        {
            List<ProdutoDTO> lstProdutos = new List<ProdutoDTO>();
            MySqlDataReader myDR;

            using (MySqlCommand myComm = mySQL.ExecutarSelect(p_sSQL))
            {

                myDR = myComm.ExecuteReader();

                while (myDR.Read())
                {
                    ProdutoDTO DTO = new ProdutoDTO();

                    DTO.Codigo = myDR["Codigo"].ToString();
                    DTO.Uk_Codigo = Convert.ToInt32(myDR["Uk_Codigo"].ToString());
                    DTO.Nome = myDR["Nome"].ToString();
                    DTO.CodigoBarra = myDR["CodigoBarra"].ToString();
                    DTO.Referencia = myDR["Referencia"].ToString();
                    DTO.ValorDeCusto = double.Parse((string.IsNullOrEmpty(myDR["ValorDeCusto"].ToString())) ? "0" : myDR["ValorDeCusto"].ToString());
                    DTO.ValorDeVenda = double.Parse((string.IsNullOrEmpty(myDR["ValorDeVenda"].ToString())) ? "0" : myDR["ValorDeVenda"].ToString());
                    DTO.ValorDeVenda2 = double.Parse((string.IsNullOrEmpty(myDR["ValorDeVenda2"].ToString())) ? "0" : myDR["ValorDeVenda2"].ToString());
                    DTO.ValorDeVenda3 = double.Parse((string.IsNullOrEmpty(myDR["ValorDeVenda3"].ToString())) ? "0" : myDR["ValorDeVenda3"].ToString());
                    DTO.Ativo = myDR["Ativo"].ToString();
                    DTO.Estoque = double.Parse((string.IsNullOrEmpty(myDR["Estoque"].ToString())) ? "0" : myDR["Estoque"].ToString());

                    lstProdutos.Add(DTO);
                }

                if (!myDR.IsClosed)
                {
                    myComm.Connection.Close();
                    myDR.Close();
                }
            }

            return lstProdutos;
        }

        /// <summary>
        /// Insere um produto no banco SQLite
        /// </summary>
        /// <param name="p_localhost">Garante que o banco tratado é o SQLite</param>
        /// <param name="p_dto">Objeto ProdutoDTO a ser inserido</param>
        /// <returns></returns>
        public bool Inserir(bool p_localhost, ProdutoDTO p_dto)
        {
            string sqlInsert =
                @"
                Insert Into produto(Nome, CodigoBarra, PrecoDeCusto, PrecoDeVenda1, PrecoDeVenda2, Estoque, uk_codigo)
                 Values 
                ('{0}', '{1}', {2}, {3}, {4}, {5}, {6});";

            string strSQL = string.Format(sqlInsert
                , Util.RemoveCaracteresEspeciais(p_dto.Nome)
                , p_dto.CodigoBarra
                , p_dto.ValorDeCusto.ToString().Replace(",", ".")
                , p_dto.ValorDeVenda.ToString().Replace(",", ".")
                , p_dto.ValorDeVenda2.ToString().Replace(",", ".")
                , p_dto.Estoque.ToString().Replace(",", ".")
                , p_dto.Uk_Codigo
                );

            return pSqlLite.ExecCommand(strSQL);
        }

        /// <summary>
        /// Deleta todos os produtos do banco local SQLite
        /// </summary>
        /// <param name="p_localhost">Garante que os dados apagados são SQLite</param>
        /// <returns></returns>
        public bool DeletarTodos(bool p_localhost)
        {
            if (!p_localhost)
                return false;

            string sqlDelete = "Delete From produto;";
            return pSqlLite.ExecCommand(sqlDelete);
        }


    }
}