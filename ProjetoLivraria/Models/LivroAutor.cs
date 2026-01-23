using System;
using System.Web.UI.HtmlControls;

namespace ProjetoLivraria.Models
{
    public class LivroAutor
    {
        public int lia_id_autor { get; set; }
        public int lia_id_livro { get; set; }
        public double lia_pc_royalty { get; set; }

        public LivroAutor(int idAutor, int idLivro, double pcRoyalty){
            this.lia_id_livro = idLivro;
            this.lia_id_autor = idAutor;
            this.lia_pc_royalty = pcRoyalty;
        }
    }

}