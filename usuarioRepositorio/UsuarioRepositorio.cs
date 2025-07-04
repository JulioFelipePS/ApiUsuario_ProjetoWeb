using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain.repositorio;
using domain.entidades;
using Npgsql;
namespace usuarioRepositorio
{
    public class UsuarioRepositorio : InterfaceUserRepo
    {
        private string strConexao;
        public UsuarioRepositorio(string strConexao)
        {
            this.strConexao = strConexao;
        }

        public void Alterar(User cliente)
        {
            using (var conexao = new NpgsqlConnection(strConexao))
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = conexao;
                        cmd.Transaction = transacao;
                        cmd.CommandText = @"
                                          UPDATE public.users
                                            SET nomecompleto = @nomecompleto,
                                                email = @email,
                                                senha = @senha,
                                                datanascimento = @datanascimento,
                                                username = @username,
                                                telefone = @telefone,
                                                genero = @genero,
                                                descricao = @descricao,
                                                foto = @foto
                                            WHERE id = @id";
                        cmd.Parameters.AddWithValue("nomecompleto", cliente.NomeCompleto);
                        cmd.Parameters.AddWithValue("email", cliente.Email);
                        cmd.Parameters.AddWithValue("senha", cliente.Senha);
                        cmd.Parameters.AddWithValue("datanascimento", cliente.DataNascimento);
                        cmd.Parameters.AddWithValue("username", cliente.Username);
                        cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
                        cmd.Parameters.AddWithValue("genero", cliente.Genero);
                        cmd.Parameters.AddWithValue("descricao", cliente.Descricao);
                        cmd.Parameters.AddWithValue("foto", cliente.Foto);
                        cmd.ExecuteNonQuery();

                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public void Excluir(Guid id)
        {
            using (var conexao = new NpgsqlConnection(strConexao))
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = conexao;
                        cmd.Transaction = transacao;
                        cmd.CommandText = " DELETE FROM public.users " +
                                          " Where id = @id";
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                        transacao.Commit();
                        
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public void Inserir(User cliente)
        {
            using (var conexao = new NpgsqlConnection(strConexao))
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new NpgsqlCommand())
                        {
                            cmd.Connection = conexao;
                            cmd.Transaction = transacao;
                            cmd.CommandText = @"
                                                INSERT INTO public.users
                                                (nomecompleto, email, senha, datanascimento, username, telefone, genero, descricao, foto)
                                                VALUES
                                                (@nomecompleto, @email, @senha, @datanascimento, @username, @telefone, @genero, @descricao, @foto)";

                            cmd.Parameters.AddWithValue("nomecompleto", cliente.NomeCompleto);
                            cmd.Parameters.AddWithValue("email", cliente.Email);
                            cmd.Parameters.AddWithValue("senha", cliente.Senha);
                            cmd.Parameters.AddWithValue("datanascimento", cliente.DataNascimento);
                            cmd.Parameters.AddWithValue("username", cliente.Username);
                            cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
                            cmd.Parameters.AddWithValue("genero", cliente.Genero);
                            cmd.Parameters.AddWithValue("descricao", cliente.Descricao);
                            cmd.Parameters.AddWithValue("foto", cliente.Foto);

                            cmd.ExecuteNonQuery();
                            transacao.Commit();
                        }
                    }
                    catch (Exception)
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

        public User Procurar(string username)
        {
            User cliente = null;
            try
            {
                using (var con = new NpgsqlConnection(strConexao))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM users WHERE username = @username";
                    cmd.Parameters.AddWithValue("username", username);
                    var leitor = cmd.ExecuteReader();
                    while (leitor.Read())
                    {
                        cliente = new User()
                        {
                            Id = Guid.Parse(leitor["id"].ToString()),
                            NomeCompleto = leitor["nomecompleto"].ToString(),
                            Email = leitor["email"].ToString(),
                            Senha = leitor["senha"].ToString(),
                            DataNascimento = Convert.ToDateTime(leitor["datanascimento"]),
                            Username = leitor["username"].ToString(),
                            Telefone = leitor["telefone"].ToString(),
                            Genero = leitor["genero"].ToString(),
                            Descricao = leitor["descricao"].ToString(),
                            Foto = leitor["foto"].ToString(),
                        };
                    }
                    leitor.Close();

                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> ProcurarTodos()
        {
            List<User> clientes = new List<User>();

            try
            {
                using (var con = new NpgsqlConnection(strConexao))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM users";
                    var leitor = cmd.ExecuteReader();
                    while (leitor.Read())
                    {
                        var cliente = new User()
                        {
                            Id = Guid.Parse(leitor["id"].ToString()),
                            NomeCompleto = leitor["nomecompleto"].ToString(),
                            Email = leitor["email"].ToString(),
                            Senha = leitor["senha"].ToString(),
                            DataNascimento = Convert.ToDateTime(leitor["datanascimento"]),
                            Username = leitor["username"].ToString(),
                            Telefone = leitor["telefone"].ToString(),
                            Genero = leitor["genero"].ToString(),
                            Descricao = leitor["descricao"].ToString(),
                            Foto = leitor["foto"].ToString(),
                        };
                        clientes.Add(cliente);
                    }
                    leitor.Close();

                }
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
