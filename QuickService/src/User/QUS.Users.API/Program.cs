using Microsoft.EntityFrameworkCore;
using QUS.Users.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();



app.UseAuthorization();

app.MapControllers();

app.Run();
