using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain.entidades;

namespace domain.repositorio
{
   public interface InterfaceUserRepo
    {
        void Inserir(User usuario);
        void Alterar(User usuario);
        void Excluir(Guid id);
        User Procurar(String username);
        List<User> ProcurarTodos();
    }
}
