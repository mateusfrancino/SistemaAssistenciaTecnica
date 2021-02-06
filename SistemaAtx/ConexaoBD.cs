using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAtx
{
    class ConexaoBD
    {
       
        //CONEXAO COM O BANCO DE DADOS LOCAL
        public string conec = "SERVER=localhost; DATABASE=atx_tec; UID=root; PWD=; PORT=3306;";


        public MySqlConnection con = null;

        public void AbrirCon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na conexão com o Banco! " + ex.Message);
            }


        }


        public void FecharCon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Close();
                con.Dispose();
                con.ClearAllPoolsAsync();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na conexão com o Banco! " + ex.Message);
            }
        }
    }

    class ValidaBD
    {

        //CONEXAO COM O BANCO DE DADOS LOCAL
        string conec = "SERVER=132.255.155.151; DATABASE=painel; UID=mateus; PWD=88563460; PORT=5306;";


        public MySqlConnection con1 = null;

        public void iniciarCon()
        {
            try
            {
                con1 = new MySqlConnection(conec);
                con1.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na conexão com o Banco! " + ex.Message);
            }


        }


        public void encerrarCon()
        {
            try
            {
                con1 = new MySqlConnection(conec);
                con1.Close();
                con1.Dispose();
                con1.ClearAllPoolsAsync();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na conexão com o Banco! " + ex.Message);
            }
        }


    }
}