using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StealAllTheCats.Business.Interfaces;
using StealAllTheCats.Business.Services;
using StealAllTheCats.Data.Context;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<StealAllTheCatsContext>(options =>
//    options.UseInMemoryDatabase(databaseName: "StealAllTheCatsDb"));

builder.Services.AddDbContext<StealAllTheCatsContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<DbContext, StealAllTheCatsContext>();
builder.Services.AddScoped<ICatRepository, CatService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Steal The Cats API",
        Description = "An ASP.NET Core Web API for Stealing Cat Photos.",
        Contact = new OpenApiContact
        {
            Name = "Georgia Tsirigotaki",
            Url = new Uri("https://www.linkedin.com/in/georgia-tsirigotaki/")
        }
    });
    c.EnableAnnotations();
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

app.MapControllers();

app.Run();