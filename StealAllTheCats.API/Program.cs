using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Business.Interfaces;
using StealAllTheCats.Business.Services;
using StealAllTheCats.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StealAllTheCatsContext>(options =>
    options.UseInMemoryDatabase(databaseName: "StealAllTheCatsDb"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICatRepository, CatService>();

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
