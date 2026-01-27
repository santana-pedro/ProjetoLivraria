using DevExpress.Web.Data;
using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                string nome = e.NewValues["aut_nm_nome"].ToString();
                string sobrenome = e.NewValues["aut_nm_sobrenome"].ToString();
                string email = e.NewValues["aut_ds_email"].ToString();

                if (string.IsNullOrEmpty(nome))
                {
                    ShowMessage("Informe o nome o autor");
                    return;
                }
                else if (string.IsNullOrEmpty(sobrenome))
                {
                    ShowMessage("Informe o sobrenome o autor");
                    return;
                }
                else if (string.IsNullOrEmpty(email))
                {
                    ShowMessage("Informe o email o autor");
                    return;
                }

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
            BindingList<Autores> autorDelete = this.ioAutoresDAO.BuscarAutores(autorId);
            this.ioAutoresDAO.RemoveAutor(autorDelete.FirstOrDefault());
            e.Cancel = true;
            this.CarregaDados();
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