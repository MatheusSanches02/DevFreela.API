using DevFreela.API.Models;
using DevFreela.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<FreelancerTotalCostingConfig>(
    builder.Configuration.GetSection("FreelanceTotalCostConfig")
);

builder.Services.AddSingleton<IConfigService, ConfigService>(); // Mesma instancia em toda  aplica��o, mantem a mesma instancia durante toda vida
builder.Services.AddScoped<IConfigService, ConfigService>(); // � utilizada uma instancia pra cada requisi��o  
builder.Services.AddTransient<IConfigService, ConfigService>(); // Utilizada uma instancia por uso (objetos diferentes na requisi��o)


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.Run();
