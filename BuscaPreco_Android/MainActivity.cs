using Android.App;
using Android.Widget;
using Android.OS;
using BuscaPreco_Android.Resources.classes;
using System.Threading;

namespace BuscaPreco_Android
{
    [Activity(Label = "Busca Preço Intellis", MainLauncher = true, Icon = "@drawable/Intellis")]
    public class MainActivity : Activity
    {
        private EditText pEdUsuario;
        private EditText pEdSenha;
        private Button pBtnLogar;
        private ImageButton pImgButtonConfig;
        private ProgressBar pProgressoLogar;
        private Switch pSwitchTipoAcesso;
        private LinearLayout pMainUsuarioSenha;
        private TextView pTxtLogin_FazerLogin;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            pEdUsuario = FindViewById<EditText>(Resource.Id.edLogin_Usuario);
            pEdSenha = FindViewById<EditText>(Resource.Id.edLogin_Senha);
            pBtnLogar = FindViewById<Button>(Resource.Id.btnLogin);
            pProgressoLogar = FindViewById<ProgressBar>(Resource.Id.pgbLogin_Progresso);
            pImgButtonConfig = FindViewById<ImageButton>(Resource.Id.btnMain_Config);
            pSwitchTipoAcesso = FindViewById<Switch>(Resource.Id.switchMain_AcessoOffline);
            pSwitchTipoAcesso.CheckedChange += pSwitchAcessoOffline_CheckedChange;
            pMainUsuarioSenha = FindViewById<LinearLayout>(Resource.Id.lay_main_loginSenha);
            pTxtLogin_FazerLogin = FindViewById<TextView>(Resource.Id.txtLogin_FazerLogin);

            Sessao.TipoDeAcesso = (pSwitchTipoAcesso.Checked) ? TipoAcesso.Online : TipoAcesso.Offline;

            mudarLayoutTipoAcesso();

            pBtnLogar.Click += PBtnLogar_Click;
            pImgButtonConfig.Click += delegate { ExibirJanelaDeConfiguracaoAcesso(); };
        }

        private void pSwitchAcessoOffline_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            mudarLayoutTipoAcesso();
        }

        private void mudarLayoutTipoAcesso()
        {

            Sessao.TipoDeAcesso = (pSwitchTipoAcesso.Checked) ? TipoAcesso.Online : TipoAcesso.Offline;

            if (Sessao.TipoDeAcesso == TipoAcesso.Online)
            {
                pEdUsuario.Text = "";
                pEdUsuario.Enabled = true;
                pEdUsuario.Hint = "Usuário";



                pEdSenha.Text = "";
                pEdSenha.Enabled = true;
                pEdSenha.Hint = "Senha";

                pMainUsuarioSenha.Visibility = Android.Views.ViewStates.Visible;
                pTxtLogin_FazerLogin.SetTextColor(Android.Graphics.Color.Rgb(74, 110, 169));
                pTxtLogin_FazerLogin.Text = "Acesso online";

            }
            else
            {
                // pEdUsuario.Text = "";
                // pEdUsuario.Enabled = false;
                //pEdSenha.Text = "";
                //pEdSenha.Enabled = false;
                pTxtLogin_FazerLogin.SetTextColor(Android.Graphics.Color.Gray);
                pTxtLogin_FazerLogin.Text = "Acesso offline";
                pMainUsuarioSenha.Visibility = Android.Views.ViewStates.Gone;
            }
        }


        private void PBtnLogar_Click(object sender, System.EventArgs e)
        {
            if (Sessao.TipoDeAcesso == TipoAcesso.Online)
                AcessarOnline();
            else
                AcessarOfflline();
        }

        private void AcessarOnline()
        {
            bool bValidacaoTela = ValidarTela();

            if (!Util_Android.ConectadoNaInternetWifi(this))
            {
                Toast.MakeText(this, "Conecte a uma wifi.", ToastLength.Long).Show();
                return;
            }

            if (!bValidacaoTela)
                return;

            pProgressoLogar.Visibility = Android.Views.ViewStates.Visible;
            Thread threadLogar = new Thread(Logar_Thread);
            threadLogar.Start();
        }

        private void AcessarOfflline()
        {
            ExibirMensagemAcessoOffline();
            StartActivity(typeof(Activity_AreaDeTrabalho));
        }

        private void ExibirMensagemAcessoOffline()
        {
            //string sMensagem = "No modo offline, alguns recursos não estão disponíveis.";
            //Dialog dialogo = Util_Android.ObterDialogoAlerta_OK(this, sMensagem, "Atenção!");
            //dialogo.Show();
 
        }

        private void ExibirJanelaDeConfiguracaoAcesso()
        {
            StartActivity(typeof(Activity_Configuracao_Inicial));
        }

        private void Logar_Thread()
        {
            bool bLoginAutorizado = FazerLogin();
            RunOnUiThread(() =>
            {

                if (bLoginAutorizado)
                {
                    //RunOnUiThread(() => { Toast.MakeText(this, "Login realizado", ToastLength.Short).Show(); });

                    //Produto_Listagem prod = new Produto_Listagem();

                    StartActivity(typeof(Activity_AreaDeTrabalho));

                }
                else
                    RunOnUiThread(() => { Toast.MakeText(this, "Erro ao fazer login", ToastLength.Long).Show(); });


                pProgressoLogar.Visibility = Android.Views.ViewStates.Invisible;
            });

        }

        private bool FazerLogin()
        {
            UsuarioBO uBO = new UsuarioBO();
            UsuarioDTO uDTO = uBO.ObterUsuarioPorLogin(pEdUsuario.Text, pEdSenha.Text);

            if (uDTO == null)
                return false;

            return true;

        }

        private bool ValidarTela()
        {
            string strValidacao;

            if (string.IsNullOrEmpty(pEdUsuario.Text.Trim()))
            {
                strValidacao = "Usuário inválido";
                pEdUsuario.RequestFocus();
                Toast.MakeText(this, strValidacao, ToastLength.Short).Show();
                return false;
            }
            else if (string.IsNullOrEmpty(pEdSenha.Text.Trim()))
            {
                strValidacao = "Senha inválida";
                pEdSenha.RequestFocus();
                Toast.MakeText(this, strValidacao, ToastLength.Short).Show();
                return false;
            }

            return true;
        }
    }
}

