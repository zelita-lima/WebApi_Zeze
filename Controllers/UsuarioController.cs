using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // Chama o repositório para obter o funcionário pelo ID
            var usuario = _usuarioRepo.GetById(id);

            // Se o usuario não for encontrado, retorna uma resposta 404
            if (usuario == null)
            {
                return NotFound(new { Mensagem = "Usuario não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var usuarioComUrl = new Usuario
            {
                Id = usuario.Id,
                Name = usuario.Name,
                Senha = usuario.Senha,

            };

            // Retorna o funcionário com status 200 OK
            return Ok(usuarioComUrl);
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Usuario>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var usuarios = _usuarioRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound(new { Mensagem = "Nenhum usuario encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = usuarios.Select(usuario => new Usuario
            {
                Id = usuario.Id,
                Name = usuario.Name,
                Senha = usuario.Senha,
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // POST api/<UsuarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] UsuarioDto novoUsuario)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var usuario = new Usuario
            {
                Name = novoUsuario.Name,
                Senha = novoUsuario.Senha
            };

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Name = usuario.Name,
                Senha = usuario.Senha
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<UsuarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] UsuarioDto usuarioAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var usuarioExistente = _usuarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (usuarioExistente == null)
            {
                return NotFound(new { Mensagem = "Usuario não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            usuarioExistente.Name = usuarioAtualizado.Name;
            usuarioExistente.Senha = usuarioAtualizado.Senha;

            // Cria a URL da foto
            var urlFoto = $"{Request.Scheme}://{Request.Host}/api/Usuario/{usuarioExistente.Id}/foto";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                id = usuarioExistente.Id,
                Name = usuarioExistente.Name,
                Senha = usuarioExistente.Senha,
        
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
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
