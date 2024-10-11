using WebApi_Zeze.Model;
using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Repositorio
{
    public class ReservaRepositorio
    {
        private readonly BibliotecaWebApiContext _context;

        public ReservaRepositorio(BibliotecaWebApiContext context)
        {
            _context = context;
        }
        public void Add(Reserva reserva)
        {

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbReserva = new TbReserva()
            {
                Id = reserva.Id,
                DataReserva = reserva.DataReserva,
                FkMembros = reserva.FkMembros,
                FkLivros = reserva.FkLivros,
                FkLivrosNavigation = reserva.FkLivrosNavigation,
                FkMembrosNavigation = reserva.FkMembrosNavigation,
            };

            // Adiciona a entidade ao contexto
            _context.TbReservas.Add(tbReserva);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbReserva = _context.TbReservas.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbReserva != null)
            {
                // Remove a entidade do contexto
                _context.TbReservas.Remove(tbReserva);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Membro não encontrado.");
            }
        }

        public List<Reserva> GetAll()
        {
            List<Reserva> listFun = new List<Reserva>();

            var listTb = _context.TbReservas.ToList();

            foreach (var item in listTb)
            {
                var reserva = new Reserva
                {
                    Id = item.Id,
                    DataReserva = item.DataReserva,
                    FkMembros = item.FkMembros,
                    FkLivros = item.FkLivros,
                    FkLivrosNavigation = item.FkLivrosNavigation,
                    FkMembrosNavigation = item.FkMembrosNavigation,
                };

                listFun.Add(reserva);
            }

            return listFun;
        }

        public Reserva GetById(int id)
        {
            // Busca o membro pelo ID no banco de dados
            var item = _context.TbReservas.FirstOrDefault(f => f.Id == id);

            // Verifica se o membro foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var reserva = new Reserva
            {
                Id = item.Id,
                DataReserva = item.DataReserva,
                FkMembros = item.FkMembros,
                FkLivros = item.FkLivros,
                FkLivrosNavigation = item.FkLivrosNavigation,
                FkMembrosNavigation = item.FkMembrosNavigation,
            };

            return reserva; // Retorna o funcionário encontrado
        }

        public void Update(Reserva reserva)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbReserva = _context.TbReservas.FirstOrDefault(f => f.Id == reserva.Id);

            // Verifica se a entidade foi encontrada
            if (tbReserva != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbReserva.Id = reserva.Id;
                tbReserva.DataReserva = reserva.DataReserva;
                tbReserva.FkMembrosNavigation = reserva.FkMembrosNavigation;
                tbReserva.FkMembros = reserva.FkMembros;
                tbReserva.FkLivros = reserva.FkLivros;
                tbReserva.FkLivrosNavigation = reserva.FkLivrosNavigation;

                // Atualiza as informações no contexto
                _context.TbReservas.Update(tbReserva);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
    }
}
