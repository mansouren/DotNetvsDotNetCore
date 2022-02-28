using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare
{
    public class ReportModuleMiddleWare
    {
        private readonly RequestDelegate next;

        public ReportModuleMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Please login before accessing the report");
            await next(context);
        }
    }
}
