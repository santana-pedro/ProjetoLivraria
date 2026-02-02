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
                Autores loAutorFiltro = Session["SessionAutorSelecionado"] as Autores;
                Editores loEditorFiltro = Session["SessionEditorSelecionado"] as Editores;
                TipoLivros loTipoLivroFiltro = Session["SessionTipoLivroSelecionado"] as TipoLivros;

                LivroAutorDAO ioLivroAutorDAO = new LivroAutorDAO();
                EditoresDAO ioEditorDAO = new EditoresDAO();
                TipoLivrosDAO ioTipoLivroDAO = new TipoLivrosDAO();

                if (loAutorFiltro != null)
                {
                    this.ListaLivros = ioLivroAutorDAO.BuscarLivrosDeAutor(loAutorFiltro.aut_id_autor);
                    Session["SessionAutorSelecionado"] = null;
                }
                if(loEditorFiltro != null)
                {
                    this.ListaLivros = ioEditorDAO.BuscarLivrosDeEditor(loEditorFiltro.edi_id_editor);
                    Session["SessionEditorSelecionado"] = null;
                }
                if(loTipoLivroFiltro != null)
                {
                    this.ListaLivros = ioTipoLivroDAO.BuscarLivrosDeTipo(loTipoLivroFiltro.til_id_tipo_livro);
                    Session["SessionTipoLivroSelecionado"] = null;
                }
                if(loTipoLivroFiltro == null && loEditorFiltro == null && loAutorFiltro == null)
                {
                    this.ListaLivros = this.ioLivrosDAO.BuscarLivros();
                }
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
            this.CarregaDados();
            this.BindGridColumns();
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
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
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
                Livros loLivroExistente = this.ioLivrosDAO.BuscarLivros(ldcIdLivro).FirstOrDefault();
                decimal ldcIdTipoLivro, ldcIdEditor;
                string lcsTitulo, lcsResumo;
                double ldcPreco, ldcRoyalty;
                int lniEdicao;

                if (e.NewValues["liv_id_tipo_livro"] != null) ldcIdTipoLivro = Convert.ToDecimal(e.NewValues["liv_id_tipo_livro"]);
                else ldcIdTipoLivro = loLivroExistente.liv_id_tipo_livro;

                if (e.NewValues["liv_id_editor"] != null) ldcIdEditor = Convert.ToDecimal(e.NewValues["liv_id_editor"]);
                else ldcIdEditor = loLivroExistente.liv_id_editor;

                if (e.NewValues["liv_nm_titulo"] != null) lcsTitulo = Convert.ToString(e.NewValues["liv_nm_titulo"]);
                else lcsTitulo = loLivroExistente.liv_nm_titulo;

                if (e.NewValues["liv_vl_preco"] != null) ldcPreco = Convert.ToDouble(e.NewValues["liv_vl_preco"]);
                else ldcPreco = loLivroExistente.liv_vl_preco;

                if (e.NewValues["liv_pc_royalty"] != null) ldcRoyalty = Convert.ToDouble(e.NewValues["liv_pc_royalty"]);
                else ldcRoyalty = loLivroExistente.liv_pc_royalty;

                if (e.NewValues["liv_ds_resumo"] != null) lcsResumo = Convert.ToString(e.NewValues["liv_ds_resumo"]);
                else lcsResumo = loLivroExistente.liv_ds_resumo;

                if (e.NewValues["liv_nu_edicao"] != null) lniEdicao = Convert.ToInt32(e.NewValues["liv_nu_edicao"]);
                else lniEdicao = loLivroExistente.liv_nu_edicao;

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

                Livros livroAtual = this.ioLivrosDAO.BuscarLivros(ldcIdLivro).FirstOrDefault();
                LivroAutorDAO ioLivroAutorDAO = new LivroAutorDAO();
                LivroAutor loLivroAutor = ioLivroAutorDAO.BuscarLivroAutor(ldcIdLivro).FirstOrDefault();
                this.ioLivrosDAO.RemoveLivro(ldcIdLivro);
                this.CarregaDados();
                e.Cancel = true;
                    
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
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
        protected void gvGerenciamentoLivros_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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