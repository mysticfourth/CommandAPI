using CommandAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<CommandContext>();
    dataContext.Database.Migrate();
}

var builderData = new SqlConnectionStringBuilder();

builderData.ConnectionString = builder.Configuration.GetConnectionString("SqlServerSqlConnection");


builderData.UserID = builder.Configuration["UserID"];
builderData.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(opt => opt.UseSqlServer
(builderData.ConnectionString));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
