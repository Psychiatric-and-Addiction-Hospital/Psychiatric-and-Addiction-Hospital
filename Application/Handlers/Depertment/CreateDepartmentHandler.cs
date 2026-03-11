using Application.Commands.Department;
using Application.Common.Interfaces.Depertment;
using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Depertment
{
    public class CreateDepartmentHandler 
        : IRequestHandler<CreateDepartmentCommand, BaseResponse<DepertmentResponse>>
    {
        private readonly ICreateDepartment _createService;

        public CreateDepartmentHandler(ICreateDepartment createService)
        {
            _createService = createService;
        }

        public async Task<BaseResponse<DepertmentResponse>> Handle(CreateDepartmentCommand request, CancellationToken ct)
        {
            return await _createService.CreateAsync(request.Name, request.Description, ct);
        }
    }

}
