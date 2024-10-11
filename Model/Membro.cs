﻿using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Model
{
    public class Membro
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telefone { get; set; } = null!;

        public DateOnly DataCadastro { get; set; }

        public string TipoMembro { get; set; } = null!;

        public virtual ICollection<TbEmprestimo> TbEmprestimos { get; set; } = new List<TbEmprestimo>();

        public virtual ICollection<TbReserva> TbReservas { get; set; } = new List<TbReserva>();
    }
}
