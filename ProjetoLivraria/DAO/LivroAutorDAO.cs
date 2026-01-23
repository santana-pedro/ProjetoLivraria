using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class LivroAutorDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<LivroAutor> BuscarLivros(int? idLivro = null)
        {
            BindingList<LivroAutor> loListLivroAutor = new BindingList<LivroAutor>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR WHERE LIA_ID_LIVRO = @idLivro", ioConexao);
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
                            LivroAutor loNovoLivroAutor = new LivroAutor(loReader.GetInt32(0), loReader.GetInt32(1), loReader.GetDouble(2));
                            loListLivroAutor.Add(loNovoLivroAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao buscar o(s) livro(s) em livro autor.");
                }
            }
            return loListLivroAutor;
        }
        public BindingList<LivroAutor> BuscarAutores(int? idAutor = null)
        {
            BindingList<LivroAutor> loListLivroAutor = new BindingList<LivroAutor>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idAutor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR WHERE LIA_ID_AUTOR = @idAutor", ioConexao);
                        ioQuery.Parameters.AddWithValue("@idAutor", idAutor);

                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            LivroAutor loNovoLivroAutor = new LivroAutor(loReader.GetInt32(0), loReader.GetInt32(1), loReader.GetDouble(2));
                            loListLivroAutor.Add(loNovoLivroAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao buscar o(s) autor(s) em livro autor.");
                }
            }
            return loListLivroAutor;
        }
        public int InsereLivroAutor(LivroAutor aoNovoLivroAutor)
        {
            if (aoNovoLivroAutor == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO LIA_LIVRO_AUTOR(LIA_ID_AUTOR, LIA_ID_LIVRO, LIA_PC_ROYALTY) VALUES(@idAutor, @idLivro, @royaltyLivro)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivroAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivro", aoNovoLivroAutor.lia_pc_royalty));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar cadastrar novo livro autor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        public int RemoveLivroAutor(LivroAutor aoLivroAutor)
        {
            if (aoLivroAutor == null) throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open(); ioQuery = new SqlCommand("DELETE FROM LIA_LIVRO_AUTOR WHERE LIA_ID_AUTOR = @idAutor AND LIA_ID_LIVRO = @idLivro ", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivroAutor.lia_id_livro));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar excluir livro autor.");
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int AtualizaLivroAutor(LivroAutor aoLivroAutor)
        {
            if (aoLivroAutor == null) throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIA_LIVRO_AUTOR SET LIA_PC_ROYALTY = @royaltyLivroAutor WHERE LIA_ID_AUTOR = @idAutor AND LIA_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivroAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivroAutor", aoLivroAutor.lia_pc_royalty));
                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do livro autor.");
                }
            }
            return liQtdLinhasAtualizadas;
        }
    }

}