﻿using System;
using System.Collections.Generic;

namespace WebApi_Zeze.ORM;

public partial class TbFuncionario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int Idade { get; set; }

    public byte[]? Foto { get; set; }
}
