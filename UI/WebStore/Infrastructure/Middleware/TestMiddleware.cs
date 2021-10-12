using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<TestMiddleware> _Logger;

        public TestMiddleware(RequestDelegate next, ILogger<TestMiddleware> Logger)
        {
            _Next = next;
            _Logger = Logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //context.Response.WriteAsJsonAsync()
            
            //Предобработка
           var processing = _Next(context); //Запуск следующего слоя промежуточного ПО

            // обработка параллельно
            
            await processing;// Ожидание завершения обработки следующей частью конвейра
             
            //Постобработка данных
        }
    }
}
