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
using System.Text.RegularExpressions;

namespace BuscaPreco_Android.Resources.classes
{
    public static class Util
    {

        /// <summary>
        /// Remove caracteres de uma string, conforme informado por argumento
        /// </summary>
        /// <param name="p_strPalavra">String completa</param>
        /// <param name="p_strRemover">Caracteres que deseja que sejam removidos</param>
        /// <returns></returns>
        public static string RemoveCaracteresEspeciais(string p_strPalavra, string p_strRemover)
        {
            int intTamanho = p_strRemover.Length;
            string strCaracterParaRemover;

            for (int i = 0; i < intTamanho; i++)
            {
                strCaracterParaRemover = p_strRemover.Substring(i, 1);

                p_strPalavra = p_strPalavra.Replace(Convert.ToChar(strCaracterParaRemover), ' ');
            }

            return p_strPalavra;

        }

        /// <summary>
        /// Remove os principais caracteres especiais: |'&\
        /// </summary>
        /// <param name="p_strPalavra">String que deseja remover os caracteres</param>
        /// <returns></returns>
        public static string RemoveCaracteresEspeciais(string p_strPalavra)
        {
            string p_strRemover = "|'&\'";
            string strCaracterParaRemover;
            int intTamanho = p_strRemover.Length;

            for (int i = 0; i < intTamanho; i++)
            {
                strCaracterParaRemover = p_strRemover.Substring(i, 1);

                p_strPalavra = p_strPalavra.Replace(Convert.ToChar(strCaracterParaRemover), ' ');
            }

            return p_strPalavra;
        }

        /// <summary>
        /// Retorna um booleano que informa se a string possui somente números
        /// </summary>
        /// <returns></returns>
        public static bool SomenteNumero(string p_sTexto)
        {
            if (string.IsNullOrEmpty(p_sTexto))
                return false;

            bool bSomenteNumero = Regex.IsMatch(p_sTexto, "^[0-9]+$");

            return bSomenteNumero;
        }

        public static double ObterDoubleComPonto(double p_dValor)
        {
            System.Globalization.NumberFormatInfo numInfo = new System.Globalization.NumberFormatInfo();
            numInfo.NumberGroupSeparator = ".";
            double dRet = Convert.ToDouble(p_dValor, numInfo);

            return dRet;
        }

    }
}