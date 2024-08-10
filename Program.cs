using dotnet_webapi_ef.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>();

builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


var app = builder.Build();



// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// app.UseSwagger();
// app.UseSwaggerUI();
// }


// Add Base Path in case of reverse proxy used
var basePath = "/tripajm";
app.UsePathBase(new PathString(basePath));
var host = "backend.csmsu.net:8008";
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{host}{basePath}" } ,
        new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}" }
        };
    });
});
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();

