using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAtx.Login
{
    public partial class Login : Form
    {
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        ValidaBD con1 = new ValidaBD();
        public Login()
        {
            InitializeComponent();
        }



        private void ChamarLogin()
        {
            try
            {
                if (txtCodigo.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Insira sua chave de ativação do sistema \n Caso não possua uma, entre em contato para obter. \n Telefone/WhatsApp: (33) 99808-0977 \n E-mail: vendas@atxsistemas.com \n Site: www.atxsistemas.com ", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Text = "";
                    txtCodigo.Focus();
                    return;
                }

                if (txtUsuario.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha o Usuário", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                    return;
                }

                if (txtSenha.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha a Senha", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSenha.Text = "";
                    txtSenha.Focus();
                    return;
                }

                // AQUI INICIA O CÓDIGO PARA A VALIDAÇÃO

                MySqlCommand cmdVerificar1;
                MySqlDataReader reader1;


                con1.iniciarCon();
                cmdVerificar1 = new MySqlCommand("SELECT * FROM atvcliente where codigo = @codigo", con1.con1);
                cmdVerificar1.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                reader1 = cmdVerificar1.ExecuteReader();


                if (reader1.HasRows)
                {
                    //EXTRAINDO INFORMAÇÕES DO LOGIN
                    while (reader1.Read())
                    {
                        Program.statusAtivacao = Convert.ToString(reader1["status"]);

                    }
                    //CÓDIGO DO BOTÃO PARA SALVAR NO LOCAL
                    con.AbrirCon();
                    sql = "INSERT INTO ativacao (codigo, nomepc, data) VALUES (@codigo, @nomepc, NOW())";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                    cmd.Parameters.AddWithValue("@nomepc", Program.nomePc);

                con.FecharCon();
                cmd.ExecuteNonQuery();

                    try
                    {
                        //CÓDIGO DO BOTÃO PARA SALVAR NA NUVEM
                        con1.iniciarCon();
                        sql = "INSERT INTO ativacao (codigo, nomepc, data) VALUES (@codigo, @nomepc, NOW())";
                        cmd = new MySqlCommand(sql, con1.con1);
                        cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                        cmd.Parameters.AddWithValue("@nomepc", Program.nomePc);
                        cmd.ExecuteNonQuery();
                        con1.encerrarCon();
                        
                 }
                 catch (Exception ex)
                  {

                     MessageBox.Show("Erro de conexão, Houve um erro ao tentar salvar suas informações na nuvem" + ex, "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);

                  }


                txtUsuario.Focus();
                }

                else
                {
                    MessageBox.Show("Insira sua chave de ativação do sistema, CORRETAMENTE \n Caso não possua uma, entre em contato para obter. \n Telefone/WhatsApp (33) 99808-0977 \n E-mail: vendas@atxsistemas.com \n Site: www.atxsistemas.com ", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Text = "";
                    txtCodigo.Focus();
                    return;
                }
                con1.encerrarCon();
            }
            catch (Exception Ex)
            {

                MessageBox.Show("Erro de autenticação, Verifique sua conexão e tente novamente!" + Ex, "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            // AQUI INICIA O CÓDIGO PARA O LOGIN

            MySqlCommand cmdVerificar;
            MySqlDataReader reader;


            con.AbrirCon();
            cmdVerificar = new MySqlCommand("SELECT * FROM funcionarios where usuario = @usuario and senha = @senha", con.con);
            cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
            cmdVerificar.Parameters.AddWithValue("@senha", txtSenha.Text);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                //EXTRAINDO INFORMAÇÕES DO LOGIN
                while (reader.Read())
                {
                    Program.nomeUsuario = Convert.ToString(reader["nome"]);
                    Program.cargoUsuario = Convert.ToString(reader["funcao"]);

                }

                FrmMenu form = new FrmMenu();
                this.Hide();
                form.Show();
                txtUsuario.Text = "";
            }

            else
            {
                MessageBox.Show("Poxa você errou seu usuário ou sua senha, Vamos tentar novamente?", "Dados Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Text = "";
                txtSenha.Text = "";
                txtUsuario.Focus();
            }

            con.FecharCon();


        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            ChamarLogin();

        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChamarLogin();
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChamarLogin();
            }
        }

        private void CarregarAtivacao()
        {
            try
            {
                // AQUI INICIA O CÓDIGO PARA O BUSCAR A CHAVE DE ATIVAÇÃO NO BD

                MySqlCommand cmdVerificar;
                MySqlDataReader reader;


                con.AbrirCon();
                cmdVerificar = new MySqlCommand("SELECT * FROM ativacao ", con.con);
                reader = cmdVerificar.ExecuteReader();

                if (reader.HasRows)
                {
                    //EXTRAINDO INFORMAÇÕES DO LOGIN
                    while (reader.Read())
                    {
                        Program.codigoAtivacao = Convert.ToString(reader["codigo"]);

                    }

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Erro de autenticação, Houve um erro ao buscar sua chave, tente novamente!" + Ex, "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void Login_Load(object sender, EventArgs e)
        {

            CarregarAtivacao();
            guna2AnimateWindow1.SetAnimateWindow(this);
            guna2ShadowForm1.SetShadowForm(this);
            /* DEFINE A SOMBRA DO FORMULÁRIO AO SER EXBIDO NOS CANTOS */
            txtUsuario.Text = "";
            txtUsuario.Focus();
            /* LIMPA O CAMPO USUÁRIO E COLOCA O CURSOR NELE */
            txtCodigo.Text = Program.codigoAtivacao;

            Program.nomePc = Environment.MachineName;


        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
