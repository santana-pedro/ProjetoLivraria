using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class TipoLivrosDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<TipoLivros> BuscarTipoLivro(decimal? idTipoLivro = null)
        {
            BindingList<TipoLivros> loListTipoLivros = new BindingList<TipoLivros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idTipoLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                        ioQuery.Parameters.AddWithValue("@idTipoLivro", idTipoLivro);

                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            TipoLivros loNovoTipoLivro = new TipoLivros(loReader.GetDecimal(0), loReader.GetString(1));
                            loListTipoLivros.Add(loNovoTipoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao buscar o(s) tipo livro(s).");
                }
            }
            return loListTipoLivros;
        }
        public int InsereTipoLivro(TipoLivros aoNovoTipoLivro)
        {
            if (aoNovoTipoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO TIL_TIPO_LIVROS(TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO) VALUES(@idTipoLivro, @descricaoTipoLivro)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoTipoLivro.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", aoNovoTipoLivro.til_ds_descricao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar cadastrar novo tipo livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        public int RemoveTipoLivro(TipoLivros aoTipoLivro)
        {
            if (aoTipoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open(); 
                    ioQuery = new SqlCommand("DELETE FROM TIL_TIPO_LIVROS WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoTipoLivro.til_id_tipo_livro));
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar excluir tipo livro.");
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int AtualizaTipoLivro(TipoLivros aoTipoLivro)
        {
            if (aoTipoLivro == null) throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE TIL_TIPO_LIVROS SET TIL_DS_DESCRICAO = @descricaoTipoLivro WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoTipoLivro.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", aoTipoLivro.til_ds_descricao));
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