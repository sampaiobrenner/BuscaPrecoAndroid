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
    public class ConexaoBO
    {
        ConexaoDAO DAO;
        public ConexaoBO()
        {
            DAO = new ConexaoDAO();
        }

        public bool Salvar_LocalHost(string p_sIp, string p_sNomeDB, string p_sPorta, string p_sUsuario, string p_sSenha)
        {
            ConexaoDTO DTO = new ConexaoDTO();
            bool bRet;
            try
            {                
                DTO.IP = p_sIp;
                DTO.NomeDB = p_sNomeDB;
                DTO.Usuario = p_sUsuario;
                DTO.Senha = p_sSenha;
                DTO.Porta = p_sPorta;

                DAO.Deletar();
                bRet = DAO.Inserir(DTO);
            }
            catch (Exception)
            {
                bRet = false;
            }

            return bRet;
        }

        public ConexaoDTO ObterConexao_LocalHost()
        {
            return DAO.ObterConexao();
        }
    }
}