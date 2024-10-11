namespace WebApi_Zeze.Model
{
    public class Funcionario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telefone { get; set; }

        public string Cargo { get; set; } = null!;
    }
}
