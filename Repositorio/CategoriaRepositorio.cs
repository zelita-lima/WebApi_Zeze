using WebApi_Zeze.Model;
using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Repositorio
{
    public class CategoriaRepositorio
    {
        private readonly BibliotecaWebApiContext _context;

        public CategoriaRepositorio(BibliotecaWebApiContext context)
        {
            _context = context;
        }

        public void Add(Categoria categoria)
        {

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbCategoria = new TbCategoria()
            {
                Nome = categoria.Nome,
                Categoria = categoria.Categorias,
            };

            // Adiciona a entidade ao contexto
            _context.TbCategorias.Add(tbCategoria);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCategoria = _context.TbCategorias.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbCategoria != null)
            {
                // Remove a entidade do contexto
                _context.TbCategorias.Remove(tbCategoria);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public List<Categoria> GetAll()
        {
            List<Categoria> listFun = new List<Categoria>();

            var listTb = _context.TbCategorias.ToList();

            foreach (var item in listTb)
            {
                var categoria = new Categoria
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Categorias = item.Categoria,
                };

                listFun.Add(categoria);
            }

            return listFun;
        }

        public Categoria GetById(int id)
        {
            // Busca a categoria pelo ID no banco de dados
            var item = _context.TbCategorias.FirstOrDefault(f => f.Id == id);

            // Verifica se a categoria foi encontrada
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var categoria = new Categoria
            {
                Id = item.Id,
                Nome = item.Nome,
                Categorias = item.Categoria,
            };

            return categoria; // Retorna o funcionário encontrado
        }

        public void Update(Categoria categoria)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCategoria = _context.TbCategorias.FirstOrDefault(f => f.Id == categoria.Id);

            // Verifica se a entidade foi encontrada
            if (tbCategoria != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbCategoria.Nome = categoria.Nome;
                tbCategoria.Categoria = categoria.Categorias;

                // Atualiza as informações no contexto
                _context.TbCategorias.Update(tbCategoria);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }

    }
}
