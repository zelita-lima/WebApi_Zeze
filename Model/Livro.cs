using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Model
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public DateOnly AnoPublcacao { get; set; }

        public int FkCategoria { get; set; }

        public bool Disponibilidade { get; set; }
       
    }
}
