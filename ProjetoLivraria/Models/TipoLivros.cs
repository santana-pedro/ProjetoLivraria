using System;

namespace ProjetoLivraria.Models
{
    public class TipoLivros
    {
        public decimal til_id_tipo_livro { get; set; }
        public string til_ds_descricao { get; set; }

        public TipoLivros(decimal idTipoLivro, string dsDescricao){
            this.til_id_tipo_livro = idTipoLivro;
            this.til_ds_descricao = dsDescricao;
        }
    }

}