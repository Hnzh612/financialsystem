using Microsoft.Extensions.PlatformAbstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo { Title = "鑫恒佳耀有限公司" ,Version = "v1" });
        var basePath = PlatformServices.Default.Application.ApplicationBasePath;
        c.IncludeXmlComments(Path.Combine(basePath, "ManagementSystem.xml"));
        c.IncludeXmlComments(Path.Combine(basePath, "hxjyController.xml"));
        c.IncludeXmlComments(Path.Combine(basePath, "hxjyModel.xml"));
        c.IncludeXmlComments(Path.Combine(basePath, "hxjyServices.xml"));
    }
    );

builder.WebHost.UseUrls(new[] { "http://*:5001" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
