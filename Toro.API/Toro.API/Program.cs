using Toro.Data.DbContext;
using Toro.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configurar Servi�os, incluindo Autentica��o e Autoriza��o Global
 ConfigureServices(builder.Services, builder.Configuration);

 void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
 {
     services.AddServices(configuration);
}

 builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar servi�os de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevServer", policy =>
    {
        policy.WithOrigins("*")  // Permitir o endere�o do seu frontend
            .AllowAnyHeader()                   // Permitir qualquer cabe�alho
            .AllowAnyMethod();                   // Permitir qualquer m�todo
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAngularDevServer");  // Adicione essa linha para habilitar o CORS

app.UseRouting();

app.MapControllers();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ToroContext>();
    db.Database.EnsureCreated();
}

app.Run();
