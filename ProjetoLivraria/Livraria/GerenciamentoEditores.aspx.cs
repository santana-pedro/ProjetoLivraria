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
                Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsUrlEditor, lsEmailEditor);
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
        private void ShowMessage(string message)
        {
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }
        protected void gvGerenciamentoEditores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal editorId = Convert.ToDecimal(e.Keys["edi_id_editor"]);
                string nome = e.NewValues["edi_nm_nome"].ToString();
                string email = e.NewValues["edi_ds_email"].ToString();
                string url = e.NewValues["edi_ds_url"].ToString();

                if (string.IsNullOrEmpty(nome))
                {
                    ShowMessage("Informe o nome do editor");
                    return;
                }
                else if (string.IsNullOrEmpty(email))
                {
                    ShowMessage("Informe o email do editor");
                    return;
                }
                else if (string.IsNullOrEmpty(url))
                {
                    ShowMessage("Informe a url do editor");
                    return;
                }

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
            decimal editorId = Convert.ToDecimal(e.Keys["edi_id_editor"]);
            this.ioEditoresDAO.RemoveEditor(editorId);
            e.Cancel = true;
            this.CarregaDados();
        }
        protected void gvGerenciamentoEditores_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal editorId = Convert.ToDecimal(gvGerenciamentoEditores.GetRowValues(e.VisibleIndex, "edi_id_editor"));
            var editor = ioEditoresDAO.BuscarEditores(editorId).FirstOrDefault();
            if (e.ButtonID == "btnAutorInfo")
            {
            }
            else if (e.ButtonID == "btnLivros")
            {
                Session["SessionEditorSelecionado"] = editor;

                gvGerenciamentoEditores.JSProperties["cpRedirectionToLivros"] = true;
            }
        }
    }
}