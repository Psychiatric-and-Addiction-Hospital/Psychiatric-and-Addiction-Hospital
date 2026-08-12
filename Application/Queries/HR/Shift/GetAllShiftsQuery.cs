using Application.Common.Responses;
using Application.DTOS.Request.HR.Shift;
using Application.DTOS.Responses.HR.Shift;
using MediatR;


namespace Application.Queries.HR.Shift
{
    public record GetAllShiftsQuery(ShiftListRequest request)
     : IRequest<BaseResponse<PagedResponse<ShiftResponse>>>;
}
