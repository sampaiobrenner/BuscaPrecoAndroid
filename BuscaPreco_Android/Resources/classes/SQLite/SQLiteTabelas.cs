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

namespace BuscaPreco_Android.Resources.classes
{
    public class SQLiteTabelas
    {
        public string SqlCreateTable_Conexao { get; private set; }
        public string SqlCreateTable_Produto { get; private set; }
        public string SqlCreateIndex_Produto { get; private set; }

        public SQLiteTabelas()
        {
            SqlCreateTable_Conexao = 
            @"
            CREATE TABLE IF NOT EXISTS conexao 
            (
                _id Integer Primary Key AutoIncrement
                , NomeDB VARCHAR
                , IP VARCHAR
                , Porta VARCHAR
                , Usuario VARCHAR
                , Senha VARCHAR
            );";

            SqlCreateTable_Produto =
            @"
            CREATE TABLE IF NOT EXISTS produto
            (
                _id Integer Primary Key AutoIncrement
	            , Nome VARCHAR
                , CodigoBarra     VARCHAR
	            , PrecoDeCusto DOUBLE
                , PrecoDeVenda1     DOUBLE
	            , PrecoDeVenda2 DOUBLE
                , Estoque         DOUBLE
	            , Uk_codigo INTEGER
            );";

            SqlCreateIndex_Produto = "CREATE INDEX idx_produto_uk_codigo ON produto(uk_codigo);";

        }

    }
}