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
            else
            {
                UsuarioModel userModel = new UsuarioModel();
                userModel.NomeCompleto = user.NomeCompleto;
                userModel.Email = user.Email;
                userModel.Senha = user.Senha;
                userModel.Username = user.Username;
                userModel.Telefone = user.Telefone;
                userModel.Genero = user.Genero;
                userModel.Descricao = user.Descricao;
                userModel.Foto = user.Foto;
                userModel.DataNascimento = user.DataNascimento;
                return userModel;
            }

        }

        public static UsuarioDTO ToDto(UsuarioModel user)
        {
            if (user == null)
                return null;
            else
            {
                UsuarioDTO userDto = new UsuarioDTO();
                userDto.NomeCompleto = user.NomeCompleto;
                userDto.Email = user.Email;
                userDto.Senha = user.Senha;
                userDto.Username = user.Username;
                userDto.Telefone = user.Telefone;
                userDto.Genero = user.Genero;
                userDto.Descricao = user.Descricao;
                userDto.Foto = user.Foto;
                userDto.DataNascimento = user.DataNascimento;

                return userDto;
            }
        }
    }
}
