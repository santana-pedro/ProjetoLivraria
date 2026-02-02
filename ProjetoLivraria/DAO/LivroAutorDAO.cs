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

        public BindingList<LivroAutor> BuscarLivroAutor(decimal? idAutor)
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
                            LivroAutor loNovoLivroAutor = new LivroAutor(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDouble(2));
                            loListLivroAutor.Add(loNovoLivroAutor);
                        }
                        loReader.Close();
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
                }
            }
            return loListLivroAutor;
        }

        public BindingList<Livros> BuscarLivrosDeAutor(decimal idAutor)
        {
            BindingList<Livros> loListLivrosDeAutor = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS AS L WHERE L.LIV_ID_LIVRO IN (SELECT LA.LIA_ID_LIVRO FROM LIA_LIVRO_AUTOR AS LA WHERE LA.LIA_ID_AUTOR = @idAutor)", ioConexao);
                    ioQuery.Parameters.AddWithValue("@idAutor", idAutor);

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

        public BindingList<Autores> BuscarAutoresDeLivro(decimal idLivro)
        {
            BindingList<Autores> loListAutoresDeLivro = new BindingList<Autores>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES AS A WHERE AUT_ID_AUTOR IN (SELECT LA.LIA_ID_AUTOR FROM LIA_LIVRO_AUTOR AS LA WHERE LA.LIA_ID_LIVRO = @idLivro)", ioConexao);
                    ioQuery.Parameters.AddWithValue("@idLivro", idLivro);

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Autores loNovoAutorDeLivro = new Autores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            loListAutoresDeLivro.Add(loNovoAutorDeLivro);
                        }
                        loReader.Close();
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
                }
            }
            return loListAutoresDeLivro;
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
                catch (SqlException sqlEx)
                {
                    throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
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
                catch (SqlException sqlEx)
                {
                    throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
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
                catch (SqlException sqlEx)
                {
                    throw new Exception("Erro no SQL (Código " + sqlEx.Number + "): " + sqlEx.Message);
                }
            }
            return liQtdLinhasAtualizadas;
        }
    }

}