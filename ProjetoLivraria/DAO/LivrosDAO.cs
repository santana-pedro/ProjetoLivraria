using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class LivrosDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Livros> BuscarLivros(int? idLivro = null)
        {
            BindingList<Livros> loListLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                        ioQuery.Parameters.AddWithValue("@idLivro", idLivro);

                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(loReader.GetInt32(0), loReader.GetInt32(1), loReader.GetInt32(2), loReader.GetString(3), loReader.GetDouble(4), loReader.GetDouble(5), loReader.GetString(6), loReader.GetInt32(7));
                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao buscar o(s) livro(s).");
                }
            }
            return loListLivros;
        }
        public int InsereLivro(Livros aoNovoLivro)
        {
            if (aoNovoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO LIV_LIVROS(LIV_ID_LIVRO, LIV_ID_TIPO_LIVRO, LIV_ID_EDITOR, LIV_NM_TITULO, LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO) VALUES(@idLivro, @idTipoLivro, @idEditor, @tituloLivro, @precoLivro, @royaltyLivro, @resumoLivro, @edicaoLivro)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@tituloLivro", aoNovoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@precoLivro", aoNovoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivro", aoNovoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumoLivro", aoNovoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicaoLivro", aoNovoLivro.liv_nu_edicao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar cadastrar novo livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        public int RemoveLivro(Livros aoLivro)
        {
            if (aoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open(); ioQuery = new SqlCommand("DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.liv_id_livro));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar excluir livro.");
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int AtualizaLivro(Livros aoLivro)
        {
            if (aoLivro == null) throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open(); 
                    ioQuery = new SqlCommand("UPDATE LIV_LIVROS SET LIV_NM_TITULO = @tituloLivro, LIV_VL_PRECO = @precoLivro, LIV_PC_ROYALTY = @royaltyLivro, LIV_DS_RESUMO = @resumoLivro, LIV_NU_EDICAO = @edicaoLivro WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@tituloLivro", aoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@precoLivro", aoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivro", aoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumoLivro", aoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicaoLivro", aoLivro.liv_nu_edicao));
                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do livro.");
                }
            }
            return liQtdLinhasAtualizadas;
        }
    }

}