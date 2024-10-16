using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Model
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        public string Categorias { get; set; } = null!;

        public virtual ICollection<TbLivro> TbLivros { get; set; } = new List<TbLivro>();
    }
}
