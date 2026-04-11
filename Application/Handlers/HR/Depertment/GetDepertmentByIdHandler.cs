using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.Depertments;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Depertment
{
    public class GetDepertmentByIdHandler : IRequestHandler<GetDepertmentByIdQuery, BaseResponse<DepertmentResponse>>
    {
        private readonly IGetDepartmentById _GetDepartmentById;

        public GetDepertmentByIdHandler(IGetDepartmentById GetDepartmentById)
        {
            _GetDepartmentById = GetDepartmentById;
        }

        public async Task<BaseResponse<DepertmentResponse>> Handle(
            GetDepertmentByIdQuery request,
            CancellationToken ct)
        {
            return await _GetDepartmentById.GetDepertmentById(request.Id, ct);

        }
    }
}
