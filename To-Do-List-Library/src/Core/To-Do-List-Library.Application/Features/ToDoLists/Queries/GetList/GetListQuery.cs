using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetList
{
    public class GetListQuery : IRequest<GetListQueryResponse>
    {
        public Guid ToDoListId { get; set; }
    }
}
