using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aplication.DTO;
using domain.entidades;

namespace aplication.Adapter
{
    internal class UsuarioAdapter
    {
        public static UsuarioDTO ToDTO(User user)
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

        public static User ToEntity(UsuarioDTO dto)
        {
            if (dto == null)
                return null;

            return new User
            {
                Id = dto.Id,
                NomeCompleto = dto.NomeCompleto,
                Email = dto.Email,
                Senha = dto.Senha,
                Username = dto.Username,
                Telefone = dto.Telefone,
                Genero = dto.Genero,
                Descricao = dto.Descricao,
                Foto = dto.Foto,
                DataNascimento = dto.DataNascimento
            };
        }
    }
}

