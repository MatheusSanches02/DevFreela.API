using DevFreela.API.ExceptionHandler;
using DevFreela.API.Models;
using DevFreela.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<FreelancerTotalCostingConfig>(
    builder.Configuration.GetSection("FreelanceTotalCostConfig")
);

builder.Services.AddSingleton<IConfigService, ConfigService>(); // Mesma instancia em toda  aplicação, mantem a mesma instancia durante toda vida
//builder.Services.AddScoped<IConfigService, ConfigService>(); // É utilizada uma instancia pra cada requisição  
//builder.Services.AddTransient<IConfigService, ConfigService>(); // Utilizada uma instancia por uso (objetos diferentes na requisição)

builder.Services.AddExceptionHandler<APIExceptionHandler>();
builder.Services.AddProblemDetails();

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

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
