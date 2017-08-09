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
using System.Threading;

using BuscaPreco_Android.Resources.classes;
using System.Threading.Tasks;
using Android.Content.Res;

namespace BuscaPreco_Android
{
    [Activity(Label = "Sincronização de produtos", Icon = "@drawable/Intellis", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Activity_Sincronizar_Produto : Activity
    {

        private int QtdErro = 0;
        private Button pBtnSincronizar;
        private ProgressBar pPgbStatusGeral;
        private TextView pTxtAviso;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SincronizarProduto);
            MapearComponentes();
        }

        private void MapearComponentes()
        {
            pBtnSincronizar = FindViewById<Button>(Resource.Id.btnSincronizarProduto_Sincronizar);
            pPgbStatusGeral = FindViewById<ProgressBar>(Resource.Id.pgbSincronizarProduto_Status);
            pTxtAviso = FindViewById<TextView>(Resource.Id.txtSincronizarProduto_Aviso1);

            pTxtAviso.Text = string.Empty;
            pPgbStatusGeral.Visibility = ViewStates.Invisible;
            pBtnSincronizar.Click += delegate
            {
                IniciarSincronizacao();
            };
        }

        private void IniciarSincronizacao()
        {
            pPgbStatusGeral.Visibility = Android.Views.ViewStates.Visible;
            pPgbStatusGeral.Indeterminate = true;
            pTxtAviso.Text = "Atenção!\r\nNão interrompa este procedimento.";
            pBtnSincronizar.Enabled = false;

            Thread threadExecutar = new Thread(IniciarSincronizacaoDeProdutos_Thread);
            threadExecutar.Start();
        }

        private void IniciarSincronizacaoDeProdutos_Thread()
        {
            ProdutoBO pBO = new ProdutoBO();

            QtdErro = 0;

            RunOnUiThread(() =>
            {
                pPgbStatusGeral.Visibility = ViewStates.Visible;
                pPgbStatusGeral.Indeterminate = true;
                pTxtAviso.Text = "Atenção!\r\nNão interrompa este procedimento.\r\n\r\nPreparando a sincronização.";
            });


            List<ProdutoDTO> lstProdutosOnline = new List<ProdutoDTO>();
            lstProdutosOnline = pBO.ObterProdutos(true, 0, 100000);

            if (lstProdutosOnline == null || lstProdutosOnline.Count == 0)
            {
                RunOnUiThread(() =>
                {
                    string sMensagem = "Nenhum produto para sincronizar";
                    pPgbStatusGeral.Indeterminate = false;
                    pTxtAviso.Text = string.Empty;
                    Toast.MakeText(this, sMensagem, ToastLength.Short).Show();
                });

                return;
            }

            RunOnUiThread(() =>
            {
                pTxtAviso.Text = "Preparando sincronização.";
                pBO.DeletarTodos_Localhost();
            });


            int Cont = 0;
            double dPercConcluido;
            int iTotalDeRegistros = lstProdutosOnline.Count;

            foreach (ProdutoDTO DTO in lstProdutosOnline)
            {

                if (!pBO.Salvar_Localhost(DTO))
                    QtdErro++;

                Cont++;
                dPercConcluido = (Cont * 100) / iTotalDeRegistros;

                RunOnUiThread(() =>
                {

                    pPgbStatusGeral.Progress = Cont;
                    pTxtAviso.Text = string.Format("{0} - {1}\r\n{2}%\r\nAguarde...", Cont, iTotalDeRegistros, dPercConcluido);

                });

            }

            RunOnUiThread(() =>
            {
                pBtnSincronizar.Enabled = true;
                pPgbStatusGeral.Visibility = ViewStates.Invisible;
                pTxtAviso.Text = string.Format("Sincronização finalizada.\r\n{0} erros ocorridos de {1} produto(s) sincronizado(s).", QtdErro, iTotalDeRegistros);
            });



        }
    }
}