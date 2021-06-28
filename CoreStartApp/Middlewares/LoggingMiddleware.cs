using System;
using System.IO;
using System.Threading.Tasks;
using CoreStartApp.Models;
using Microsoft.AspNetCore.Http;

namespace CoreStartApp.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private IUserInfoRepository _repo;
        
        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, IUserInfoRepository repo)
        {
            _next = next;
            _repo = repo;
        }
        
        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            string userAgent = context.Request.Headers["User-Agent"][0];

            var newUserInfo = new UserInfo()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                UserAgent = userAgent
            };

            await _repo.Add(newUserInfo);
            
            LogConsole(context);
            await LogFile(context);
            
            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
        
        private void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }
        
        private async Task LogFile(HttpContext context)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
                
            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
                
            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }
    }
}