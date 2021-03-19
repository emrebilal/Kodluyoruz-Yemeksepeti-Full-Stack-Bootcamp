using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IPFiltering.Filter
{
    public class IpControlAttribute : ActionFilterAttribute
    {
        IConfiguration _configuration;
        public IpControlAttribute(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string controllerName = (string)context.RouteData.Values["Controller"];

            if(!isValid(remoteIp, controllerName))
            {
                context.Result = new ContentResult { StatusCode = (int)HttpStatusCode.Forbidden, Content= "This IP is not authorized here, access denied" };
                return;
            }

            base.OnActionExecuting(context);
        }

        public bool isValid(string ip, string controller)
        {
            bool rule1 = ip == "192.168.1.1" && (controller == "Customers" || controller == "Homes"); //"::1" for local test
            bool rule2 = ip == "192.168.1.2" && controller == "Persons";
            if (!rule1 && !rule2)
                return false;

            return true;
        }
    }
}
