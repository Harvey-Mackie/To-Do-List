﻿using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List_Library.Application.Contracts.Presentation;

namespace To_Do_List_Library.API.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public string UserId { get; }
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization];
        }
    }
}