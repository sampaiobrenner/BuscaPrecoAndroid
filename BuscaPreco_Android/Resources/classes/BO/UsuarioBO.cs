using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaPreco_Android.Resources.classes
{
    public class UsuarioBO
    {
        private UsuarioDAO DAO;

        public UsuarioBO()
        {
            DAO = new UsuarioDAO();
        }

        public UsuarioDTO ObterUsuarioPorLogin(String p_strLogin, String p_strSenha)
        {
            return DAO.ObterUsuarioPorLoginSenha(p_strLogin, p_strSenha);
        }

    }
}
