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

using ZXing.Mobile;

using BuscaPreco_Android.Resources.classes;

namespace BuscaPreco_Android
{
    [Activity(Label = "Listagem de produtos", Icon = "@drawable/Intellis")]

    class Activity_Produto_Listagem : Activity
    {
        private List<ProdutoDTO> pLstProdutoDTO;
        private ListView pListViewListagemProduto;
        private ImageButton pBtnBuscarPorCodigo;
        private ImageButton pBtnBiparCodigoBarras;
        private EditText pEdCodigoBarras;

        private bool BuscarOffline = true;

        public Activity_Produto_Listagem()
        {

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Produto_Listagem);
            pListViewListagemProduto = FindViewById<ListView>(Resource.Id.lstView_Produto_Listagem);
            pBtnBuscarPorCodigo = FindViewById<ImageButton>(Resource.Id.imgBtnListagemProduto_Pesquisar);
            pBtnBiparCodigoBarras = FindViewById<ImageButton>(Resource.Id.btnListagemProduto_BiparCodigo);
            pEdCodigoBarras = FindViewById<EditText>(Resource.Id.edListaProduto_Pesquisa);

            pBtnBuscarPorCodigo.Click += delegate { ListarProdutos(pEdCodigoBarras.Text.Trim()); };
            pBtnBiparCodigoBarras.Click += delegate { BiparCodigoEBuscar(); };
            pListViewListagemProduto.ItemClick += PListViewListagemProduto_ItemClick;

            ListarProdutos();
        }

        private async void BiparCodigoEBuscar()
        {
            Barcode lerCodBarras = new Barcode();
            string sCodBarra = await lerCodBarras.LerCodigo("Posicione o código de barras no centro do leitor.", false);

            if (string.IsNullOrEmpty(sCodBarra))
                sCodBarra = string.Empty;

            pEdCodigoBarras.Text = sCodBarra;
            ListarProdutos(sCodBarra);
        }

        private void ListarProdutos(string p_sFiltro = null)
        {
            if (string.IsNullOrEmpty(p_sFiltro))
                pLstProdutoDTO = ObterListagemProdutos("");
            else
                pLstProdutoDTO = ObterListagemProdutos(p_sFiltro);

            if (pLstProdutoDTO == null || pLstProdutoDTO.Count == 0)
            {
                pListViewListagemProduto.Adapter = null;

                string sMsg = "Nenhum produto encontrado.";
                Toast.MakeText(this, sMsg, ToastLength.Long).Show();

                return;
            }

            ListView_ListagemProdutos_Adapter prodAdapter = new ListView_ListagemProdutos_Adapter(this, pLstProdutoDTO);
            pListViewListagemProduto.Adapter = prodAdapter;           

        }

        private void PListViewListagemProduto_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //string msg = pLstProdutoDTO[e.Position].Uk_Codigo.ToString();
            //Toast.MakeText(this, msg, ToastLength.Short).Show();

            ExibirDetalhesProdutoSelecionado(pLstProdutoDTO[e.Position].Uk_Codigo);
        }

        private void ExibirDetalhesProdutoSelecionado(int p_iUkCodigoProduto)
        {
            ProdutoBO prodBO = new ProdutoBO();
            ProdutoDTO prodDTO = prodBO.ObterProdutos_Localhost(p_iukCodigo: p_iUkCodigoProduto)[0];

            FragmentTransaction fragTransaction = FragmentManager.BeginTransaction();
            DiagFragment_DetalheProduto detalhesProduto = new DiagFragment_DetalheProduto(prodDTO);
            detalhesProduto.Cancelable = true;
            detalhesProduto.Show(fragTransaction, "Detalhes do produto");
            
        }

        private List<ProdutoDTO> ObterListagemProdutos(string p_sBusca)
        {
            List<ProdutoDTO> lstProdDTO;
            ProdutoBO prodBO = new ProdutoBO();

            if (string.IsNullOrEmpty(p_sBusca))
                p_sBusca = string.Empty;

            if (!BuscarOffline)
            {
                if (Util.SomenteNumero(p_sBusca))
                    lstProdDTO = prodBO.ObterProdutos(p_sCodigoBarra: p_sBusca);
                else if (p_sBusca.Length > 3)
                    lstProdDTO = prodBO.ObterProdutos(p_sNome: p_sBusca);
                else
                    lstProdDTO = prodBO.ObterProdutos(true, 0, 128);
            }
            else
            {
                if (Util.SomenteNumero(p_sBusca))
                    lstProdDTO = prodBO.ObterProdutos_Localhost(p_sCodigoBarra: p_sBusca);
                else if (p_sBusca.Length > 3)
                    lstProdDTO = prodBO.ObterProdutos_Localhost(p_sNome: p_sBusca);
                else
                    lstProdDTO = prodBO.ObterProdutos_Localhost(p_iLimit_APartir: 0, p_iLimit_Qtd: 128);
            }

            return lstProdDTO;
        }
    }
}