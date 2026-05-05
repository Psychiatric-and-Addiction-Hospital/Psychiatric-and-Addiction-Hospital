using Application.Commands.HR.Contract;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Handlers.HR.Contract
{
    public class CreateContractHandler : IRequestHandler<CreateContractCommand, BaseResponse<ContractResponse>>
    {
        private readonly ICreateContract _CreateContract;
        public CreateContractHandler(ICreateContract createContract)
        {
            _CreateContract = createContract;
        }
       

        public async Task<BaseResponse<ContractResponse>> Handle(CreateContractCommand request, CancellationToken ct)
        {
            return await _CreateContract.CreateContractAsync(request.EmployeeId,request.StartDate, request.EndDate, request.Terms, request.BaseSalary, ct);
        }
    }
}
