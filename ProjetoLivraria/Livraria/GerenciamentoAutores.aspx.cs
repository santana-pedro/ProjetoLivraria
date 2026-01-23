using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoAutores : Page
    {
        AutoresDAO ioAutoresDAO = new AutoresDAO();
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
        protected void Page_Load(object sender, EventArgs e) { }
        private void CarregaDados()
        {
            this.ListaAutores = this.ioAutoresDAO.BuscarAutores();
        }
        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                int ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;
                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.tbxCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.tbxCadastroEmailAutor.Text;
                Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                this.ioAutoresDAO.InsereAutor(loAutor);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Autor.');</script>");
            }
            this.tbxCadastroNomeAutor.Text = String.Empty;
            this.tbxCadastroSobrenomeAutor.Text = String.Empty;
            this.tbxCadastroEmailAutor.Text = String.Empty;
        }
    }
}