using DevExpress.Web;
using DevExpress.Web.Data;
using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoAutores : Page
    {
        private AutoresDAO ioAutoresDAO = new AutoresDAO();

        public BindingList<Autores> ListaAutores
        {
            get
            {
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null) this.CarregaDados();
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }

        public Autores AutorSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CarregaDados();
        }
        private void CarregaDados()
        {
            try
            {
                this.ListaAutores = this.ioAutoresDAO.BuscarAutores();
                this.gvGerenciamentoAutores.DataSource = this.ListaAutores;
                this.gvGerenciamentoAutores.DataBind();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
        }
        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;
                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.tbxCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.tbxCadastroEmailAutor.Text;
                Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                this.ioAutoresDAO.InsereAutor(loAutor);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
            this.tbxCadastroNomeAutor.Text = String.Empty;
            this.tbxCadastroSobrenomeAutor.Text = String.Empty;
            this.tbxCadastroEmailAutor.Text = String.Empty;
        }
        private void ShowMessage(string message)
        {
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }
        protected void gvGerenciamentoAutores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal autorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
                string nome, sobrenome, email;

                AutoresDAO autoresDAO = new AutoresDAO();
                Autores autorExistente = autoresDAO.BuscarAutores(autorId).FirstOrDefault();

                if(e.NewValues["aut_nm_nome"] != null) nome = e.NewValues["aut_nm_nome"].ToString();
                else nome = autorExistente.aut_nm_nome;
                if(e.NewValues["aut_nm_sobrenome"] != null) sobrenome = e.NewValues["aut_nm_sobrenome"].ToString();
                else sobrenome = autorExistente.aut_nm_sobrenome;
                if(e.NewValues["aut_ds_email"] != null) email = e.NewValues["aut_ds_email"].ToString();
                else email = autorExistente.aut_ds_email;


                Autores autor = new Autores(autorId, nome, sobrenome, email);

                this.ioAutoresDAO.AtualizaAutor(autor);

                e.Cancel = true;
                this.gvGerenciamentoAutores.CancelEdit();

                this.CarregaDados();
            }
            catch
            {
                ShowMessage("Erro na atualização do cadastro do autor.");
            }
        }
        protected void gvGerenciamentoAutores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            decimal autorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
            LivroAutorDAO livroAutorDAO = new LivroAutorDAO();
            LivroAutor livroAutor = livroAutorDAO.BuscarLivroAutor(autorId).FirstOrDefault();

            if (livroAutor != null)
            {
                ASPxGridView grid = (ASPxGridView)sender;
                grid.JSProperties["cpMensagemErro"] = "Não é possível excluir o autor, pois está associado a um ou mais livros.";
                e.Cancel = true;
                this.CarregaDados();
            }
            else { 
                this.ioAutoresDAO.RemoveAutor(autorId);
                e.Cancel = true;
                this.CarregaDados();
            }
        }
        protected void gvGerenciamentoAutores_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal autorId = Convert.ToDecimal(gvGerenciamentoAutores.GetRowValues(e.VisibleIndex, "aut_id_autor"));
            var autor = ioAutoresDAO.BuscarAutores(autorId).FirstOrDefault();
            if (e.ButtonID == "btnAutorInfo")
            {
            }
            else if (e.ButtonID == "btnLivros")
            {
                Session["SessionAutorSelecionado"] = autor;

                gvGerenciamentoAutores.JSProperties["cpRedirectionToLivros"] = true;
            }
        }
    }
}