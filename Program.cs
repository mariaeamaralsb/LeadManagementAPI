using LeadManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",new OpenApiInfo
    {
        Title="Lead Management API",
        Version="v1",
        Description="API para gerenciamento de leads com endpoints de aceitação e recusa.",
        Contact=new OpenApiContact
        {
            Name="Equipe de Desenvolvimento Maria Amaral",
            Email="mariaeduardaamaralsb@gmail.com",
            Url=new Uri("https://www.empresa.com")
        }
    });
});

builder.Services.AddDbContext<LeadContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Lead Management API v1");
        c.RoutePrefix=string.Empty; // Opcional: acessa o Swagger na raiz do app (ex: http://localhost:5000)
    });
}

app.MapControllers();
app.Run();
