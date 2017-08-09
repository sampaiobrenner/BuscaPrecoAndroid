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
    public class ConexaoDTO
    {
        public int ID{ get; set; }
        public string NomeDB { get; set; }
        public string IP{ get; set; }
        public string Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}