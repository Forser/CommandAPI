using Microsoft.EntityFrameworkCore;
using CommandAPI.Data;

var builder = WebApplication.CreateBuilder(args);

var databasebuilder = new Npgsql.NpgsqlConnectionStringBuilder();
databasebuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
databasebuilder.Username = builder.Configuration["UserID"];
databasebuilder.Password = builder.Configuration["Password"];

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
builder.Services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(databasebuilder.ConnectionString));

var app = builder.Build();

if (builder.Environment.IsDevelopment()){
  app.UseDeveloperExceptionPage();
}

app.MapControllers();
app.Run();