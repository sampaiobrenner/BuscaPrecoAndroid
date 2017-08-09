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
    public class ProdutoDTO
    {
        public string Codigo { get; set; }
        public int Uk_Codigo { get; set; }
        public string Nome { get; set; }
        public string CodigoBarra { get; set; }
        public string Referencia { get; set; }
        public double ValorDeCusto { get; set; }
        public double ValorDeVenda { get; set; }
        public double ValorDeVenda2 { get; set; }
        public double ValorDeVenda3 { get; set; }
        public double Estoque { get; set; }
        public string Ativo { get; set; }
        public int CodigoDepartamento { get; set; }
    }
}