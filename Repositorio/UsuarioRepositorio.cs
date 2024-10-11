using WebApi_Zeze.Model;
using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly BibliotecaWebApiContext _context;

        public UsuarioRepositorio(BibliotecaWebApiContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbUsuario = new TbUsuario()
            {
                Id = usuario.Id,
                Usuario = usuario.Name,
                Senha = usuario.Senha,
            };

            // Adiciona a entidade ao contexto
            _context.TbUsuarios.Add(tbUsuario);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbUsuario = _context.TbUsuarios.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbUsuario != null)
            {
                // Remove a entidade do contexto
                _context.TbUsuarios.Remove(tbUsuario);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> listFun = new List<Usuario>();

            var listTb = _context.TbUsuarios.ToList();

            foreach (var item in listTb)
            {
                var usuario = new Usuario
                {
                    Id = item.Id,
                    Name = item.Usuario,
                    Senha = item.Senha,
                };

                listFun.Add(usuario);
            }

            return listFun;
        }

        public Usuario GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbUsuarios.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var usuario = new Usuario
            {
                Id = item.Id,
                Name = item.Usuario,
                Senha = item.Senha,
            };

            return usuario; // Retorna o funcionário encontrado
        }

        public void Update(Usuario usuario)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbUsuario = _context.TbUsuarios.FirstOrDefault(f => f.Id == usuario.Id);

            // Verifica se a entidade foi encontrada
            if (tbUsuario != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbUsuario.Id = usuario.Id;
                tbUsuario.Usuario = usuario.Name;
                tbUsuario.Senha = usuario.Senha;

                // Atualiza as informações no contexto
                _context.TbUsuarios.Update(tbUsuario);

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
