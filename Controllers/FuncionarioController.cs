using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Zeze.Model;
using WebApi_Zeze.Repositorio;

namespace WebApi_Zeze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class FuncionarioController : Controller
    {
        private readonly FuncionarioRepositorio _funcionarioRepo;

        public FuncionarioController(FuncionarioRepositorio funcionarioRepo)
        {
            _funcionarioRepo = funcionarioRepo;
        }
        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Funcionario> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var funcionario = _funcionarioRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (funcionario == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var funcionarioComUrl = new Funcionario
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone = funcionario.Telefone,
                Cargo = funcionario.Cargo,
            };

            // Retorna o funcionário com status 200 OK
            return Ok(funcionarioComUrl);
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Funcionario>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var funcionarios = _funcionarioRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (funcionarios == null || !funcionarios.Any())
            {
                return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = funcionarios.Select(funcionario => new Funcionario
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone= funcionario.Telefone,
                Cargo = funcionario.Cargo,
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var funcionario = new Funcionario
            {
                Nome = novoFuncionario.Nome,
                Email = novoFuncionario.Email,
                Telefone = novoFuncionario.Telefone,
                Cargo = novoFuncionario.Cargo,
            };

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone = funcionario.Telefone,
                Cargo = funcionario.Cargo,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] FuncionarioDto funcionarioAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var funcionarioExistente = _funcionarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (funcionarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            funcionarioExistente.Nome = funcionarioAtualizado.Nome;
            funcionarioExistente.Email = funcionarioAtualizado.Email;
            funcionarioExistente.Telefone = funcionarioAtualizado.Telefone;
            funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;

            // Cria a URL da foto
            var urlFoto = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{funcionarioExistente.Id}/foto";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = funcionarioExistente.Nome,
                Telefone = funcionarioExistente.Telefone,
                Email = funcionarioExistente.Email,
                Cargo = funcionarioExistente.Cargo,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var funcionarioExistente = _funcionarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (funcionarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _funcionarioRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = funcionarioExistente.Nome,
                Email = funcionarioExistente.Email,
                Telefone = funcionarioExistente.Telefone,
                Cargo = funcionarioExistente.Cargo,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

    }
}
