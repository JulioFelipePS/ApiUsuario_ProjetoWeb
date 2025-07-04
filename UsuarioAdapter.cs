using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplication.Adapter
{
    public static class UsuarioAdapter
    {
        public static UsuarioExternalServiceModel ToExternalService(UsuarioDTO usuario)
        {
            if (usuario == null)
                return null;
            else
            {
                UsuarioExternalServiceModel usuarioExternalService = new UsuarioExternalServiceModel();
                usuarioExternalService.Email = usuario.Email;
                usuarioExternalService.Nome = usuario.Nome;
                usuarioExternalService.Id = usuario.Id;

                return usuarioExternalService;
            }
        }

        public static UsuarioDTO ToDTO(UsuarioExternalServiceModel usuario)
        {
            if (usuario == null)
                return null;
            else
            {
                UsuarioDTO usuarioDTO = new UsuarioDTO();
                usuarioDTO.Email = usuario.Email;
                usuarioDTO.Nome = usuario.Nome;
                usuarioDTO.Id = usuario.Id;

                return usuarioDTO;
            }
        }
    }
}
