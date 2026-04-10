using Application.Common.Interfaces.Admin;
using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.Depertment;
using Application.Queries.HR;
using Domain.Entites.DoctorsModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Depertment
{
    public class GetAllDepertmentHandler : IRequestHandler<GetDepertmentQuery, BaseResponse<List<DepertmentResponse>>>
    {

        private readonly IGetDepertments _GetDepertment;
        public GetAllDepertmentHandler(IGetDepertments getDepertment)
        {
            _GetDepertment = getDepertment;
        }

        public async Task<BaseResponse<List<DepertmentResponse>>> Handle(GetDepertmentQuery request, CancellationToken ct)
        {
            return await _GetDepertment.GetAllDepertment(ct);
        }
    }
}
