using System;
using System.Collections.Generic;
using aplication.Adapter;
using aplication.DTO;
using domain.repositorio;
using domain.entidades;
using usuarioRepositorio;
namespace aplication.Application
{
    public class UsuarioApplication
    {
        private readonly InterfaceUserRepo _usuarioRepository;

        public UsuarioApplication(InterfaceUserRepo usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Inserir(UsuarioDTO dto)
        {
            var user = UsuarioAdapter.ToEntity(dto);
            _usuarioRepository.Inserir(user);
        }

        public void Alterar(UsuarioDTO dto)
        {
            var user = UsuarioAdapter.ToEntity(dto);
            _usuarioRepository.Alterar(user);
        }

        public void Excluir(Guid id)
        {
            _usuarioRepository.Excluir(id);
        }

        public UsuarioDTO Procurar(string username)
        {
            var user = _usuarioRepository.Procurar(username);
            return UsuarioAdapter.ToDTO(user);
        }

        public List<UsuarioDTO> ProcurarTodos()
        {
            var usuarios = _usuarioRepository.ProcurarTodos();
            var usuariosDTO = new List<UsuarioDTO>();

            foreach (var user in usuarios)
            {
                usuariosDTO.Add(UsuarioAdapter.ToDTO(user));
            }

            return usuariosDTO;
        }

        public bool Autenticar(string user, string password)
        {
             
            User usu = _usuarioRepository.Procurar(user); 
            if (!usu.PasswordIsValid(password))
            {
                throw new Exception("Usuário ou senha inválidos");
            }
            return true;
        }
    }
}
