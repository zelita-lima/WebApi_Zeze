﻿using WebApi_Zeze.Model;
using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Repositorio
{
    public class EmprestimoRepositorio
    {
        private readonly BibliotecaWebApiContext _context;

        public EmprestimoRepositorio(BibliotecaWebApiContext context)
        {
            _context = context;
        }


        public void Add(Emprestimo emprestimo)
        {

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbEmprestimo = new TbEmprestimo()
            {
                DataDevolusao = emprestimo.DataDevolusao,
                DataEmprestimo = emprestimo.DataDevolusao,
                FkLivros = emprestimo.FkLivros,
                FkMembros = emprestimo.FkMembros,
            };

            // Adiciona a entidade ao contexto
            _context.TbEmprestimos.Add(tbEmprestimo);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEmprestimo != null)
            {
                // Remove a entidade do contexto
                _context.TbEmprestimos.Remove(tbEmprestimo);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public List<Emprestimo> GetAll()
        {
            List<Emprestimo> listFun = new List<Emprestimo>();

            var listTb = _context.TbEmprestimos.ToList();

            foreach (var item in listTb)
            {
                var emprestimo = new Emprestimo
                {
                    DataDevolusao = item.DataDevolusao,
                    DataEmprestimo = item.DataEmprestimo,
                    FkMembros = item.FkMembros,
                    FkLivros = item.FkLivros,
                };

                listFun.Add(emprestimo);
            }

            return listFun;
        }

        public Emprestimo GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbEmprestimos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var emprestimo = new Emprestimo
            {
                DataDevolusao = item.DataDevolusao,
                DataEmprestimo = item.DataEmprestimo,
                FkLivros = item.FkLivros,
                FkMembros = item.FkMembros,
            };

            return emprestimo; // Retorna o funcionário encontrado
        }

        public void Update(Emprestimo emprestimo)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(f => f.Id == emprestimo.Id);

            // Verifica se a entidade foi encontrada
            if (tbEmprestimo != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbEmprestimo.DataEmprestimo = emprestimo.DataEmprestimo;
                tbEmprestimo.DataDevolusao = emprestimo.DataDevolusao;
                tbEmprestimo.FkMembros = emprestimo.FkMembros;
                tbEmprestimo.FkLivros = emprestimo.FkLivros;

                // Atualiza as informações no contexto
                _context.TbEmprestimos.Update(tbEmprestimo);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }
    }
}
