using Application.Common.Responses;
using Application.DTOS.Responses.ChatMessage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ChatMessage
{
    public record GetConversationQuery(string UserId1, string UserId2, int PageNumber, int PageSize)
        : IRequest<BaseResponse<List<ChatMessageResponse>>>;

}
