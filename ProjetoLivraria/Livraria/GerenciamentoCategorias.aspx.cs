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

	public partial class GerenciamentoCategorias : System.Web.UI.Page
	{
        private TipoLivrosDAO ioTipoLivrosDAO = new TipoLivrosDAO();

        public BindingList<TipoLivros> ListaTipoLivros
        {
            get
            {
                if ((BindingList<TipoLivros>)ViewState["ViewStateListaTipoLivros"] == null) this.CarregaDados();
                return (BindingList<TipoLivros>)ViewState["ViewStateListaTipoLivros"];
            }
            set
            {
                ViewState["ViewStateListaTipoLivros"] = value;
            }
        }

        public TipoLivros TipoLivroSessao
        {
            get { return (TipoLivros)Session["SessionTipoLivroSelecionado"]; }
            set { Session["SessionTipoLivroSelecionado"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CarregaDados();
        }
        private void CarregaDados()
        {
            try
            {
                this.ListaTipoLivros = this.ioTipoLivrosDAO.BuscarTipoLivro();
                this.gvGerenciamentoCategorias.DataSource = this.ListaTipoLivros;
                this.gvGerenciamentoCategorias.DataBind();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
        }
        protected void BtnNovaCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdTipoLivro = this.ListaTipoLivros.OrderByDescending(t => t.til_id_tipo_livro).First().til_id_tipo_livro + 1;
                string lsDescricaoTipoLivro = this.tbxCadastroDescricaoCategoria.Text;
                TipoLivros loTipoLivro = new TipoLivros(ldcIdTipoLivro, lsDescricaoTipoLivro);
                this.ioTipoLivrosDAO.InsereTipoLivro(loTipoLivro);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Categoria cadastrada com sucesso!');</script>");
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
            }
            this.tbxCadastroDescricaoCategoria.Text = String.Empty;
        }
        private void ShowMessage(string message)
        {
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }
        protected void gvGerenciamentoCategorias_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal tipoLivroId = Convert.ToDecimal(e.Keys["til_id_tipo_livro"]);
                string descricao;

                TipoLivrosDAO tipoLivrosDAO = new TipoLivrosDAO();
                TipoLivros tipoLivroExistente = tipoLivrosDAO.BuscarTipoLivro(tipoLivroId).FirstOrDefault();
                
                if (e.NewValues["til_ds_descricao"] != null) descricao = e.NewValues["til_ds_descricao"].ToString();
                else descricao = tipoLivroExistente.til_ds_descricao;

                TipoLivros tipoLivros = new TipoLivros(tipoLivroId, descricao);

                this.ioTipoLivrosDAO.AtualizaTipoLivro(tipoLivros);

                e.Cancel = true;
                this.gvGerenciamentoCategorias.CancelEdit();

                this.CarregaDados();
            }
            catch
            {
                ShowMessage("Erro na atualização da categoria.");
            }
        }
        protected void gvGerenciamentoCategorias_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            decimal tipoLivroId = Convert.ToDecimal(e.Keys["til_id_tipo_livro"]);
            Livros livrosTipoLivros = this.ioTipoLivrosDAO.BuscarLivrosDeTipo(tipoLivroId).FirstOrDefault();

            if(livrosTipoLivros != null)
            {
                ASPxGridView grid = (ASPxGridView)sender;
                grid.JSProperties["cpMensagemErro"] = "Não é possível excluir a categoria, pois está associada a um ou mais livros.";
                e.Cancel = true;
                this.CarregaDados();
            }
            else { 
                this.ioTipoLivrosDAO.RemoveTipoLivro(tipoLivroId);
                e.Cancel = true;
                this.CarregaDados();
            }
        }
        protected void gvGerenciamentoCategorias_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal tipoLivroId = Convert.ToDecimal(gvGerenciamentoCategorias.GetRowValues(e.VisibleIndex, "til_id_tipo_livro"));
            var tipoLivro = ioTipoLivrosDAO.BuscarTipoLivro(tipoLivroId).FirstOrDefault();
            if (e.ButtonID == "btnLivros")
            {
                Session["SessionTipoLivroSelecionado"] = tipoLivro;

                gvGerenciamentoCategorias.JSProperties["cpRedirectionToLivros"] = true;
            }
        }
    }
}