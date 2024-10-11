using System;
using System.Collections.Generic;

namespace WebApi_Zeze.ORM;

public partial class TbEmprestimo
{
    public int Id { get; set; }

    public DateOnly DataEmprestimo { get; set; }

    public DateOnly DataDevolusao { get; set; }

    public int FkMembros { get; set; }

    public int FkLivros { get; set; }

    public virtual TbLivro FkLivrosNavigation { get; set; } = null!;

    public virtual TbMembro FkMembrosNavigation { get; set; } = null!;
}
