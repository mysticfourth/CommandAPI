using CommandAPI.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);



//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<CommandContext>();

//    //dataContext.Database.Migrate();
//    dataContext.Database.EnsureCreated();
//}

var builderData = new SqlConnectionStringBuilder();

builderData.ConnectionString = builder.Configuration.GetConnectionString("SqlServerSqlConnection");


builderData.UserID = builder.Configuration["UserID"];
builderData.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(opt => opt.UseSqlServer
(builderData.ConnectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.Audience = builder.Configuration["ResourceId"];
    opt.Authority = $"{builder.Configuration["Instance"]}{builder.Configuration["TenantId"]}";
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//app.UseAuthentication();

//app.UseAuthorization();

app.UseHttpsRedirection();

//app.UseRouting();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
