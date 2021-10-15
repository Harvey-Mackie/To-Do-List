using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_Library.Application.Contracts.Presentation
{
    public interface ILoggedInUserService
    {
        public string UserId { get; }
    }
}
