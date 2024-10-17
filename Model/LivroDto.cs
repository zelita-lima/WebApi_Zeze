using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Model
{
    public class LIvroDto
    {
        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public DateOnly AnoPublcacao { get; set; }

        public int FkCategoria { get; set; }

        public bool Disponibilidade { get; set; }

        public virtual TbCategoria FkCategoriaNavigation { get; set; } = null!;
    }
}
