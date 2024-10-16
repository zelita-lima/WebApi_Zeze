﻿using WebApi_Zeze.ORM;

namespace WebApi_Zeze.Model
{
    public class FuncionarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; } = null!;
        public string Telefone { get; set; }
        public string Cargo { get; set; } = null!;


        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public DateOnly AnoPublcacao { get; set; }

        public int FkCategoria { get; set; }

        public bool Disponibilidade { get; set; }

        public virtual TbCategoria FkCategoriaNavigation { get; set; } = null!;
    }
}
