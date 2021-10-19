using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List_Library.Application.Contracts.Identity;
using To_Do_List_Library.Identity.Configuration;

namespace To_Do_List_Library.Presentation.API.Controllers.Filters
{
    public class AuthTokenFilter : ActionFilterAttribute 
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthTokenFilter()
        {

        }
        public AuthTokenFilter(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string authToken = context.HttpContext.Request.Headers[HeaderNames.Authorization];

            if(authToken is null)
            {
                throw new Exception("No Bearer Token Found");
            }

            _authenticationService.IsTokenValid(authToken);
        }
    }
}
