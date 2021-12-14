using DB_API_TEST.Database;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;

using DB_API_TEST.Serives;
using DB_API_TEST.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<TestDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevDb")));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

// setup event listeners
EMBootstrap.SetupEMListeners();

app.Run();
