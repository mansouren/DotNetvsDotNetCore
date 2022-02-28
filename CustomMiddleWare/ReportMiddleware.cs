using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomMiddleWare
{
    public class ReportMiddleware
    {
        private readonly RequestDelegate next;

        public ReportMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string content =GenerateResponse();
            await context.Response.WriteAsync(content);
            await next(context);
        }
        private string GenerateResponse()
        {
            return "This Url has .report extension";
        }
    }
}