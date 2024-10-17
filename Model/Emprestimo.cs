using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Model
{
    public class Emprestimo
    {
        public int Id { get; set; }

        public DateOnly DataEmprestimo { get; set; }

        public DateOnly DataDevolusao { get; set; }

        public int FkMembros { get; set; }

        public int FkLivros { get; set; }
    }
}
