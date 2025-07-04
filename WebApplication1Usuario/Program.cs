var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obter a connection string do appsettings.json
var conn = builder.Configuration.GetConnectionString("conexao");
Console.WriteLine($"Connection String Teste: {conn}");

// Registrar o UsuarioRepositorio como implementação de InterfaceUserRepo, passando a connection string
builder.Services.AddScoped<domain.repositorio.InterfaceUserRepo>(provider => new usuarioRepositorio.UsuarioRepositorio(conn));

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
