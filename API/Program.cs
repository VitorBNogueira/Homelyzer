using Application;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;
        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(typeof(ApplicationAssemblyEntryPoint).Assembly); // fetches everything to do with MediatR in the assembly where the class resides (in this case, Application)
        builder.Services.AddAutoMapper(typeof(ApplicationAssemblyEntryPoint).Assembly); // fetches everything to do with AutoMapper in the assembly where the class resides (in this case, API)
        builder.Services.AddDbContext<HomelyzerDBContext>(options => options.UseSqlServer(config["ConnectionStrings:HomelyzerDBContext"]));

        builder.Services.AddScoped<IAdvertRepository, AdvertRepository>();
        builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
        builder.Services.AddScoped<IPictureRepository, PictureRepository>();

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

        app.Run();
    }
}