using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Zeze.Model;
using WebApi_Zeze.ORM;
using WebApi_Zeze.Repositorio;

namespace WebApi_Zeze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroRepositorio _livroRepo;

        public LivroController(LivroRepositorio livroRepo)
        {
            _livroRepo = livroRepo;
        }

        // GET: api/Livro/{id}
        [HttpGet("{id}")]
        public ActionResult<Livro> GetById(int id)
        {
            try
            {
                // Chama o repositório para obter o funcionário pelo ID
                var livro = _livroRepo.GetById(id);
                // Se o funcionário não for encontrado, retorna uma resposta 404
                if (livro == null)
                {
                    return NotFound(new { Mensagem = "Livro não encontrado." }); // Retorna 404 com mensagem
                }
                // Mapeia o funcionário encontrado para incluir a URL da foto
                var livroComUrl = new Livro
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    Disponibilidade = livro.Disponibilidade,
                    AnoPublcacao = livro.AnoPublcacao,
                    FkCategoria = livro.FkCategoria,
                };
                // Retorna o funcionário com status 200 OK
                return Ok(livroComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar o funcionário.", Detalhes = ex.Message });
            }
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Livro>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todos os funcionários
                var livros = _livroRepo.GetAll();

                // Verifica se a lista de funcionários está vazia
                if (livros == null || !livros.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum livro encontrado." });
                }

                // Mapeia a lista de livros
                var listaComUrl = livros.Select(livro => new Livro
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    Disponibilidade = livro.Disponibilidade,
                    AnoPublcacao = livro.AnoPublcacao,
                    FkCategoria = livro.FkCategoria,
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
        public ActionResult<object> Post([FromBody] FuncionarioDto novoLivro)
        {
            try
            {
                // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
                var livro = new Livro
                {
                    Titulo = novoLivro.Titulo,
                    Autor = novoLivro.Autor,
                    Disponibilidade = novoLivro.Disponibilidade,
                    AnoPublcacao = novoLivro.AnoPublcacao,
                    FkCategoria = novoLivro.FkCategoria,
                };

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Livro cadastrado com sucesso!",
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    AnoPublcaco = livro.AnoPublcacao,
                    Disponibilidade = livro.Disponibilidade,
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
        public ActionResult<object> Put(int id, [FromBody] FuncionarioDto livroAtualizado)
        {
            try
            {
                // Busca o funcionário existente pelo Id
                var livroExistente = _livroRepo.GetById(id);

                // Verifica se o funcionário foi encontrado
                if (livroExistente == null)
                {
                    return NotFound(new { Mensagem = "Livro não encontrado." });
                }

                // Atualiza os dados do funcionário existente com os valores do objeto recebido
                livroExistente.Titulo = livroAtualizado.Titulo;
                livroExistente.Autor = livroAtualizado.Autor;
                livroExistente.AnoPublcacao = livroAtualizado.AnoPublcacao;
                livroExistente.Disponibilidade = livroAtualizado.Disponibilidade;

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Usuário atualizado com sucesso!",
                    Titulo = livroExistente.Titulo,
                    Autor = livroExistente.Autor,
                    AnoPublcacoa = livroExistente.AnoPublcacao,
                    Disponibilidade = livroExistente.Disponibilidade,
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
                var livroExistente = _livroRepo.GetById(id);

                // Verifica se o funcionário foi encontrado
                if (livroExistente == null)
                {
                    return NotFound(new { Mensagem = "Livro não encontrado." });
                }

                // Chama o método de exclusão do repositório
                _livroRepo.Delete(id);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Usuário excluído com sucesso!",
                    Titulo = livroExistente.Titulo,
                    Autor = livroExistente.Autor,
                    AnoPublcacoa = livroExistente.AnoPublcacao,
                    Disponibilidade = livroExistente.Disponibilidade,
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
