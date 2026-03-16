using FluentValidation;
using LabScore.io.Server.Data;
using LabScore.io.Server.Model;
using LabScore.io.Server.Repository;
using LabScore.io.Server.Service;
using LabScore.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

connectionString = connectionString
    .Replace("{DB_USER}", Environment.GetEnvironmentVariable("DB_USER_POSTGRES") ?? throw new InvalidOperationException("Variável de ambiente DB_USER_POSTGRES não definida."))
    .Replace("{DB_PASS}", Environment.GetEnvironmentVariable("DB_PASS_POSTGRES") ?? throw new InvalidOperationException("Variável de ambiente DB_PASS_POSTGRES não definida."));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

// Registrando repositórios  
builder.Services.AddScoped<IRepository<Simulado>, SimuladoRepository>();
builder.Services.AddScoped<IRepositoryQuestao, QuestaoRepository>();

// Registrando serviços
builder.Services.AddScoped<ISimuladoService, SimuladoService>();
builder.Services.AddScoped<IQuestaoService, QuestaoService>();

// Registrando validadores
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


builder.Services.AddAutoMapper(cfg =>
{
}, typeof(Program).Assembly);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(a =>
{
    a.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LabScore API",
        Version = "v1",
        Description = "API para simulação de questões de concurso com resultado imediato.",
        License = new OpenApiLicense
        {
            Name = "Apache License 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0")
        },
        Contact = new OpenApiContact
        {
            Name = "Larissa Brasil",
            Email = "larissabrasil009@gmail.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        a.IncludeXmlComments(xmlPath);
    }
});


var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("DefaultCors");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LabScore API"));


app.UseDefaultFiles();
app.MapStaticAssets();


app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
