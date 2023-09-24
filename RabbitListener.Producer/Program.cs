using AutoMapper;
using Microsoft.OpenApi.Models;
using RabbitListener.Data.Services.Abstract;
using RabbitListener.Data.Services.Concrete;
using RabbitListener.Dto.ConfigurationModels;
using RabbitListener.Producer.Mapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Rabbitmq configuration
builder.Services.Configure<RabbitConfiguration>(builder.Configuration.GetSection("RabbitConfiguration"));


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Akakce Case Study",

        License = new OpenApiLicense
        {
            Name = "GNU General Public License",
            Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.html")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.EnableAnnotations();
});


builder.Services.AddScoped<IRabbitProducerService ,RabbitProducerService>();


//Mapper configurations
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());


var app = builder.Build();

// Configure the HTTP request pipeline.
//removed for release build
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


//Cors configuration didnt added


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
