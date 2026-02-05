using System;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Livros
    {
        public decimal liv_id_livro { get; set; }
        public decimal liv_id_tipo_livro { get; set; }
        public decimal liv_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public double liv_vl_preco { get; set; }
        public double liv_pc_royalty { get; set; }
        public string liv_ds_resumo { get; set; }
        public int liv_nu_edicao { get; set; }
        public string liv_im_capa { get; set; }
        public decimal liv_lia_livro_autor { get; set; }

        public Livros(decimal idLivro, decimal idTipoLivro, decimal idEditor, string tituloLivro, double precoLivro, double royaltyLivro, string resumoLivro, int edicaoLivro){
            this.liv_id_livro = idLivro;
            this.liv_id_tipo_livro = idTipoLivro;
            this.liv_id_editor = idEditor;
            this.liv_nm_titulo = tituloLivro;
            this.liv_vl_preco = precoLivro;
            this.liv_pc_royalty = royaltyLivro;
            this.liv_ds_resumo = resumoLivro;
            this.liv_nu_edicao = edicaoLivro;
            this.liv_im_capa = null;
            this.liv_lia_livro_autor = 0;
        }
        public Livros(decimal idLivro, decimal idTipoLivro, decimal idEditor, string tituloLivro, double precoLivro, double royaltyLivro, string resumoLivro, int edicaoLivro, string imCapa = null)
        {
            this.liv_id_livro = idLivro;
            this.liv_id_tipo_livro = idTipoLivro;
            this.liv_id_editor = idEditor;
            this.liv_nm_titulo = tituloLivro;
            this.liv_vl_preco = precoLivro;
            this.liv_pc_royalty = royaltyLivro;
            this.liv_ds_resumo = resumoLivro;
            this.liv_nu_edicao = edicaoLivro;
            this.liv_im_capa = imCapa;
            this.liv_lia_livro_autor = 0;
        }
        public Livros(decimal idLivro, decimal idTipoLivro, decimal idEditor, string tituloLivro, double precoLivro, double royaltyLivro, string resumoLivro, int edicaoLivro, decimal liv_lia_livro_autor, string imCapa = null)
        {
            this.liv_id_livro = idLivro;
            this.liv_id_tipo_livro = idTipoLivro;
            this.liv_id_editor = idEditor;
            this.liv_nm_titulo = tituloLivro;
            this.liv_vl_preco = precoLivro;
            this.liv_pc_royalty = royaltyLivro;
            this.liv_ds_resumo = resumoLivro;
            this.liv_nu_edicao = edicaoLivro;
            this.liv_im_capa = imCapa;
            this.liv_lia_livro_autor = liv_lia_livro_autor;
        }
    }
}