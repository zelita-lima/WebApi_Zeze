﻿using System;
using System.Collections.Generic;

namespace WebApi_Zeze.ORM;

public partial class TbLivro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public DateOnly AnoPublcacao { get; set; }

    public int FkCategoria { get; set; }

    public bool Disponibilidade { get; set; }

    public virtual TbCategoria FkCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<TbEmprestimo> TbEmprestimos { get; set; } = new List<TbEmprestimo>();

    public virtual ICollection<TbReserva> TbReservas { get; set; } = new List<TbReserva>();
}
