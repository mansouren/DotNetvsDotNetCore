using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpHandler
{
    public class ReportHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            if (HasReportExtension(context))
            {
                string response = GenerateResponse();
                context.Response.Output.Write(response);
            }
        }

        private bool HasReportExtension(HttpContext context)
        {
            string url = context.Request.Url.ToString();
            return url.EndsWith(".Report");
        }

        private string GenerateResponse()
        {
            return "This Url has .report extension";
        }
    }
}