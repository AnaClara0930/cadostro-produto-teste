using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsultasProduto
{
    //Cadastra o produto no banco de dados
    public static bool InserirProduto(string nome, string descricao, string fabricante, int quantidade)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiInserido = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Produto (nome, descricao, quantidade, fabricante) 
                VALUES (@nome,@descricao,@quantidade,@fabricante)";
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@descricao", descricao);
            comando.Parameters.AddWithValue("@quantidade", quantidade);
            comando.Parameters.AddWithValue("@fabricante", fabricante);
            var leitura = comando.ExecuteReader();
            foiInserido = true;
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if(conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiInserido;
    }

    // Excluir um produto do banco de dados - CREATE
    public static bool ExcluirProduto(int id)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiExcluido = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                DELETE FROM Produto WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            var leitura = comando.ExecuteReader();
            foiExcluido = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiExcluido;
    }

    // Alterar um produto do banco de dados - CREATE
    public static bool AlterarProduto(int id, string nome, string descricao, string fabricante, int quantidade)
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        bool foiAlterado = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                UPDATE Produto 
                SET nome = @nome, descricao = @descricao, fabricante = @fabricante, quantidade = @quantidade
                WHERE id = @id";
            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@descricao", descricao);
            comando.Parameters.AddWithValue("@quantidade", quantidade);
            comando.Parameters.AddWithValue("@fabricante", fabricante);
            var leitura = comando.ExecuteReader();
            foiAlterado = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiAlterado;
    }

    public static List<Produto> ObterTodosProdutos()
    {
        var conexao = new MySqlConnection(ConexaoBD.Connection.ConnectionString);
        List<Produto>ListaDeProduto = new List<Produto>();

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                SELECT * FROM Produto;";
            var leitura = comando.ExecuteReader();
            while (leitura.Read())
            {
                Produto produto = new Produto();
                produto.id = leitura.GetInt32("id");
                produto.descricao = leitura.GetString("descricao");
                produto.fabricante = leitura.GetString();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiAlterado;
    }
}
