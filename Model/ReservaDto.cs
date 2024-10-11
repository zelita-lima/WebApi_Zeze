using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Model
{
    public class ReservaDto
    {
        public DateOnly DataReserva { get; set; }

        public int FkMembros { get; set; }

        public int FkLivros { get; set; }

        public virtual TbLivro FkLivrosNavigation { get; set; } = null!;

        public virtual TbMembro FkMembrosNavigation { get; set; } = null!;
    }
}
