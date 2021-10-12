using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace To_Do_List_Library.Application.Features.ToDoLists.Queries.GetAllLists
{
    public class GetAllListsQuery : IRequest<List<GetAllListsQueryResponse>>
    {
        public Guid UserId { get; set; }
    }
}
