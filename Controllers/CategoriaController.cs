using Microsoft.AspNetCore.Mvc;
using WebApi_Zeze.Model;
using WebApi_Zeze.Repositorio;

namespace WebApi_Zeze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly CategoriaRepositorio _categoriaRepo;

        public CategoriaController(CategoriaRepositorio categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }

        // GET: api/Categoria/{id}
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(int id)
        {
            try
            {
                // Chama o repositório para obter a categoria pelo ID
                var categoria = _categoriaRepo.GetById(id);

                // Se a categoria não for encontrada, retorna uma resposta 404
                if (categoria == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." }); // Retorna 404 com mensagem
                }

                // Mapeia a categoria encontrada
                var categoriaComUrl = new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Categorias = categoria.Categorias,
                };

                // Retorna a categoria com status 200 OK
                return Ok(categoriaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar a categoria.", Detalhes = ex.Message });
            }
        }

        // GET: api/Categoria
        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todas as categorias
                var categorias = _categoriaRepo.GetAll();

                // Verifica se a lista de categorias está vazia
                if (categorias == null || !categorias.Any())
                {
                    return NotFound(new { Mensagem = "Nenhuma categoria encontrada." });
                }

                // Mapeia a lista de categorias
                var listaComUrl = categorias.Select(categoria => new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Categorias = categoria.Categorias,
                }).ToList();

                // Retorna a lista de categorias com status 200 OK
                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar as categorias.", Detalhes = ex.Message });
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] CategoriaDto novoCategoria)
        {
            try
            {
                // Cria uma nova instância da categoria a partir do DTO recebido
                var categoria = new Categoria
                {
                    Nome = novoCategoria.Nome,
                    Categorias = novoCategoria.Categorias,
                };

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Categoria cadastrada com sucesso!",
                    Nome = categoria.Nome,
                    Categoria = categoria.Categorias,
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar a categoria.", Detalhes = ex.Message });
            }
        }

        // PUT api/<CategoriaController>
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] CategoriaDto categoriaAtualizado)
        {
            try
            {
                // Busca a categoria existente pelo Id
                var categoriaExistente = _categoriaRepo.GetById(id);

                // Verifica se a categoria foi encontrada
                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." });
                }

                // Atualiza os dados da categoria existente com os valores do objeto recebido
                categoriaExistente.Nome = categoriaAtualizado.Nome;
                categoriaExistente.Categorias = categoriaAtualizado.Categorias;

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Categoria atualizada com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Categoria = categoriaExistente.Categorias,
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar a categoria.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Busca a categoria existente pelo Id
                var categoriaExistente = _categoriaRepo.GetById(id);

                // Verifica se a categoria foi encontrada
                if (categoriaExistente == null)
                {
                    return NotFound(new { Mensagem = "Categoria não encontrada." });
                }

                // Chama o método de exclusão do repositório
                _categoriaRepo.Delete(id);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Categoria excluída com sucesso!",
                    Nome = categoriaExistente.Nome,
                    Categoria = categoriaExistente.Categorias,
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção e retorna um erro 500 com os detalhes do erro
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir a categoria.", Detalhes = ex.Message });
            }
        }

    }
}
