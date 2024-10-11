namespace WebApi_Zeze.Model
{
    public class FuncionarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; } = null!;
        public string Telefone { get; set; }
        public string Cargo { get; set; } = null!;
    }
}
