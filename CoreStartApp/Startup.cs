using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreStartApp.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreStartApp
{
    public class Startup
    {
        static IWebHostEnvironment _env;
        
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }
        
        // Метод вызывается средой ASP.NET.
        // Используйте его для подключения сервисов приложения
        // Документация:  https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // Метод вызывается средой ASP.NET.
        // Используйте его для настройки конвейера запросов
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine($"Launching project from: {_env.ContentRootPath}");
            
            // Проверяем, не запущен ли проект в среде разработки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // обрабатываем ошибки HTTP
            app.UseStatusCodePages();

            // Добавляем компонент, отвечающий за маршрутизацию
            app.UseRouting();
            
            // Поддержка статических файлов
            app.UseStaticFiles();
            
            // Подключаем логирвоание с использованием ПО промежуточного слоя
            app.UseMiddleware<LoggingMiddleware>();
            
            //Добавляем компонент с настройкой маршрутов для главной страницы
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Welcome to the {_env.ApplicationName}!");
                });
            });
            
            // Все прочие страницы имеют отдельные обработчики
            app.Map("/about", About);
            app.Map("/config", Config);
            
            app.MapWhen(context => context.Request.Query.ContainsKey("itemId"), Config);
        }
        
        /// <summary>
        ///  Обработчик для страницы About
        /// </summary>
        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{_env.ApplicationName} - ASP.Net Core tutorial project");
            });
        }
        
        /// <summary>
        ///  Обработчик для главной страницы
        /// </summary>
        private static void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {_env.ApplicationName}. App running configuration: {_env.EnvironmentName}");
            });
        }
    }
}