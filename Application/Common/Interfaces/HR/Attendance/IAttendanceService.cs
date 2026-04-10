using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface IAttendanceService
    {
        Task<BaseResponse<object>> CreateAttendance(Guid employeeId, CancellationToken ct);
        Task<BaseResponse<List<AttendanceResponse>>> GetByEmployee(Guid employeeId, CancellationToken ct);
        Task<BaseResponse<AttendanceResponse>> GetById(Guid id, CancellationToken ct);
        Task<BaseResponse<List<AttendanceResponse>>> GetAll(CancellationToken ct);
        Task<BaseResponse<AttendanceResponse>> UpdateAttendance(Guid id, DateTime? checkOut, CancellationToken ct);
        Task<BaseResponse<AttendanceResponse>> DeleteAttendance(Guid id, CancellationToken ct);
    }
}
