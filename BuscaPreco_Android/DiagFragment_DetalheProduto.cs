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
    [Activity(Label = "Detalhes do produto", MainLauncher = true, Icon = "@drawable/Intellis")]

    class DiagFragment_DetalheProduto : DialogFragment
    {
        private ProdutoDTO pProduto;
        private EditText pEdCodigoBarra;
        private EditText pEdNomeProduto;
        private EditText pEdValorDeCusto;
        private EditText pEdValorDeVenda;
        private EditText pEdValorDeVendaPrazo;
        private EditText pEdEstoque;

        public DiagFragment_DetalheProduto(ProdutoDTO p_prodDTO)
        {
            if (p_prodDTO == null)
                throw new System.ArgumentException("O parâmetro não pode ser nulo", "Erro!");

            this.pProduto = p_prodDTO;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
       
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Dialog_DetalheProduto, container, false);
 
            CarregarComponentes(view);
            ExibirDetalhesProduto();

            return view;
        }

        private void CarregarComponentes(View p_view)
        {
            pEdCodigoBarra = p_view.FindViewById<EditText>(Resource.Id.edDetalheProduto_CodigoBarra);
            pEdNomeProduto = p_view.FindViewById<EditText>(Resource.Id.edDetalheProduto_NomeProduto);
            pEdValorDeCusto = p_view.FindViewById<EditText>(Resource.Id.edDetalheProduto_ValorCusto);
            pEdValorDeVenda = p_view.FindViewById<EditText>(Resource.Id.edDetalheProduto_ValorVenda);
            pEdValorDeVendaPrazo = p_view.FindViewById<EditText>(Resource.Id.edDetalheProduto_ValorVendaPrazo);
            pEdEstoque = p_view.FindViewById<EditText>(Resource.Id.edDetalheProduto_Estoque);
        }

        private void ExibirDetalhesProduto()
        {
            pEdCodigoBarra.Text = pProduto.CodigoBarra;
            pEdNomeProduto.Text = pProduto.Nome;
            pEdValorDeCusto.Text = "R$ " + string.Format("{0:0.00}", pProduto.ValorDeCusto).Replace(".", ",");
            pEdValorDeVenda.Text = "R$ " + string.Format("{0:0.00}", pProduto.ValorDeVenda).Replace(".", ",");
            pEdValorDeVendaPrazo.Text = "R$ " +  string.Format("{0:0.00}", pProduto.ValorDeVenda2).Replace(".", ",");
            pEdEstoque.Text = pProduto.Estoque.ToString();
        }

    }
}