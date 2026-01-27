using System;
using System.Web.UI.HtmlControls;

namespace ProjetoLivraria.Models
{
    public class LivroAutor
    {
        public decimal lia_id_autor { get; set; }
        public decimal lia_id_livro { get; set; }
        public double lia_pc_royalty { get; set; }

        public LivroAutor(decimal idAutor, decimal idLivro, double pcRoyalty){
            this.lia_id_livro = idLivro;
            this.lia_id_autor = idAutor;
            this.lia_pc_royalty = pcRoyalty;
        }
    }

}