using Application.Common.Responses;
using System.Threading;
using System.Threading.Tasks;
using vaildEmployee = Domain.Entites.HR.Employee;
using VaildAttendance=Domain.Entites.HR.Attendance;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface IAttendanceValidation
    {
        Task<BaseResponse<vaildEmployee>> ValidateCheckInAsync(string appUserId,string qrToken,CancellationToken ct);

        Task<BaseResponse<VaildAttendance>> ValidateCheckOutAsync(string appUserId,string qrToken,CancellationToken ct);
    }
}
