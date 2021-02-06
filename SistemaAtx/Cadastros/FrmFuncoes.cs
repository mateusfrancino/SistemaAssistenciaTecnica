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
    public partial class FrmFuncoes : Form
    {
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        string id;
        public FrmFuncoes()
        {
            InitializeComponent();
        }

        private void FormatarDG()
        {
            try
            {
                grid.Columns[0].HeaderText = "ID";
                grid.Columns[1].HeaderText = "Nome";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao formatar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }

        }

        private void BuscarNome()
        {
            con.AbrirCon();
            sql = "SELECT * FROM funcoes where nome LIKE @nome order by nome asc";
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
                sql = "SELECT * FROM funcoes order by nome asc";
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

        }

        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = false;
        }

        private void habilitarCampos()
        {
            txtNome.Enabled = true;
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
                sql = "UPDATE funcoes SET nome = @nome where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Função Editada com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Erro ao editar Função, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);

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
                sql = "INSERT INTO funcoes (nome) VALUES (@nome)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Função Salva com Sucesso!", "Registros Salvos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
                txtBuscarNome.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro salvar Função, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir a Função?", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (resultado == DialogResult.Yes)
                {
                    //CÓDIGO DO BOTÃO PARA EXCLUIR
                    con.AbrirCon();
                    sql = "DELETE FROM funcoes where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharCon();

                    MessageBox.Show("Função Excluida com Sucesso!", "Registro Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Erro ao excluir função, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        private void FrmFuncoes_Load(object sender, EventArgs e)
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

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
