using Microsoft.AspNetCore.Mvc;
using WebApi_Zeze.Model;
using WebApi_Zeze.Repositorio;

namespace WebApi_Zeze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : Controller
    {
        private readonly ReservaRepositorio _reservaRepo;

        public ReservaController(ReservaRepositorio reservaRepo)
        {
            _reservaRepo = reservaRepo;
        }
        // GET: api/Reserva/{id}
        [HttpGet("{id}")]
        public ActionResult<Reserva> GetById(int id)
        {
            try
            {
                // Chama o repositório para obter a reserva pelo ID
                var reserva = _reservaRepo.GetById(id);

                // Se a reserva não for encontrada, retorna uma resposta 404
                if (reserva == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                // Retorna a reserva com status 200 OK
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar a reserva.", Detalhes = ex.Message });
            }
        }

        // GET: api/Reserva
        [HttpGet]
        public ActionResult<List<Reserva>> GetAll()
        {
            try
            {
                // Chama o repositório para obter todas as reservas
                var reservas = _reservaRepo.GetAll();

                // Verifica se a lista está vazia
                if (reservas == null || !reservas.Any())
                {
                    return NotFound(new { Mensagem = "Nenhuma reserva encontrada." });
                }

                // Retorna a lista de reservas com status 200 OK
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao buscar as reservas.", Detalhes = ex.Message });
            }
        }

        // POST api/Reserva
        [HttpPost]
        public ActionResult<object> Post([FromForm] ReservaDto novoReserva)
        {
            try
            {
                // Cria uma nova instância da reserva
                var reserva = new Reserva
                {
                    DataReserva = novoReserva.DataReserva,
                    FkMembrosNavigation = novoReserva.FkMembrosNavigation,
                    FkLivrosNavigation = novoReserva.FkLivrosNavigation,
                    FkMembros = novoReserva.FkMembros,
                    FkLivros = novoReserva.FkLivros,
                };

                // Retorna o objeto com status 200 OK
                return Ok(new
                {
                    Mensagem = "Reserva cadastrada com sucesso!",
                    DataReserva = reserva.DataReserva,
                    FkLivros = reserva.FkLivros,
                    FkMembros = reserva.FkMembros,
                    FkMembrosNavigation = reserva.FkMembrosNavigation,
                    FkLivrosNavigation = reserva.FkLivrosNavigation,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao cadastrar a reserva.", Detalhes = ex.Message });
            }
        }

        // PUT api/Reserva/{id}
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ReservaDto reservaAtualizado)
        {
            try
            {
                // Busca a reserva existente pelo Id
                var reservaExistente = _reservaRepo.GetById(id);

                // Verifica se a reserva foi encontrada
                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                // Atualiza os dados da reserva existente com os valores do objeto recebido
                reservaExistente.DataReserva = reservaAtualizado.DataReserva;
                reservaExistente.FkLivros = reservaAtualizado.FkLivros;
                reservaExistente.FkMembros = reservaAtualizado.FkMembros;
                reservaExistente.FkMembrosNavigation = reservaAtualizado.FkMembrosNavigation;
                reservaExistente.FkLivrosNavigation = reservaAtualizado.FkLivrosNavigation;

                // Retorna o objeto com status 200 OK
                return Ok(new
                {
                    Mensagem = "Reserva atualizada com sucesso!",
                    FkLivros = reservaExistente.FkLivros,
                    FkMembros = reservaExistente.FkMembros,
                    FkMembrosNavigation = reservaExistente.FkMembrosNavigation,
                    FkLivrosNavigation = reservaExistente.FkLivrosNavigation,
                    DataReserva = reservaExistente.DataReserva,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar a reserva.", Detalhes = ex.Message });
            }
        }

        // DELETE api/Reserva/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Busca a reserva existente pelo Id
                var reservaExistente = _reservaRepo.GetById(id);

                // Verifica se a reserva foi encontrada
                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                // Chama o método de exclusão do repositório
                _reservaRepo.Delete(id);

                // Retorna o objeto com status 200 OK
                return Ok(new
                {
                    Mensagem = "Reserva excluída com sucesso!",
                    FkLivros = reservaExistente.FkLivros,
                    FkMembros = reservaExistente.FkMembros,
                    FkMembrosNavigation = reservaExistente.FkMembrosNavigation,
                    FkLivrosNavigation = reservaExistente.FkLivrosNavigation,
                    DataReserva = reservaExistente.DataReserva,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir a reserva.", Detalhes = ex.Message });
            }
        }

    }
}
