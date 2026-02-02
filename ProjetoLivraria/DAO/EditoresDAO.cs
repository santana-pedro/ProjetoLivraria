using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class EditoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Editores> BuscarEditores(decimal? idEditor = null)
        {
            BindingList<Editores> loListEditores = new BindingList<Editores>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idEditor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                        ioQuery.Parameters.AddWithValue("@idEditor", idEditor);

                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Editores loNovoEditor = new Editores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            loListEditores.Add(loNovoEditor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao buscar o(s) autor(s).");
                }
            }
            return loListEditores;
        }
        public BindingList<Livros> BuscarLivrosDeEditor(decimal idEditor)
        {
            BindingList<Livros> loListLivrosDeAutor = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.AddWithValue("@idEditor", idEditor);

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(Convert.ToDecimal(loReader["LIV_ID_LIVRO"]), Convert.ToDecimal(loReader["LIV_ID_TIPO_LIVRO"]), Convert.ToDecimal(loReader["LIV_ID_EDITOR"]), loReader["LIV_NM_TITULO"]?.ToString() ?? "", Convert.ToDouble(loReader["LIV_VL_PRECO"]), Convert.ToDouble(loReader["LIV_PC_ROYALTY"]), loReader["LIV_DS_RESUMO"]?.ToString() ?? "", Convert.ToInt32(loReader["LIV_NU_EDICAO"]), null);
                            loListLivrosDeAutor.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
                }
            }
            return loListLivrosDeAutor;
        }
        public int InsereEditor(Editores aoNovoEditor)
        {
            if (aoNovoEditor == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO EDI_EDITORES(EDI_ID_EDITOR, EDI_NM_EDITOR, EDI_DS_EMAIL, EDI_DS_URL) VALUES(@idEditor, @nomeEditor, @emailEditor, @urlEditor)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoEditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoNovoEditor.edi_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoNovoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoNovoEditor.edi_ds_url));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar cadastrar novo editor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        public int RemoveEditor(decimal idAoEditor)
        {
            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open(); 
                    ioQuery = new SqlCommand("DELETE FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", idAoEditor));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int AtualizaEditor(Editores aoEditor)
        {
            if (aoEditor == null) throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open(); 
                    ioQuery = new SqlCommand("UPDATE EDI_EDITORES SET EDI_NM_EDITOR = @nomeEditor, EDI_DS_EMAIL = @emailEditor, EDI_DS_URL = @urlEditor WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoEditor.edi_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoEditor.edi_ds_url)); 
                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do editor.");
                }
            }
            return liQtdLinhasAtualizadas;
        }
    }

}