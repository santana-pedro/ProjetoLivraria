using DevExpress.Web;
using DevExpress.Web.Data;
using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoEditores : System.Web.UI.Page
    {
        private EditoresDAO ioEditoresDAO = new EditoresDAO();

        public BindingList<Editores> ListaEditores
        {
            get
            {
                if ((BindingList<Editores>)ViewState["ViewStateListaEditores"] == null) this.CarregaDados();
                return (BindingList<Editores>)ViewState["ViewStateListaEditores"];
            }
            set
            {
                ViewState["ViewStateListaEditores"] = value;
            }
        }
        public Editores EditorSessao
        {
            get { return (Editores)Session["SessionEditorSelecionado"]; }
            set { Session["SessionEditorSelecionado"] = value; }
        }
        private void CarregaDados()
        {
            try
            {
                this.ListaEditores = this.ioEditoresDAO.BuscarEditores();
                this.gvGerenciamentoEditores.DataSource = this.ListaEditores;
                this.gvGerenciamentoEditores.DataBind();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CarregaDados();
        }
        protected void BtnNovoEditor_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdEditor = this.ListaEditores.OrderByDescending(ed => ed.edi_id_editor).First().edi_id_editor + 1;
                string lsNomeEditor = this.tbxCadastroNomeEditor.Text;
                string lsUrlEditor = this.tbxCadastroUrlEditor.Text;
                string lsEmailEditor = this.tbxCadastroEmailEditor.Text;
                Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);
                this.ioEditoresDAO.InsereEditor(loEditor);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Editor cadastrado com sucesso!');</script>");
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
            this.tbxCadastroNomeEditor.Text = String.Empty;
            this.tbxCadastroUrlEditor.Text = String.Empty;
            this.tbxCadastroEmailEditor.Text = String.Empty;
        }
        protected void gvGerenciamentoEditores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal editorId = Convert.ToDecimal(e.Keys["edi_id_editor"]);
                string nome, email, url;
                Editores editorExistente = this.ioEditoresDAO.BuscarEditores(editorId).FirstOrDefault();

                if (e.NewValues["edi_nm_nome"] != null) nome = e.NewValues["edi_nm_nome"].ToString();
                else nome = editorExistente.edi_nm_nome;
                if(e.NewValues["edi_ds_email"] != null) email = e.NewValues["edi_ds_email"].ToString();
                else email = editorExistente.edi_ds_email;
                if(e.NewValues["edi_ds_url"] != null) url = e.NewValues["edi_ds_url"].ToString();
                else url = editorExistente.edi_ds_url;

                Editores editor = new Editores(editorId, nome, email, url);

                this.ioEditoresDAO.AtualizaEditor(editor);

                e.Cancel = true;
                this.gvGerenciamentoEditores.CancelEdit();

                this.CarregaDados();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
        }
        protected void gvGerenciamentoEditores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal editorId = Convert.ToDecimal(e.Keys["edi_id_editor"]);
                Livros livrosEditores = this.ioEditoresDAO.BuscarLivrosDeEditor(editorId).FirstOrDefault();

                if (livrosEditores != null)
                {
                    ASPxGridView grid = (ASPxGridView)sender;
                    grid.JSProperties["cpMensagemErro"] = "Não é possível excluir o editor, pois está associado a um ou mais livros.";
                    e.Cancel = true;
                    this.CarregaDados();
                }
                else{ 
                    this.ioEditoresDAO.RemoveEditor(editorId);
                    e.Cancel = true;
                    this.CarregaDados();
                }
                
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
        }
        protected void gvGerenciamentoEditores_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal editorId = Convert.ToDecimal(gvGerenciamentoEditores.GetRowValues(e.VisibleIndex, "edi_id_editor"));
            var editor = ioEditoresDAO.BuscarEditores(editorId).FirstOrDefault();
            if (e.ButtonID == "btnLivros")
            {
                Session["SessionEditorSelecionado"] = editor;

                gvGerenciamentoEditores.JSProperties["cpRedirectionToLivros"] = true;
            }
        }
    }
}