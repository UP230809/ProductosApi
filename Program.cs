var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

var app = builder.Build();

// Crea la base de datos y la tabla si no existen
DbInicializador.Inicializar(builder.Configuration.GetConnectionString("Default")!);

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();