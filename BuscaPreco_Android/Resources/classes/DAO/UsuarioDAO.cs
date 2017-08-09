using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BuscaPreco_Android.Resources.classes
{
    public class UsuarioDAO
    {

        MySQL mySQL;

        public UsuarioDAO()
        {
            mySQL = new MySQL();

            ConexaoDAO conDAO = new ConexaoDAO();
            ConexaoDTO conDTO = conDAO.ObterConexao();

            if (conDTO != null)
                mySQL.SetStringConexao(conDTO.IP, Convert.ToInt32(conDTO.Porta), conDTO.Usuario, conDTO.Senha, conDTO.NomeDB);
        }

        public UsuarioDTO ObterUsuarioPorLoginSenha(string p_strLogin, String p_strSenha)
        {
            UsuarioDTO DTO = null;
            MySqlDataReader myDR;
            string strSQL = string.Empty;

            strSQL += " SELECT ";
            strSQL += " 	codigo ";
            strSQL += " 	, nome	 ";
            strSQL += " 	, ativo	 ";
            strSQL += " FROM ";
            strSQL += " 	usuario ";
            strSQL += " WHERE ";
            strSQL += "     ativo = 'S' ";
            strSQL += " 	AND login = '" + p_strLogin + "'";
            strSQL += " 	AND senha = '" + p_strSenha + "'";

            using (MySqlCommand myComm = mySQL.ExecutarSelect(strSQL))
            {
                myDR = myComm.ExecuteReader();

                if(myDR.Read())
                {
                    DTO = new UsuarioDTO();

                    DTO.Codigo = Convert.ToInt32(myDR[0].ToString());
                    DTO.Nome = myDR[1].ToString();
                    DTO.Ativo = myDR[2].ToString();
                    DTO.Login = p_strLogin;
                }

                if (!myDR.IsClosed)
                {
                    myComm.Connection.Close();
                    myDR.Close();
                }
            }

            return DTO;
        }

    }
}
