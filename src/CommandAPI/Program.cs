using CommandAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);

var builderData = new SqlConnectionStringBuilder();

builderData.ConnectionString = builder.Configuration.GetConnectionString("SqlServerSqlConnection");


builderData.UserID = builder.Configuration["UserID"];
builderData.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(opt => opt.UseSqlServer
(builderData.ConnectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
