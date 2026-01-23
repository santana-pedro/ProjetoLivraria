using System;

namespace ProjetoLivraria.Models
{
    public class Editores
    {
        public int edi_id_editor { get; set; }
        public string edi_nm_nome { get; set; }
        public string edi_ds_email { get; set; }
        public string edi_ds_url { get; set; }

        public Editores(int idEditor, string nomeEditor, string emailEditor, string urlEditor){
            this.edi_id_editor = idEditor;
            this.edi_nm_nome = nomeEditor;
            this.edi_ds_email = emailEditor;
            this.edi_ds_url = urlEditor;
        }
    }

}