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
using Android.Support.V4.App;

using ZXing.Mobile;
using System.Threading.Tasks;
using System.Threading;

namespace BuscaPreco_Android.Resources.classes
{
    public class Barcode
    {
        Application app;

        public Barcode()
        {
            app = new Application();
            MobileBarcodeScanner.Initialize(app);
        }

        /// <summary>
        /// Efetua a leitura de um código de barras; Por padrão, lê qualquer formato disponível atualmente
        /// </summary>
        /// <param name="p_sTextoExibicao">Texto que aparecerá para o usuário durante a leitura do código.</param>
        /// <returns></returns>
        public async Task<string> LerCodigo(string p_sTextoExibicao, bool p_bAutoFoco)
        {
            string sRet = string.Empty;           
            MobileBarcodeScanningOptions scanOptions = new MobileBarcodeScanningOptions();

            scanOptions.AutoRotate = false;
            scanOptions.UseNativeScanning = true;
            //scanOptions.TryInverted = true;
            scanOptions.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_13);
            //scanOptions.TryHarder = true;
            scanOptions.DelayBetweenAnalyzingFrames = 712;

            var scanner = new MobileBarcodeScanner();

            if (!string.IsNullOrEmpty(p_sTextoExibicao))
                scanner.TopText = p_sTextoExibicao;

            ZXing.Result zRet = null;
            
            new Thread(new ThreadStart(delegate
            {
                while (zRet == null)
                {
                    scanner.AutoFocus();
                    Thread.Sleep(1916);
                }
            })).Start();

            zRet = await scanner.Scan(scanOptions);

            if (zRet != null)
                sRet = (!string.IsNullOrEmpty(zRet.Text) ? zRet.Text : "");

            MobileBarcodeScanner.Uninitialize(app);

            return sRet;
        }
    }
}