using System;
using System.Collections.Generic;

namespace WebApi_Zeze.ORM;

public partial class TbUsuarioTarde
{
    public int Id { get; set; }

    public string Usuario { get; set; } = null!;

    public string Senha { get; set; } = null!;
}
