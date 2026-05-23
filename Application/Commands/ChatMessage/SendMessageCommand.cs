using Application.Common.Responses;
using Application.DTOS.Responses.ChatMessage;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ChatMessage
{
    public record SendMessageCommand(string SenderId, string ReceiverId, string Message, MessageType Type, string? MediaUrl)
       : IRequest<BaseResponse<SendMessageResponse>>;

}
