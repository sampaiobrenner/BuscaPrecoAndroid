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

using BuscaPreco_Android.Resources.classes;

namespace BuscaPreco_Android
{
    [Activity(Label = "Desktop", Icon ="@drawable/Intellis")]
    public class Activity_AreaDeTrabalho : Activity
    {

        private Button pBtnListagemProdutos;
        private Button pBtnSincronizarProdutos;
        private Button pBtnSincronizarUsuarios;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AreaDeTrabalho);
            MapearComponentes();
            DefinirTela();
        }

        private void MapearComponentes()
        {
            pBtnListagemProdutos = FindViewById<Button>(Resource.Id.btnAreaDeTrabalho_ListagemProdutos);
            pBtnSincronizarProdutos = FindViewById<Button>(Resource.Id.btnAreaDeTrabalho_SincronizarProdutos);
            pBtnSincronizarUsuarios = FindViewById<Button>(Resource.Id.btnAreaDeTrabalho_SincronizarUsuarios);

            pBtnListagemProdutos.Click += PBtnListagemProdutos_Click;
            pBtnSincronizarProdutos.Click += PBtnSincronizarProdutos_Click;
        }

        private void DefinirTela()
        {
            if (Sessao.TipoDeAcesso == TipoAcesso.Offline)
                pBtnSincronizarProdutos.Enabled = false;
        }

        private void PBtnSincronizarProdutos_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Activity_Sincronizar_Produto));
        }

        private void PBtnListagemProdutos_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Activity_Produto_Listagem));
        }
    }
}