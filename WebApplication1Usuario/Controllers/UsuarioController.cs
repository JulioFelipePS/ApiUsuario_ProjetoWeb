using Microsoft.AspNetCore.Mvc;
using domain.repositorio;
using WebApplication1Usuario.Models;
using aplication.DTO;
using aplication.Application;
using WebApplication1Usuario.Adapter;
using Microsoft.AspNetCore.Authorization;
using WebApplication1Usuario.Services;

namespace WebApplication1Usuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private string connectionString;
        private readonly InterfaceUserRepo _usuarioRepo;

        public UsuarioController(IConfiguration configuration, InterfaceUserRepo usuarioRepo)
        {
            connectionString = configuration.GetConnectionString("conexao")
       ?? throw new InvalidOperationException("Connection string 'conexao' não encontrada.");
            _usuarioRepo = usuarioRepo;
        }


        [HttpGet]
        public IEnumerable<UsuarioModel> Get()
        {
            List<UsuarioModel> userModel = new List<UsuarioModel>();
            UsuarioApplication app = new UsuarioApplication(_usuarioRepo);
            var usuarios = app.ProcurarTodos();
            foreach (var user in usuarios)
            {
                userModel.Add(UsuarioMapping.ToModel(user));
            }
            return userModel.ToArray();
        }

        [HttpGet("{Username}")]
        public UsuarioModel Get(String Username)
        {

            UsuarioApplication app = new UsuarioApplication(_usuarioRepo);
            var user = app.Procurar(Username);
            return UsuarioMapping.ToModel(user);
        }
        [HttpPost]
        public IActionResult Post(UsuarioModel user)
        {
            UsuarioApplication app = new UsuarioApplication(_usuarioRepo);
            app.Inserir(UsuarioMapping.ToDto(user));

            return CreatedAtAction(nameof(Get), new { username = user.Username }, null);
        }

        [HttpPut]
        public IActionResult Put(UsuarioModel user)
        {
            UsuarioApplication app = new UsuarioApplication(_usuarioRepo);
            app.Alterar(UsuarioMapping.ToDto(user));
            return NoContent(); // Retorna HTTP 204 (sem conteúdo), padrão para PUT
        }

        [HttpDelete("{id:Guid}")]
        public void Delete(Guid id)
        {
            UsuarioApplication app = new UsuarioApplication(_usuarioRepo);
            app.Excluir(id);
        }

        [HttpPost("autenticar")]
        [AllowAnonymous]
        public IActionResult Autenticar([FromBody] AutenticarDto usuario)
        {
            UsuarioApplication app = new UsuarioApplication(_usuarioRepo);

            try
            {
                if (app.Autenticar(usuario.Username, usuario.Senha))
                {
                    var user = app.Procurar(usuario.Username);

                    var userDto = app.Procurar(usuario.Username);
                    var userModel = UsuarioMapping.ToModel(userDto);

                    var token = TokenServices.GenerateToken(userModel);
                    return Ok(new { usuario.Username, Token = token });
                }
                else
                {
                    return BadRequest("Usuario ou senha inválidos");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

