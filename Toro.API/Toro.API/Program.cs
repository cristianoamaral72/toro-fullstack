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


// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
