using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_Zeze.Model;
using WebApi_Zeze.Repositorio;


namespace WebApi_Zeze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepo;

        public UsuarioController(UsuarioRepositorio usuarioRepo)
        {
            this._usuarioRepo = usuarioRepo;
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetById(int id)
        {
            try
            {
                // Chama o repositório para obter o funcionário pelo ID
                var usuario = _usuarioRepo.GetById(id);

                // Se o usuário não for encontrado, retorna uma resposta 404
                if (usuario == null)
                {
                    return NotFound(new { Mensagem = "Usuário não encontrado." });
                }

                // Retorna o usuário com status 200 OK
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar o usuário.", Detalhes = ex.Message });
            }
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Usuario>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todos os funcionários
                var usuarios = _usuarioRepo.GetAll();

                // Verifica se a lista está vazia
                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum usuário encontrado." });
                }

                // Retorna a lista de usuários com status 200 OK
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar os usuários.", Detalhes = ex.Message });
            }
        }
        // POST api/Usuario
        [HttpPost]
        public ActionResult<object> Post([FromForm] UsuarioDto novoUsuario)
        {
            try
            {
                // Cria uma nova instância de usuário a partir do DTO recebido
                var usuario = new Usuario
                {
                    Name = novoUsuario.Name,
                    Senha = novoUsuario.Senha
                };

                // Retorna o objeto com status 200 OK
                return Ok(new
                {
                    Mensagem = "Usuário cadastrado com sucesso!",
                    Name = usuario.Name,
                    Senha = usuario.Senha
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar o usuário.", Detalhes = ex.Message });
            }
        }

        // PUT api/Usuario/{id}
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] UsuarioDto usuarioAtualizado)
        {
            try
            {
                // Busca o usuário existente pelo Id
                var usuarioExistente = _usuarioRepo.GetById(id);

                // Verifica se o usuário foi encontrado
                if (usuarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Usuário não encontrado." });
                }

                // Atualiza os dados do usuário existente
                usuarioExistente.Name = usuarioAtualizado.Name;
                usuarioExistente.Senha = usuarioAtualizado.Senha;

                // Retorna o objeto com status 200 OK
                return Ok(new
                {
                    Mensagem = "Usuário atualizado com sucesso!",
                    Id = usuarioExistente.Id,
                    Name = usuarioExistente.Name,
                    Senha = usuarioExistente.Senha
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar o usuário.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var usuarioExistente = _usuarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (usuarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _usuarioRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                id = usuarioExistente.Id,
                Name = usuarioExistente.Name,
                Senha = usuarioExistente.Senha,

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
