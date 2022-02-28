using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpHandler
{
    public class ReportModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += Context_AuthenticateRequest;
        }

        private void Context_AuthenticateRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            if (!IsAuthenticated(context))
            {
                context.Response.Write("Please login before accessing the report");
            }
        }

        private bool IsAuthenticated(HttpContext context)
        {
            return context.User == null ? false : true;
        }
    }
}