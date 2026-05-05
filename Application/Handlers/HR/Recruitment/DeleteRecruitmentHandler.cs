using Application.Commands.HR.Recruitment;
using Application.Common.Interfaces.HR.Recruitment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Recruitment
{
    public class DeleteRecruitmentHandler: IRequestHandler<DeleteRecruitmentCommand, BaseResponse<RecruitmentResponse>>
    {
        private readonly IDeleteRecruitment _deleteRecruitment;
        public DeleteRecruitmentHandler(IDeleteRecruitment deleteRecruitment)
        {
            _deleteRecruitment = deleteRecruitment;
        }

        public async Task<BaseResponse<RecruitmentResponse>> Handle(DeleteRecruitmentCommand request, CancellationToken ct)
        {
            return await _deleteRecruitment.DeleteRecruitmentAsync(request.RecruitmentId, ct);
        }
    }
}
