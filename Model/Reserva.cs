using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Model
{
    public class Reserva
    {

        public int Id { get; set; }

        public DateOnly DataReserva { get; set; }

        public int FkMembros { get; set; }

        public int FkLivros { get; set; }

        public virtual TbLivro FkLivrosNavigation { get; set; } = null!;

        public virtual TbMembro FkMembrosNavigation { get; set; } = null!;
    }
}
