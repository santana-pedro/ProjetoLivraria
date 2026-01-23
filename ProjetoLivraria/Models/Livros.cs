using System;

namespace ProjetoLivraria.Models
{
    public class Livros
    {
        public int liv_id_livro { get; set; }
        public int liv_id_tipo_livro { get; set; }
        public int liv_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public double liv_vl_preco { get; set; }
        public double liv_pc_royalty { get; set; }
        public string liv_ds_resumo { get; set; }
        public int liv_nu_edicao { get; set; }

        public Livros(int idLivro, int idTipoLivro, int idEditor, string tituloLivro, double precoLivro, double royaltyLivro, string resumoLivro, int edicaoLivro){
            this.liv_id_livro = idLivro;
            this.liv_id_tipo_livro = idTipoLivro;
            this.liv_id_editor = idEditor;
            this.liv_nm_titulo = tituloLivro;
            this.liv_vl_preco = precoLivro;
            this.liv_pc_royalty = royaltyLivro;
            this.liv_ds_resumo = resumoLivro;
            this.liv_nu_edicao = edicaoLivro;
        }
    }

}