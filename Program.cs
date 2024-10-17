using Microsoft.EntityFrameworkCore;
using WebApi_Zeze.ORM;
using WebApi_Zeze.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adicione o contexto do banco de dados funcionario
builder.Services.AddDbContext<BibliotecaWebApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Adicione o repositório FuncionarioR
builder.Services.AddScoped<FuncionarioRepositorio>();

// Adicione o repositório UsuarioR
builder.Services.AddScoped<UsuarioRepositorio>();

// Adicione o repositório UsuarioR
builder.Services.AddScoped<MembroRepositorio>();

// Adicione o repositório UsuarioR
builder.Services.AddScoped<LivroRepositorio>();

// Adicione o repositório UsuarioR
builder.Services.AddScoped<ReservaRepositorio>();

// Adicione o repositório UsuarioR
builder.Services.AddScoped<CategoriaRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
