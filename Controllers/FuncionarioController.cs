using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                // Chama o repositório para obter o funcionário pelo ID
                var funcionario = _funcionarioRepo.GetById(id);

                // Se o funcionário não for encontrado, retorna uma resposta 404
                if (funcionario == null)
                {
                    return NotFound(new { Mensagem = "Funcionário não encontrado." }); // Retorna 404 com mensagem
                }

                // Mapeia o funcionário encontrado
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
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar o funcionário.", Detalhes = ex.Message });
            }
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Funcionario>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todos os funcionários
                var funcionarios = _funcionarioRepo.GetAll();

                // Verifica se a lista de funcionários está vazia
                if (funcionarios == null || !funcionarios.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
                }

                // Mapeia a lista de funcionários
                var listaComUrl = funcionarios.Select(funcionario => new Funcionario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo,
                }).ToList();

                // Retorna a lista de funcionários com status 200 OK
                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar os funcionários.", Detalhes = ex.Message });
            }
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar o funcionário.", Detalhes = ex.Message });
            }
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] FuncionarioDto funcionarioAtualizado)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar o funcionário.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
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
                    Cargo = funcionarioExistente.Cargo
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção e retorna um erro 500 com os detalhes do erro
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir o funcionário.", Detalhes = ex.Message });
            }
        }

    }
}
