using DevExpress.Web;
using DevExpress.Web.Data;
using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoLivros : System.Web.UI.Page
    {
        private LivrosDAO ioLivrosDAO = new LivrosDAO();

        public BindingList<Livros> ListaLivros
        {
            get
            {
                if ((BindingList<Livros>)ViewState["ViewStateListaLivros"] == null) this.CarregaDados();
                return (BindingList<Livros>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }
        private void CarregaDados()
        {
            try
            {
                this.ListaLivros = this.ioLivrosDAO.BuscarLivros();
                this.gvGerenciamentoLivros.DataSource = this.ListaLivros;
                this.gvGerenciamentoLivros.DataBind();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
        }

        private void PreencherComboBoxes()
        {
            cmbCadastroTipoLivro.DataSource = new TipoLivrosDAO().BuscarTipoLivro();
            cmbCadastroTipoLivro.DataBind();

            cmbCadastroIdEditorLivro.DataSource = new EditoresDAO().BuscarEditores();
            cmbCadastroIdEditorLivro.DataBind();

            cmbCadastroAutor.DataSource = new AutoresDAO().BuscarAutores();
            cmbCadastroAutor.DataBind();
        }
        private void BindGridColumns()
        {
            var colTipo = gvGerenciamentoLivros.Columns["liv_id_tipo_livro"] as GridViewDataComboBoxColumn;
            if (colTipo != null)
            {
                colTipo.PropertiesComboBox.DataSource = new TipoLivrosDAO().BuscarTipoLivro();
            }

            var colEditor = gvGerenciamentoLivros.Columns["liv_id_editor"] as GridViewDataComboBoxColumn;
            if (colEditor != null)
            {
                colEditor.PropertiesComboBox.DataSource = new EditoresDAO().BuscarEditores();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BindGridColumns();
            this.CarregaDados();
            this.PreencherComboBoxes();
        }
        protected void BtnNovoLivro_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(l => l.liv_id_livro).First().liv_id_livro + 1;
                decimal ldcIdTipoLivro = Convert.ToDecimal(this.cmbCadastroTipoLivro.Value);
                decimal ldcIdEditor = Convert.ToDecimal(this.cmbCadastroIdEditorLivro.Value);
                string lcsTitulo = this.tbxCadastroTituloLivro.Text;
                double ldcPreco = Convert.ToDouble(this.tbxCadastroPrecoLivro.Text);
                double ldcRoyalty = Convert.ToDouble(this.tbxCadastroRoyaltyLivro.Text);
                string lcsResumo = this.tbxCadastroResumoLivro.Text;
                int lniEdicao = Convert.ToInt32(this.tbxCadastroEdicaoLivro.Text);
                Livros loLivro = new Livros(ldcIdLivro, ldcIdTipoLivro, ldcIdEditor, lcsTitulo, ldcPreco, ldcRoyalty, lcsResumo, lniEdicao);

                decimal ldcIdAutor = Convert.ToDecimal(this.cmbCadastroAutor.Value);
                LivroAutor loLivroAutor = new LivroAutor(ldcIdAutor, ldcIdLivro, ldcRoyalty);
                LivroAutorDAO ioLivroAutorDAO = new LivroAutorDAO();

                this.ioLivrosDAO.InsereLivro(loLivro);
                ioLivroAutorDAO.InsereLivroAutor(loLivroAutor);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Livro cadastrado com sucesso!');</script>");

            }
            catch (SqlException sqlEx)
            {
                HttpContext.Current.Response.Write("<script>alert('Houve um erro ao tentar cadastrar  o livro!');</script>" + sqlEx.Message);
            }
            this.cmbCadastroTipoLivro.Value = String.Empty;
            this.cmbCadastroIdEditorLivro.Value = String.Empty;
            this.tbxCadastroTituloLivro.Text = String.Empty;
            this.tbxCadastroPrecoLivro.Text = String.Empty;
            this.tbxCadastroRoyaltyLivro.Text = String.Empty;
            this.tbxCadastroResumoLivro.Text = String.Empty;
            this.tbxCadastroEdicaoLivro.Text = String.Empty;
            this.cmbCadastroAutor.Value = String.Empty;
        }

        protected void gvGerenciamentoLivros_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal ldcIdLivro = Convert.ToDecimal(e.Keys["liv_id_livro"]);
                decimal ldcIdTipoLivro = Convert.ToDecimal(e.NewValues["liv_id_tipo_livro"]);
                decimal ldcIdEditor = Convert.ToDecimal(e.NewValues["liv_id_editor"]);
                string lcsTitulo = Convert.ToString(e.NewValues["liv_titulo"]);
                double ldcPreco = Convert.ToDouble(e.NewValues["liv_preco"]);
                double ldcRoyalty = Convert.ToDouble(e.NewValues["liv_royalty"]);
                string lcsResumo = Convert.ToString(e.NewValues["liv_resumo"]);
                int lniEdicao = Convert.ToInt32(e.NewValues["liv_edicao"]);
                Livros loLivro = new Livros(ldcIdLivro, ldcIdTipoLivro, ldcIdEditor, lcsTitulo, ldcPreco, ldcRoyalty, lcsResumo, lniEdicao);
                this.ioLivrosDAO.AtualizaLivro(loLivro);
                this.CarregaDados();
                e.Cancel = true;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
        }
        protected void gvGerenciamentoLivros_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal ldcIdLivro = Convert.ToDecimal(e.Keys["liv_id_livro"]);
                this.ioLivrosDAO.RemoveLivro(ldcIdLivro);
                this.CarregaDados();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir livro: " + ex.Message);
            }
        }
        protected void gvGerenciamentoLivros_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "liv_id_tipo_livro")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.DataSource = new TipoLivrosDAO().BuscarTipoLivro();
                combo.DataBind();
            }

            if (e.Column.FieldName == "liv_id_editor")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.DataSource = new EditoresDAO().BuscarEditores();
                combo.DataBind();
            }
        }
    }
}