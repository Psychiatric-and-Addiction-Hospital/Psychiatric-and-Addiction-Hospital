using Application.Commands.Session;
using Application.Common.Interfaces.Session;
using Application.Common.Responses;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Session
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, BaseResponse<Guid>>
    {
        private readonly ISessionService _sessionService; // الواجهة التي ستنفذ المنطق

        public CreateSessionHandler(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<BaseResponse<Guid>> Handle(CreateSessionCommand request, CancellationToken ct)
        {
            // نمرر الـ Request بالكامل للخدمة لمعالجة منطق الإنشاء والتأكد من المواعيد
            return await _sessionService.CreateSessionAsync(request, ct);
        }
    }
}
