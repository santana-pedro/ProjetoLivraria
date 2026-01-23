using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class AutoresDAO
    {
        SqlCommand ioQuery;

        public BindingList<Autores> BuscarAutores(int? idAutor = null)
        {
            BindingList<Autores> loListAutores = new BindingList<Autores>();
            using (SqlConnection ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idAutor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                        ioQuery.Parameters.AddWithValue("@idAutor", idAutor);

                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Autores loNovoAutor = new Autores(loReader.GetInt32(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            loListAutores.Add(loNovoAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao buscar o(s) autor(s).");
                }
            }
            return loListAutores;
        }
        public int InsereAutor(Autores aoNovoAutor)
        { 
            if (aoNovoAutor == null) throw new NullReferenceException(); 
            int liQtdRegistrosInseridos = 0; 
            using (SqlConnection ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)) 
            { 
                try { 
                    ioConexao.Open(); 
                    ioQuery = new SqlCommand("INSERT INTO AUT_AUTORES(AUT_ID_AUTOR, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) VALUES(@idAutor, @nomeAutor, @sobrenomeAutor, @emailAutor)", ioConexao); 
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoAutor.aut_id_autor)); 
                    ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", aoNovoAutor.aut_nm_nome)); 
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoNovoAutor.aut_nm_sobrenome)); 
                    ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoNovoAutor.aut_ds_email));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery(); 
                } catch (Exception ex) { 
                    throw new Exception("Erro ao tentar cadastrar novo autor."); 
                } 
            } 
            return liQtdRegistrosInseridos; 
        }
        public int RemoveAutor(Autores aoAutor) { 
            if (aoAutor == null) throw new NullReferenceException(); 
            int liQtdRegistrosExcluidos = 0; 
            using (SqlConnection ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)) { 
                try { 
                    ioConexao.Open(); 
                    ioQuery = new SqlCommand("DELETE FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao); 
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoAutor.aut_id_autor)); 
                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery(); 
                } catch { 
                    throw new Exception("Erro ao tentar excluir autor."); 
                } 
            } 
            return liQtdRegistrosExcluidos; 
        }
        public int AtualizaAutor(Autores aoAutor) { 
            if (aoAutor == null) throw new NullReferenceException(); 
            int liQtdLinhasAtualizadas = 0; 
            using (SqlConnection ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)) { 
                try { 
                    ioConexao.Open(); 
                    ioQuery = new SqlCommand("UPDATE AUT_AUTORES SET AUT_NM_NOME = @nomeAutor, AUT_NM_SOBRENOME = @sobrenomeAutor, AUT_DS_EMAIL = @emailAutor WHERE AUT_ID_AUTOR = @idAutor", ioConexao); 
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoAutor.aut_id_autor)); 
                    ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", aoAutor.aut_nm_nome)); 
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoAutor.aut_nm_sobrenome)); 
                    ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoAutor.aut_ds_email)); liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery(); 
                } catch { 
                    throw new Exception("Erro ao tentar atualizar informações do autor."); 
                } 
            } 
            return liQtdLinhasAtualizadas; 
        }
        public void ImprimirFuncionamento()
        {
            Console.WriteLine("AutoresDAO funcionando!");
        }
    }
}