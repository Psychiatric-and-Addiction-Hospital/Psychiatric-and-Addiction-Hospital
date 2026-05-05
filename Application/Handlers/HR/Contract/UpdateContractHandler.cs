using Application.Commands.HR.Contract;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Contract
{
    public class UpdateContractHandler : IRequestHandler<UpdateContractCommand, BaseResponse<ContractResponse>>
    {
        private readonly IUpdateContract _UpdateContract;
        public UpdateContractHandler(IUpdateContract UpdateContract)
        {
            _UpdateContract = UpdateContract;
        }
        public async Task<BaseResponse<ContractResponse>> Handle(UpdateContractCommand request, CancellationToken ct)
        {
            return await _UpdateContract.UpdateContractAsync(request.Id, request.EmployeeId, request.StartDate, request.EndDate, request.Terms, request.BaseSalary, ct);
        }
    }
}
