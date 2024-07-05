using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.Data;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
//builder.Services.AddDbContext<SimpleWebApiDbContext>(options =>
//{
//    options.EnableDetailedErrors();
//    options.UseSqlite(builder.Configuration.GetConnectionString("SimpleWebApiDbContext")
//        ?? throw new InvalidOperationException("Connection string 'SimpleWebApiDbContext' not found."), (options) =>
//        {
//            options.CommandTimeout(60);
//        });
//});

builder.Services.AddDbContext<SimpleWebApiDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SimpleWebApiDbContextSql");
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
    });
});


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

await using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<SimpleWebApiDbContext>();
    await dbContext.Database.MigrateAsync();
}


app.Run();

public partial class Program { }