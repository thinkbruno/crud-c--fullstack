using Microsoft.EntityFrameworkCore;
using UserApi.Domain.Interfaces;
using UserApi.Infra.Data;
using UserApi.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURAÇÃO DOS SERVIÇOS (Dependency Injection)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ThinkBruno User API",
        Version = "v1",
        Description = "API de Gestão de Usuários desenvolvida em .NET 8 para portfólio."
    });
});

// Configurando o SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrando o Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// CONFIGURAÇÃO DO PIPELINE DE HTTP (Middlewares)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();