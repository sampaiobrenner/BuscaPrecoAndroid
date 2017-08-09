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
using Android.Net;

namespace BuscaPreco_Android.Resources.classes
{
    public static class Util_Android
    {
        /// <summary>
        /// Retorna um booleano que indica se o aparelho está conectado em alguma Wifi
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool ConectadoNaInternetWifi(Context context)
        {
            ConnectivityManager connManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            NetworkInfo netInfo = connManager.GetNetworkInfo(ConnectivityType.Wifi);

            if (netInfo != null && netInfo.IsConnected)
                return true;

            return false;
        }

        /// <summary>
        /// Retorna um Dialogo, com botão OK
        /// </summary>
        /// <param name="p_context">Contexto</param>
        /// <param name="p_sMsg">Mensagem que será exibida</param>
        /// <param name="p_sTitulo">Título da caixa de diálogo</param>
        /// <returns>Caixa de diálogo</returns>
        public static Dialog ObterDialogoAlerta_OK(Context p_context, string p_sMsg, string p_sTitulo)
        {
            AlertDialog.Builder alerta = new AlertDialog.Builder(p_context);
            alerta.SetTitle(p_sTitulo);
            alerta.SetMessage(p_sMsg);
            alerta.SetNeutralButton("OK", (senderAlerts, e) =>
            {
                Toast.MakeText(p_context, "Teste", ToastLength.Long).Show();
            });

            Dialog dialogo = alerta.Create();

            return dialogo;
        }
    }
}