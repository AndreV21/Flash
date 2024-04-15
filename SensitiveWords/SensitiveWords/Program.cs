using Microsoft.EntityFrameworkCore;
using SensitiveWords.Interface;
using SensitiveWords.Models;
using SensitiveWords.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SensitiveWordContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ISensitiveWordsFilterService, SensitiveWordsFilterService>();
builder.Services.AddScoped<ISensitiveWordsService, SensitiveWordsService>();


// Add services to the container.

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
