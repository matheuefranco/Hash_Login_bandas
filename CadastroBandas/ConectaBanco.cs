using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
namespace CadastroBandas
{
    class ConectaBanco
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=compServer;database=banco_bandas");
        public string mensagem;
        //-------------------------------------------------
        public bool consultaUsuario(string user, string pass)
        {
            string senhaHash = Hash.makeHash(pass);
            MySqlCommand cmd = new MySqlCommand("consultaSenha", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("usuario", user);
            cmd.Parameters.AddWithValue("senha", senhaHash);
            try
            {
                conexao.Open();//abrindo a conexão;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();// tabela virtual
                da.Fill(ds); //passando os valores consultados para o DataSet 
                if (ds.Tables[0].Rows.Count > 0) // verifica se houve retorno
                    return true;
                else
                    return false;

            }
            catch (MySqlException er)
            {
                mensagem = "Erro" + er.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }
        //-------------------------------------------------
        public DataTable listaBandas()
        {
            MySqlCommand cmd = new MySqlCommand("listaBandas", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();// conectando com o banco
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dados = new DataTable();
                da.Fill(dados);
                return dados;
            }
            catch(MySqlException erro)
            {
                mensagem = "Erro MySQL:" + erro.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }

        }// fim lista
//------------------------------------------------------
    public bool insereBanda(Banda b)
     {
            MySqlCommand cmd = new MySqlCommand("insereBanda", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("nome", b.Nome);
            cmd.Parameters.AddWithValue("genero", b.Genero);
            cmd.Parameters.AddWithValue("integrantes", b.Integrantes);
            cmd.Parameters.AddWithValue("ranking", b.Ranking);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = "Erro Mysql " + erro.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }


        }// fim insereBanda

        //------------------------------------------------------
       public bool deletaBanda(int id)
        {
            MySqlCommand cmd = new MySqlCommand("deletaBanda", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idremove", id);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = "Erro Mysql " + erro.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }


        }// fim deletaBanda
        //---------------------------
        public bool alteraBanda(Banda b, int idAltera)
        {
            MySqlCommand cmd = new MySqlCommand("updateBanda", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("nome", b.Nome);
            cmd.Parameters.AddWithValue("genero", b.Genero);
            cmd.Parameters.AddWithValue("integrantes", b.Integrantes);
            cmd.Parameters.AddWithValue("ranking", b.Ranking);
            cmd.Parameters.AddWithValue("id", idAltera);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = "Erro Mysql " + erro.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }


        }// fim alteraBanda

        //------------------------------------------------------

    }
}
