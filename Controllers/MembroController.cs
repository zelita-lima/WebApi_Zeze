using Microsoft.AspNetCore.Mvc;
using WebApi_Zeze.Model;
using WebApi_Zeze.Repositorio;

namespace WebApi_Zeze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembroController : Controller
    {
        private readonly MembroRepositorio _membroRepo;

        public MembroController(MembroRepositorio membroRepo)
        {
            _membroRepo = membroRepo;
        }

        // GET: api/Membro/{id}
        [HttpGet("{id}")]
        public ActionResult<Membro> GetById(int id)
        {
            try
            {
                // Chama o repositório para obter o membro pelo ID
                var membro = _membroRepo.GetById(id);

                // Se o membro não for encontrado, retorna uma resposta 404
                if (membro == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                // Mapeia o membro encontrado para incluir a URL da foto
                var membroComUrl = new Membro
                {
                    Id = membro.Id,
                    Nome = membro.Nome,
                    Email = membro.Email,
                    Telefone = membro.Telefone,
                    DataCadastro = membro.DataCadastro,
                    TipoMembro = membro.TipoMembro,
                    TbEmprestimos = membro.TbEmprestimos,
                    TbReservas = membro.TbReservas
                };

                // Retorna o membro com status 200 OK
                return Ok(membroComUrl);
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção e retorna um erro 500 com os detalhes do erro
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar o membro.", Detalhes = ex.Message });
            }
        }

        // GET: api/Membro
        [HttpGet]
        public ActionResult<List<Membro>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todos os membros
                var membros = _membroRepo.GetAll();

                // Verifica se a lista de membros está vazia
                if (membros == null || !membros.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum usuário encontrado." });
                }

                // Mapeia a lista de membros para incluir a URL da foto
                var listaComUrl = membros.Select(membro => new Membro
                {
                    Id = membro.Id,
                    Nome = membro.Nome,
                    Email = membro.Email,
                    Telefone = membro.Telefone,
                    DataCadastro = membro.DataCadastro,
                    TipoMembro = membro.TipoMembro,
                    TbReservas = membro.TbReservas,
                    TbEmprestimos = membro.TbEmprestimos
                }).ToList();

                // Retorna a lista de membros com status 200 OK
                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção e retorna um erro 500 com os detalhes
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar os membros.", Detalhes = ex.Message });
            }
        }

        // POST api/<MembroController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] MembroDto novoMembro)
        {
            try
            {
                // Cria uma nova instância do modelo Membro a partir do DTO recebido
                var membro = new Membro
                {
                    Nome = novoMembro.Nome,
                    Email = novoMembro.Email,
                    Telefone = novoMembro.Telefone,
                    TipoMembro = novoMembro.TipoMembro,
                    TbEmprestimos = novoMembro.TbEmprestimos,
                    DataCadastro = novoMembro.DataCadastro,
                    TbReservas = novoMembro.TbReservas,
                };

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Usuário cadastrado com sucesso!",
                    Id = membro.Id,
                    Nome = membro.Nome,
                    Email = membro.Email,
                    Telefone = membro.Telefone,
                    DataCadastro = membro.DataCadastro,
                    TipoMembro = membro.TipoMembro,
                    TbReservas = membro.TbReservas,
                    TbEmprestimos = membro.TbEmprestimos,
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção e retorna um erro 500 com os detalhes do erro
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar o membro.", Detalhes = ex.Message });
            }
        }

        // PUT api/<MembroController>
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] MembroDto membroAtualizado)
        {
            try
            {
                // Busca o membro existente pelo Id
                var membroExistente = _membroRepo.GetById(id);

                // Verifica se o membro foi encontrado
                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                // Atualiza os dados do membro existente com os valores do objeto recebido
                membroExistente.Nome = membroAtualizado.Nome;
                membroExistente.Email = membroAtualizado.Email;
                membroExistente.TipoMembro = membroAtualizado.TipoMembro;
                membroExistente.TbReservas = membroAtualizado.TbReservas;
                membroExistente.TbEmprestimos = membroAtualizado.TbEmprestimos;
                membroExistente.Telefone = membroAtualizado.Telefone;

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Usuário atualizado com sucesso!",
                    Id = membroExistente.Id,
                    Nome = membroExistente.Nome,
                    Email = membroExistente.Email,
                    Telefone = membroExistente.Telefone,
                    DataCadastro = membroExistente.DataCadastro,
                    TipoMembro = membroExistente.TipoMembro,
                    TbReservas = membroExistente.TbReservas,
                    TbEmprestimos = membroExistente.TbEmprestimos,
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção e retorna um erro 500 com os detalhes do erro
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar o membro.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<MembroController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Busca o membro existente pelo Id
                var membroExistente = _membroRepo.GetById(id);

                // Verifica se o membro foi encontrado
                if (membroExistente == null)
                {
                    return NotFound(new { Mensagem = "Membro não encontrado." });
                }

                // Chama o método de exclusão do repositório
                _membroRepo.Delete(id);

                // Cria um objeto anônimo para retornar
                var resultado = new
                {
                    Mensagem = "Usuário excluído com sucesso!",
                    Id = membroExistente.Id,
                    Nome = membroExistente.Nome,
                    Email = membroExistente.Email,
                    Telefone = membroExistente.Telefone,
                    DataCadastro = membroExistente.DataCadastro,
                    TipoMembro = membroExistente.TipoMembro,
                    TbReservas = membroExistente.TbReservas,
                    TbEmprestimos = membroExistente.TbEmprestimos,
                };

                // Retorna o objeto com status 200 OK
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção e retorna um erro 500
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir o membro.", Detalhes = ex.Message });
            }
        }
    }
}

