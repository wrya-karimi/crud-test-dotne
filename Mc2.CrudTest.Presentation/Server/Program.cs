global using Mc2.CrudTest.Presentation.Shared;
using AutoMapper;
using Mc2.CrudTest.Domain.Abstracts.Application;
using Mc2.CrudTest.Domain.Abstracts.Repositories.CustomerRepositories;
using Mc2.CrudTest.Domain.Application;
using Mc2.CrudTest.Infrastructure.Persistence.Contexts;
using Mc2.CrudTest.Infrastructure.Persistence.Respositories.CustomerRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;

namespace Mc2.CrudTest.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            

            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}