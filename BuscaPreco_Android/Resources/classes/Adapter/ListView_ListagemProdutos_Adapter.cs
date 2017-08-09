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
using System.Drawing;

namespace BuscaPreco_Android.Resources.classes
{
    class ListView_ListagemProdutos_Adapter : BaseAdapter<ProdutoDTO>
    {
        private int Linha;
        private List<ProdutoDTO> pLstProdutos;
        private Context pContext;

        public ListView_ListagemProdutos_Adapter(Context p_context, List<ProdutoDTO> p_lstProduto)
        {
            this.pContext = p_context;
            this.pLstProdutos = p_lstProduto;
        }

        public override ProdutoDTO this[int position]
        {
            get
            {
                return pLstProdutos[position];
            }
        }

        public override int Count
        {
            get
            {
                return pLstProdutos.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
                row = LayoutInflater.From(pContext).Inflate(Resource.Layout.ListView_ListagemProduto_Row, null, false);

            /*
            TextView txtUkCodigo = row.FindViewById<TextView>(Resource.Id.txtListagem_UkCodigo);
            TextView txtNomeProduto = row.FindViewById<TextView>(Resource.Id.txtListagem_Nome);
            TextView txtCodigoBarra = row.FindViewById<TextView>(Resource.Id.txtListagem_CodigoBarra);
            TextView txtValorDeCusto = row.FindViewById<TextView>(Resource.Id.txtListagem_ValorCusto);
            */

            TextView txtUkCodigo = row.FindViewById<TextView>(Resource.Id.txtListagemProduto_UkCodigo);
            TextView txtNomeProduto = row.FindViewById<TextView>(Resource.Id.txtListagemProduto_Nome);
            TextView txtCodigoBarra = row.FindViewById<TextView>(Resource.Id.txtListagemProdutos_CodigoBarra);
            TextView txtValorDeCusto = row.FindViewById<TextView>(Resource.Id.txtListagemProduto_ValorDeCusto);
            TextView txtValorDeVenda = row.FindViewById<TextView>(Resource.Id.txtListagemProduto_ValorDeVenda);


            txtUkCodigo.TextAlignment = TextAlignment.Center;
            txtUkCodigo.Text = pLstProdutos[position].Uk_Codigo.ToString();

            txtNomeProduto.TextAlignment = TextAlignment.Center;
            txtNomeProduto.Text = pLstProdutos[position].Nome;

            txtCodigoBarra.TextAlignment = TextAlignment.Center;
            txtCodigoBarra.Text = pLstProdutos[position].CodigoBarra;

            txtValorDeCusto.TextAlignment = TextAlignment.TextEnd;
            txtValorDeCusto.Text =  string.Format("{0:0.00}", pLstProdutos[position].ValorDeCusto).Replace(".", ",");

            txtValorDeVenda.TextAlignment = TextAlignment.TextEnd;
            txtValorDeVenda.Text = string.Format("{0:0.00}", pLstProdutos[position].ValorDeVenda).Replace(".", ",");


            //if(Linha % 2 == 0)
            //    row.SetBackgroundColor(Android.Graphics.Color.Black);
            //else
            //    row.SetBackgroundColor(Android.Graphics.Color.DarkGray);

            Linha++;
            return row;
        }
    }
}