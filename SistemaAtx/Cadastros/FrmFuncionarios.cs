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
    public partial class FrmFuncionarios : Form
    {
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        string id;
        public FrmFuncionarios()
        {
            InitializeComponent();
        }

        private void BuscarNome()
        {
            try
            {
                con.AbrirCon();
                sql = "SELECT pro.id, pro.nomecompleto, pro.telefone, pro.cpf, pro.nome, pro.usuario, pro.senha, forn.nome, pro.funcao FROM funcionarios as pro INNER JOIN funcoes as forn ON pro.funcao = forn.id where pro.nomecompleto LIKE @nomecompleto order by pro.nomecompleto asc";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nomecompleto", txtBuscarNome.Text + "%");
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
                MessageBox.Show("Erro ao buscar produtos, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void CarregarCombobox()
        {
            try
            {
                con.AbrirCon();
                sql = "SELECT * FROM funcoes order by nome asc";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                CBFuncao.DataSource = dt;
                CBFuncao.ValueMember = "id";
                CBFuncao.DisplayMember = "nome";

                con.FecharCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar funções, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void FormatarDG()
        {
            
                grid.Columns[0].HeaderText = "ID";
                grid.Columns[1].HeaderText = "Nome Completo";
                grid.Columns[2].HeaderText = "Telefone";
                grid.Columns[3].HeaderText = "CPF";
                grid.Columns[4].HeaderText = "Nome";
                grid.Columns[5].HeaderText = "Usuário";
                grid.Columns[6].HeaderText = "Senha";
                grid.Columns[7].HeaderText = "Função";
               // grid.Columns[8].HeaderText = "Cod Função";

                //FORMATAR COLUNA PARA MOEDA
                //grid.Columns[3].DefaultCellStyle.Format = "Date";
                //grid.Columns[4].DefaultCellStyle.Format = "C2";

                grid.Columns[0].Visible = false;
               // grid.Columns[8].Visible = false;

        }


        private void Listar()
        {
            try
            {
                      
                con.AbrirCon();
                sql = "SELECT * FROM funcionarios order by nome asc";
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
            txtNomeCompleto.Text = "";
            txtTelefone.Text = "";
            txtDataNascimento.Text = "";
            txtCpf.Text = "";
            txtNome.Text = "";
            txtBuscarNome.Text = "";
            txtUsuario.Text = "";
            txtSenha.Text = "";

        }

        private void desabilitarCampos()
        {
            txtNomeCompleto.Enabled = false;
            txtTelefone.Enabled = false;
            txtDataNascimento.Enabled = false;
            txtCpf.Enabled = false;
            txtNome.Enabled = false;
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = false;
            CBFuncao.Enabled = false;
        }

        private void habilitarCampos()
        {
            txtNomeCompleto.Enabled = true;
            txtTelefone.Enabled = true;
            txtDataNascimento.Enabled = true;
            txtCpf.Enabled = true;
            txtNome.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            btnExcluir.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = true;
            CBFuncao.Enabled = true;
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

                if (txtUsuario.Text == "")
                {
                    MessageBox.Show("Preencha o Usuário", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsuario.Focus();
                    return;
                }

                if (txtSenha.Text == "")
                {
                    MessageBox.Show("Preencha a Senha", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSenha.Focus();
                    return;
                }

                //CÓDIGO DO BOTÃO PARA EDITAR
                con.AbrirCon();
                sql = "UPDATE funcionarios SET nomecompleto = @nomecompleto, telefone = @telefone, cpf = @cpf, nome = @nome, usuario = @usuario, senha = @senha, funcao = @funcao where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nomecompleto", txtNomeCompleto.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@cpf", txtCpf.Text);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                cmd.Parameters.AddWithValue("@funcao", CBFuncao.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Funcionário Editado com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Erro ao editar Funcionário, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);

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

                if (txtUsuario.Text == "")
                {
                    MessageBox.Show("Preencha o Usuário", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsuario.Focus();
                    return;
                }

                if (txtSenha.Text == "")
                {
                    MessageBox.Show("Preencha a Senha", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSenha.Focus();
                    return;
                }

                //CÓDIGO DO BOTÃO PARA SALVAR
                con.AbrirCon();
                sql = "INSERT INTO funcionarios (nomecompleto, telefone, cpf, nome, usuario, senha, funcao) VALUES (@nomecompleto, @telefone, @cpf, @nome, @usuario, @senha, @funcao)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nomecompleto", txtNomeCompleto.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@cpf", txtCpf.Text);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                cmd.Parameters.AddWithValue("@funcao", CBFuncao.Text);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Funcionário Salvo com Sucesso!", "Registros Salvos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
                txtBuscarNome.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro salvar Funcionário, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Funcionário?", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (resultado == DialogResult.Yes)
                {
                    //CÓDIGO DO BOTÃO PARA EXCLUIR
                    con.AbrirCon();
                    sql = "DELETE FROM funcionarios where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharCon();

                    MessageBox.Show("Funcionário Excluido com Sucesso!", "Registro Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void FrmFuncionarios_Load(object sender, EventArgs e)
        {


                Listar();
                CarregarCombobox();
                desabilitarCampos();
                limparCampos();

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
            txtNomeCompleto.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtTelefone.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtCpf.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtUsuario.Text = grid.CurrentRow.Cells[5].Value.ToString();
            txtSenha.Text = grid.CurrentRow.Cells[6].Value.ToString();
            CBFuncao.Text = grid.CurrentRow.Cells[7].Value.ToString();
        }
    }
}
