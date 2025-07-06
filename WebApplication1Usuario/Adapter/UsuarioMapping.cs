using aplication.DTO;
using WebApplication1Usuario.Models;

namespace WebApplication1Usuario.Adapter
{
    public class UsuarioMapping
    {
        public static UsuarioModel ToModel(UsuarioDTO user)
        {
            if (user == null)
                return null;

            return new UsuarioModel
            {
                Id = user.Id, // se tiver Id no DTO
                NomeCompleto = user.NomeCompleto,
                Email = user.Email,
                Senha = user.Senha,
                Username = user.Username,
                Telefone = user.Telefone,
                Genero = user.Genero,
                Descricao = user.Descricao,
                Foto = user.Foto,
                DataNascimento = user.DataNascimento
            };
        }

        public static UsuarioDTO ToDto(UsuarioModel user)
        {
            if (user == null)
                return null;

            return new UsuarioDTO
            {
                Id = user.Id,
                NomeCompleto = user.NomeCompleto,
                Email = user.Email,
                Senha = user.Senha,
                Username = user.Username,
                Telefone = user.Telefone,
                Genero = user.Genero,
                Descricao = user.Descricao,
                Foto = user.Foto,
                DataNascimento = user.DataNascimento
            };
        }

        // ✅ Novo método: mapping do InserirDto para UsuarioModel
        public static UsuarioModel ToModel(InserirDto dto)
        {
            if (dto == null)
                return null;

            return new UsuarioModel
            {
                Id = Guid.NewGuid(), // gera um novo ID para inserção
                NomeCompleto = dto.NomeCompleto,
                Email = dto.Email,
                Senha = dto.Senha,
                Username = dto.Username,
                Telefone = dto.Telefone,
                Genero = dto.Genero,
                Descricao = dto.Descricao,
                Foto = dto.Foto,
                DataNascimento = dto.DataNascimento,
               
            };
        }
    }
}
