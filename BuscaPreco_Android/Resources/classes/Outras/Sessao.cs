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
    public enum TipoAcesso { Online, Offline }

    public static class Sessao
    {
        public static bool Logado { get; set; }
        public static TipoAcesso TipoDeAcesso { get; set; }
    }
}