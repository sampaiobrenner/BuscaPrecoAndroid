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
    [Activity(Label = "Configuração inicial", Icon = "@drawable/Intellis")]
    public class Activity_Configuracao_Inicial : Activity
    {
        private EditText pEdIP;
        private EditText pEdNomeDB;
        private EditText pEdPorta;
        private EditText pEdUsuario;
        private EditText pEdSenha;
        private Button pBtnSalvar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ConfiguracaoAcessoInicial);
            MapearComponentes();
            CarregarConexao();
        }

        private void MapearComponentes()
        {
            pEdIP = FindViewById<EditText>(Resource.Id.edConfig_IP);
            pEdNomeDB = FindViewById<EditText>(Resource.Id.edConfig_NomeDB);
            pEdPorta = FindViewById<EditText>(Resource.Id.edConfig_Porta);
            pEdUsuario = FindViewById<EditText>(Resource.Id.edConfig_Usuario);
            pEdSenha = FindViewById<EditText>(Resource.Id.edConfig_Senha);
            pBtnSalvar = FindViewById<Button>(Resource.Id.btnConfig_Salvar);

            pBtnSalvar.Click += PBtnSalvar_Click;
        }

        private void CarregarConexao()
        {
            ConexaoBO cBO = new ConexaoBO();
            ConexaoDTO cDTO = cBO.ObterConexao_LocalHost();

            if (cDTO == null)
            {
                string sMensagem = "Preencha os campos!";
                Toast.MakeText(this, sMensagem, ToastLength.Long).Show();

                return;
            }

            pEdIP.Text = cDTO.IP;
            pEdNomeDB.Text = cDTO.NomeDB;
            pEdPorta.Text = cDTO.Porta;
            pEdUsuario.Text = cDTO.Usuario;
            pEdSenha.Text = cDTO.Senha;
        }

        private void PBtnSalvar_Click(object sender, EventArgs e)
        {

            bool bValidarTela = ValidarTela();

            if (!bValidarTela)
                return;

            ConexaoBO conBO = new ConexaoBO();
            bool bSalvou = conBO.Salvar_LocalHost(pEdIP.Text, pEdNomeDB.Text, pEdPorta.Text, pEdUsuario.Text, pEdSenha.Text);

            string sMensagem = string.Empty;

            if (bSalvou)
                sMensagem = "Configurações salvas com sucesso.";
            else
                sMensagem = "Erro ao salvar configurações.";

            Toast.MakeText(this, sMensagem, ToastLength.Short).Show();
        }

        private bool ValidarTela()
        {
            bool bRet = true;
            string sRet = string.Empty;

            if(string.IsNullOrEmpty(pEdIP.Text))
            {
                sRet = "Informe o IP";
                bRet = false;
            }
            else if(string.IsNullOrEmpty(pEdNomeDB.Text))
            {
                sRet = "Informe o nome do banco";
                bRet = false;
            }
            else if (string.IsNullOrEmpty(pEdPorta.Text))
            {
                sRet = "Informe a porta de acesso";
                bRet = false;
            }
            else if (string.IsNullOrEmpty(pEdUsuario.Text))
            {
                sRet = "Informe o usuário";
                bRet = false;
            }
            else if (string.IsNullOrEmpty(pEdSenha.Text))
            {
                sRet = "Informe a senha";
                bRet = false;
            }

            if (!bRet)
                Toast.MakeText(this, sRet, ToastLength.Long).Show();

            return bRet;            
        }
    }
}