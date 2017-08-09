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
    public class ProdutoBO
    {
        ProdutoDAO DAO;

        public ProdutoBO()
        {
            DAO = new ProdutoDAO();
        }

        public List<ProdutoDTO> ObterProdutos(bool p_bAtivos, int p_iLimit_APartir, int p_iLimit_Qtd)
        {
            string sAtivo = (p_bAtivos ? "S" : "N");

            return DAO.ObterProdutos(TipoDB.MySQL, p_sAtivo: sAtivo, p_iLimit_APartir: p_iLimit_APartir, p_iLimit_Qtd: p_iLimit_Qtd);
        }

        public List<ProdutoDTO> ObterProdutos_Localhost(int? p_iukCodigo = null, string p_sCodigoBarra = null, string p_sNome = null, int? p_iLimit_APartir = null, int? p_iLimit_Qtd = null)
        {
            return DAO.ObterProdutos(TipoDB.SQLite,p_sCodigoBarra: p_sCodigoBarra, p_iUkCodigo: p_iukCodigo, p_sNome: p_sNome, p_iLimit_APartir: p_iLimit_APartir, p_iLimit_Qtd: p_iLimit_Qtd);
        }

        public ProdutoDTO ObterProduto_Localhost(int p_iukCodigo)
        {
            return DAO.ObterProdutos(TipoDB.SQLite, p_iUkCodigo: p_iukCodigo)[0];
        }

        public ProdutoDTO ObterProdutoViaUkCodigo(int p_iukCodigo)
        {
            return DAO.ObterProdutos(TipoDB.MySQL, p_iUkCodigo: p_iukCodigo)[0];
        }

        public List<ProdutoDTO> ObterProdutos(string p_sCodigoBarra = null, string p_sNome = null)
        {
            return DAO.ObterProdutos(TipoDB.MySQL, p_sCodigoBarra: p_sCodigoBarra, p_sNome: p_sNome);
        }

        public bool DeletarTodos_Localhost()
        {
            bool bRet = DAO.DeletarTodos(true);
            return bRet;
        }

        public bool Salvar_Localhost(ProdutoDTO p_DTO)
        {
            bool bRet;

            try
            {
                p_DTO.ValorDeCusto = Util.ObterDoubleComPonto(p_DTO.ValorDeCusto);
                p_DTO.ValorDeVenda = Util.ObterDoubleComPonto(p_DTO.ValorDeVenda);
                p_DTO.ValorDeVenda2 = p_DTO.ValorDeVenda2;

                bRet = DAO.Inserir(true, p_DTO);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return bRet;
        }

    }
}