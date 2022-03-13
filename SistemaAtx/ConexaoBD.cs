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
        public string conec = "SERVER=2.57.91.5; DATABASE=u103483530_atx_sist_tec; UID=u103483530_atxtec; PWD=88563460Deus@; PORT=3306;";


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

        //CONEXAO COM O BANCO DE DADOS REMOTO
        string conec = "SERVER=2.57.91.5; DATABASE=u103483530_atx_tec; UID=u103483530_atxsistemas; PWD=88563460Deus@; PORT=3306;";


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
        // edi cuzao

    }
}