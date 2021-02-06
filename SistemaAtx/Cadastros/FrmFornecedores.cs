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
    public partial class FrmFornecedores : Form
    {
        //CODIGO DE CONEXAO COM O BANCO DE DADOS MYSQL
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        string id;

        public FrmFornecedores()
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
                grid.Columns[4].HeaderText = "CNPJ";
                grid.Columns[5].HeaderText = "Endereco";
                grid.Columns[6].HeaderText = "Bairro";
                grid.Columns[7].HeaderText = "Cidade";
                grid.Columns[8].HeaderText = "E-mail";


                //FORMATAR COLUNA PARA MOEDA
                //grid.Columns[3].DefaultCellStyle.Format = "Date";
                //grid.Columns[4].DefaultCellStyle.Format = "C2";

                grid.Columns[0].Visible = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao formatar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }

        }

        private void BuscarNome()
        {
            con.AbrirCon();
            sql = "SELECT * FROM fornecedores where nome LIKE @nome order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtBuscarNome.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();

            FormatarDG();
        }


        private void Listar()
        {
            try
            {

                con.AbrirCon();
                sql = "SELECT * FROM fornecedores order by nome asc";
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
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
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
            btnEditar.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                limparCampos();
                habilitarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
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


                //CÓDIGO DO BOTÃO PARA EDITAR
                con.AbrirCon();
                sql = "UPDATE fornecedores SET nome = @nome, telefone = @telefone, celular = @celular, cnpj = @cnpj, endereco = @endereco, bairro = @bairro, cidadeuf = @cidadeuf, email = @email where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
                cmd.Parameters.AddWithValue("@cidadeuf", txtCidadeUf.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Fornecedor Editado com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
                txtBuscarNome.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar produto, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);

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


                //CÓDIGO DO BOTÃO PARA SALVAR
                con.AbrirCon();
                sql = "INSERT INTO fornecedores (nome, telefone, celular, cnpj, endereco, bairro, cidadeuf, email) VALUES (@nome, @telefone, @celular, @cnpj, @endereco, @bairro, @cidadeuf, @email)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd.Parameters.AddWithValue("@cnpj", txtCnpj.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
                cmd.Parameters.AddWithValue("@cidadeuf", txtCidadeUf.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Fornecedor Salvo com Sucesso!", "Registros Salvos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
                txtBuscarNome.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar fornecedor, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Fornecedor?", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (resultado == DialogResult.Yes)
                {
                    //CÓDIGO DO BOTÃO PARA EXCLUIR
                    con.AbrirCon();
                    sql = "DELETE FROM fornecedores where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharCon();

                    MessageBox.Show("Fornecedor Excluido com Sucesso!", "Registro Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNovo.Enabled = true;
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                    limparCampos();
                    desabilitarCampos();
                    Listar();
                    txtBuscarNome.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir produto, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void FrmFornecedores_Load(object sender, EventArgs e)
        {
            try
            {
                Listar();
                desabilitarCampos();
                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar itens e carregar fornecedores, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
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
        }
    }
}
