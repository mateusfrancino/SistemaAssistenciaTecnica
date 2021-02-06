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
    public partial class FrmCadastroProdutos : Form
    {
        ConexaoBD con = new ConexaoBD();
        string sql;
        MySqlCommand cmd;
        string id;
        public FrmCadastroProdutos()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
      
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void FrmCadastroProdutos_Load(object sender, EventArgs e)
        {
            try
            {
                Listar();
                CarregarCombobox();
                desabilitarCampos();
                txtBuscarNome.Size.Width.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar itens e carregar fornecedores, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }
            
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

        private void CarregarCombobox()
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
                CBFornecedor.DataSource = dt;
                CBFornecedor.ValueMember = "id";
                CBFornecedor.DisplayMember = "nome";

                con.FecharCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar fornecedores, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
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
                grid.Columns[5].HeaderText = "Estoque";
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
            txtValorCompra.Text = "";
            txtValorVenda.Text = "";
            txtDescProduto.Text = "";
            txtCodBarras.Text = "";
            txtBuscarNome.Text = "";
            txtEstoque.Text = "";

        }

        private void desabilitarCampos()
        {
            txtNomeProduto.Enabled = false;
            txtValorCompra.Enabled = false;
            txtValorVenda.Enabled = false;
            txtDescProduto.Enabled = false;
            txtCodBarras.Enabled = false;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = false;
            txtEstoque.Enabled = false;
            CBFornecedor.Enabled = false;
        }

        private void habilitarCampos()
        {
            txtNomeProduto.Enabled = true;
            txtValorCompra.Enabled = true;
            txtValorVenda.Enabled = true;
            txtDescProduto.Enabled = true;
            txtCodBarras.Enabled = true;
            btnExcluir.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = true;
            txtEstoque.Enabled = true;
            CBFornecedor.Enabled = true;
        }


        private void grid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNomeProduto.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtCodBarras.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtValorCompra.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtValorVenda.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtEstoque.Text = grid.CurrentRow.Cells[5].Value.ToString();
            CBFornecedor.Text = grid.CurrentRow.Cells[6].Value.ToString();
            txtDescProduto.Text = grid.CurrentRow.Cells[7].Value.ToString();
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
                if (txtNomeProduto.Text.ToString().Trim() == "")
                {
                    txtNomeProduto.Text = "";
                    MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNomeProduto.Focus();
                    return;
                }

                if (txtValorVenda.Text == "")
                {
                    MessageBox.Show("Preencha o Valor", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValorVenda.Focus();
                    return;
                }

                //CÓDIGO DO BOTÃO PARA EDITAR
                con.AbrirCon();
                sql = "UPDATE produtos SET nome = @nome, codigodebarras = @codigodebarras, valor_compra = @valor_compra, valor_venda = @valor_venda, estoque = @estoque, fornecedor = @fornecedor, descricao = @descricao where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNomeProduto.Text);
                cmd.Parameters.AddWithValue("@codigodebarras", txtCodBarras.Text);
                cmd.Parameters.AddWithValue("@valor_compra", txtValorCompra.Text.Replace(",", "."));
                cmd.Parameters.AddWithValue("@valor_venda", txtValorVenda.Text.Replace(",", "."));
                cmd.Parameters.AddWithValue("@estoque", txtEstoque.Text);
                cmd.Parameters.AddWithValue("@fornecedor", CBFornecedor.SelectedValue);
                cmd.Parameters.AddWithValue("@descricao", txtDescProduto.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Produto Editado com Sucesso!", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (txtNomeProduto.Text.ToString().Trim() == "")
                {
                    txtNomeProduto.Text = "";
                    MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNomeProduto.Focus();
                    return;
                }


                if (txtValorVenda.Text == "")
                {
                    MessageBox.Show("Preencha o Valor de Venda", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValorCompra.Focus();
                    return;
                }

                //CÓDIGO DO BOTÃO PARA SALVAR
                con.AbrirCon();
                sql = "INSERT INTO produtos (nome, codigodebarras, valor_compra, valor_venda, estoque, fornecedor, descricao) VALUES (@nome, @codigodebarras, @valor_compra, @valor_venda, @estoque, @fornecedor, @descricao)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNomeProduto.Text);
                cmd.Parameters.AddWithValue("@codigodebarras", txtCodBarras.Text);
                cmd.Parameters.AddWithValue("@valor_compra", txtValorCompra.Text.Replace(",", "."));
                cmd.Parameters.AddWithValue("@valor_venda", txtValorVenda.Text.Replace(",", "."));
                cmd.Parameters.AddWithValue("@estoque", txtEstoque.Text);
                cmd.Parameters.AddWithValue("@fornecedor", CBFornecedor.SelectedValue);
                cmd.Parameters.AddWithValue("@descricao", txtDescProduto.Text);

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Produto Salvo com Sucesso!", "Registros Salvos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
                txtBuscarNome.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar tabela, \n  pode ter ocorrido um erro na conexão com o Banco! " + ex.Message);
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            var resultado = MessageBox.Show("Deseja Realmente Excluir o Produto?", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (resultado == DialogResult.Yes)
                {
                    //CÓDIGO DO BOTÃO PARA EXCLUIR
                    con.AbrirCon();
                    sql = "DELETE FROM produtos where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharCon();

                    MessageBox.Show("Produto Excluido com Sucesso!", "Registro Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
