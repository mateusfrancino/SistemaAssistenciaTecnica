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
    public partial class FrmEstoque : Form
    {
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        string id;
        public FrmEstoque()
        {
            InitializeComponent();
        }

        private void BuscarNome()
        {
            try
            {
                con.AbrirCon();
                sql = "SELECT pro.id, pro.nome, pro.codigodebarras, pro.valor_compra, pro.valor_venda, pro.estoque, forn.nome, pro.descricao, pro.fornecedor FROM produtos as pro INNER JOIN fornecedores as forn ON pro.fornecedor = forn.id where pro.nome LIKE @nome order by pro.nome asc";
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produtos, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void FormatarDG()
        {
            try
            {
                grid.Columns[0].HeaderText = "ID";
                grid.Columns[1].HeaderText = "Nome";
                grid.Columns[2].HeaderText = "Cod Barras";
                grid.Columns[3].HeaderText = "Valor Compra";
                grid.Columns[4].HeaderText = "Valor Venda";
                grid.Columns[5].HeaderText = "Estoque Atual";
                grid.Columns[6].HeaderText = "Fornecedor";
                grid.Columns[7].HeaderText = "Descrição";
                grid.Columns[8].HeaderText = "Cod Fornecedor";

                //FORMATAR COLUNA PARA MOEDA
                grid.Columns[3].DefaultCellStyle.Format = "C2";
                grid.Columns[4].DefaultCellStyle.Format = "C2";

                grid.Columns[0].Visible = false;
                grid.Columns[8].Visible = false;
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
                sql = "SELECT pro.id, pro.nome, pro.codigodebarras, pro.valor_compra, pro.valor_venda, pro.estoque, forn.nome, pro.descricao, pro.fornecedor FROM produtos as pro INNER JOIN fornecedores as forn ON pro.fornecedor = forn.id order by pro.nome asc";
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
            txtNomeProduto.Text = "";
            txtBuscarNome.Text = "";
            txtQuantidade.Text = "";
            txtEstoque.Text = "";

        }

        private void desabilitarCampos()
        {
            txtNomeProduto.Enabled = false;
            btnSalvar.Enabled = false;
            txtQuantidade.Enabled = false;
            txtEstoque.Enabled = false;
            CheckBox.Checked = false;
        }


        private void FrmEstoque_Load(object sender, EventArgs e)
        {
            try
            {
                Listar();
                limparCampos();
                desabilitarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar itens e carregar fornecedores, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            btnSalvar.Enabled = true;
            txtEstoque.Enabled = false;
            txtQuantidade.Enabled = true;
            txtNomeProduto.Enabled = false;

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNomeProduto.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtEstoque.Text = grid.CurrentRow.Cells[5].Value.ToString();
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BuscarNome();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox.Checked == true)
                { 
                    //CÓDIGO DO BOTÃO PARA EDITAR OS PRODUTOS
                    con.AbrirCon();
                    sql = "UPDATE produtos SET estoque = @estoque where id = @id";
                    cmd = new MySqlCommand(sql, con.con);

                    cmd.Parameters.AddWithValue("@estoque", Convert.ToDouble(txtEstoque.Text) + Convert.ToDouble(txtQuantidade.Text));
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    con.FecharCon();

                    //MessageBox.Show("Lançamento Feito com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limparCampos();
                    desabilitarCampos();
                    Listar();
                    MessageBox.Show("Lançamento nos gastos feito com sucesso!", "Caixa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //CÓDIGO DO BOTÃO PARA EDITAR OS PRODUTOS
                    con.AbrirCon();
                    sql = "UPDATE produtos SET estoque = @estoque where id = @id";
                    cmd = new MySqlCommand(sql, con.con);

                    cmd.Parameters.AddWithValue("@estoque", Convert.ToDouble(txtEstoque.Text) + Convert.ToDouble(txtQuantidade.Text));
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    con.FecharCon();

                    MessageBox.Show("Lançamento Feito com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limparCampos();
                    desabilitarCampos();
                    Listar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao adicionar estoque, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
        }
    }
}
