using Microsoft.EntityFrameworkCore;
using QUS.Users.Application;
using QUS.Users.Data;
using QUS.Users.Data.Repository;
using QUS.Users.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();



app.UseAuthorization();

app.MapControllers();

app.Run();
