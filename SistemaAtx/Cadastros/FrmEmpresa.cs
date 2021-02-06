using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAtx.Cadastros
{
    public partial class FrmEmpresa : Form
    {
        //CODIGO DE CONEXAO COM O BANCO DE DADOS MYSQL
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        string id;
        public FrmEmpresa()
        {
            InitializeComponent();
        }

        private void FormatarDG()
        {
            try
            {
                grid.Columns[0].HeaderText = "ID";
                grid.Columns[1].HeaderText = "Nome";
                grid.Columns[2].HeaderText = "Telefone";
                grid.Columns[3].HeaderText = "Celular";
                grid.Columns[4].HeaderText = "CPF";
                grid.Columns[5].HeaderText = "Endereço";
                grid.Columns[6].HeaderText = "Bairro";
                grid.Columns[7].HeaderText = "Cidade";
                grid.Columns[8].HeaderText = "E-mail";
                grid.Columns[9].HeaderText = "Site";
                grid.Columns[10].HeaderText = "Mensagem";


                grid.Columns[2].Visible = false;
                grid.Columns[0].Visible = false;
                grid.Columns[3].Visible = false;
                grid.Columns[4].Visible = false;
                grid.Columns[5].Visible = false;
                grid.Columns[6].Visible = false;
                grid.Columns[7].Visible = false;
                grid.Columns[8].Visible = false;
                grid.Columns[9].Visible = false;
                grid.Columns[10].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao formatar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }

        }


        private void Listar()
        {
            try
            {

                con.AbrirCon();
                sql = "SELECT * FROM dados_empresa order by nome asc";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                con.FecharCon();

                FormatarDG();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }


        private void limparCampos()
        {
            txtNome.Text = "";
            txtTelefone.Text = "";
            txtCelular.Text = "";
            txtCnpj.Text = "";
            txtEndereco.Text = "";
            txtBairro.Text = "";
            txtCidadeUf.Text = "";
            txtEmail.Text = "";
            txtSite.Text = "";
            txtMsg.Text = "";

        }

        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtTelefone.Enabled = false;
            txtCelular.Enabled = false;
            txtCnpj.Enabled = false;
            txtEndereco.Enabled = false;
            txtBairro.Enabled = false;
            txtCidadeUf.Enabled = false;
            txtEmail.Enabled = false;
            txtMsg.Enabled = false;
            txtSite.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
        }

        private void habilitarCampos()
        {
            txtNome.Enabled = true;
            txtTelefone.Enabled = true;
            txtCelular.Enabled = true;
            txtCnpj.Enabled = true;
            txtEndereco.Enabled = true;
            txtBairro.Enabled = true;
            txtCidadeUf.Enabled = true;
            txtEmail.Enabled = true;
            btnExcluir.Enabled = true;
            txtMsg.Enabled = true;
            txtSite.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /*
         * Para criar uma forma de não permitir que meu usuário insira
           mais de uma empresa no sistema, assim que meu form faz o load
           eu desabilito todos os campos e só permito que eles possam ser 
           ativados novamente se for clicaco em alguma linha do grid, nesse
           caso já irei mandar uma linha preenchida no BD, com a mensagem 
           "clique aqui para alterar os dados da sua empresa", dessa forma
           eu tiro a nescessidade de fazer uma consulta no banco de dados, apenas
           para contar a quantidade de linhas, se quantidade fosse > 1 então
           os campos ficariam desabilitados. de qualquer forma isso resolveu meu problema.
         */

        private void FrmEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                Listar();
                desabilitarCampos();
                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar clientes! \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // VERIFICAR SE OS CAMPOS NOME E VALOR ESTÃO VAZIOS
                if (txtNome.Text.ToString().Trim() == "")
                {
                    txtNome.Text = "";
                    MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Focus();
                    return;
                }

                if (txtCnpj.Text.ToString().Trim() == "")
                {
                    txtNome.Text = "";
                    MessageBox.Show("Preencha o CNPJ", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Focus();
                    return;
                }


                //CÓDIGO DO BOTÃO PARA EDITAR/SALVAR
                con.AbrirCon();
                sql = "UPDATE dados_empresa SET nome = @nome, telefone = @telefone, celular = @celular, cnpj = @cnpj, endereco = @endereco, bairro = @bairro, cidadeuf = @cidadeuf, email = @email, site = @site, msg = @msg where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
                cmd.Parameters.AddWithValue("@cidadeuf", txtCidadeUf.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@site", txtSite.Text);
                cmd.Parameters.AddWithValue("@msg", txtMsg.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Empresa Salva com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnExcluir.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
                grid.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar dados da sua Empresa, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);

            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = true;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtTelefone.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtCelular.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtCnpj.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[5].Value.ToString();
            txtBairro.Text = grid.CurrentRow.Cells[6].Value.ToString();
            txtCidadeUf.Text = grid.CurrentRow.Cells[7].Value.ToString();
            txtEmail.Text = grid.CurrentRow.Cells[8].Value.ToString();
            txtSite.Text = grid.CurrentRow.Cells[9].Value.ToString();
            txtMsg.Text = grid.CurrentRow.Cells[10].Value.ToString();
        }
    }
}

