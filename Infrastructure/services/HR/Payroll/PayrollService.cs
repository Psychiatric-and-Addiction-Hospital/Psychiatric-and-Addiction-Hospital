using Application.Common.Interfaces.HR.Payroll;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Payroll
{
    public class PayrollService : IPayrollService
    {
        private readonly AddIdentityDbContext _context;

        public PayrollService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PayrollResponse>> CreatePayroll(Guid employeeId, DateTime paymentDate, decimal grossPay, decimal deductions, decimal overtimeRate, decimal overSefit, CancellationToken ct)
        {
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeId, ct);
            if (!employeeExists)
                return ResponseFactory.Fail<PayrollResponse>("Employee not found");

            var payroll = new Domain.Entites.HR.Payroll
            {
                EmployeeId = employeeId,
                PaymentDate = paymentDate,
                GrossPay = grossPay,
                Deductions = deductions,
                OvertimeSefite = overtimeRate,
                OverSefit = overSefit
            };

            await _context.Payrolls.AddAsync(payroll, ct);
            await _context.SaveChangesAsync(ct);

            var res = new PayrollResponse
            {
                Id = payroll.Id,
                EmployeeId = payroll.EmployeeId,
                PaymentDate = payroll.PaymentDate,
                GrossPay = payroll.GrossPay,
                Deductions = payroll.Deductions,
                OvertimeSefite = payroll.OvertimeSefite,
                OverSefit = payroll.OverSefit,
                NetPay = payroll.NetPay
            };

            return ResponseFactory.Success(res, "Payroll created");
        }

        public async Task<BaseResponse<PayrollResponse>> GetPayroll(Guid id, CancellationToken ct)
        {
            var p = await _context.Payrolls.FindAsync(new object[] { id }, ct);
            if (p == null)
                return ResponseFactory.Fail<PayrollResponse>("Payroll not found");

            var res = new PayrollResponse
            {
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                PaymentDate = p.PaymentDate,
                GrossPay = p.GrossPay,
                Deductions = p.Deductions,
                OvertimeSefite = p.OvertimeSefite,
                OverSefit = p.OverSefit,
                NetPay = p.NetPay
            };

            return ResponseFactory.Success(res, "Payroll retrieved");
        }

        public async Task<BaseResponse<List<PayrollResponse>>> GetAll(CancellationToken ct)
        {
            var records = await _context.Payrolls
                .Select(p => new PayrollResponse
                {
                    Id = p.Id,
                    EmployeeId = p.EmployeeId,
                    PaymentDate = p.PaymentDate,
                    GrossPay = p.GrossPay,
                    Deductions = p.Deductions,
                    OvertimeSefite = p.OvertimeSefite,
                    OverSefit = p.OverSefit,
                    NetPay = p.NetPay
                }).ToListAsync(ct);

            return ResponseFactory.Success(records, records.Any() ? "Payroll records retrieved" : "No payroll records found");
        }

        public async Task<BaseResponse<PayrollResponse>> UpdatePayroll(Guid id, decimal grossPay, decimal deductions, decimal overtimeRate, decimal overSefit, CancellationToken ct)
        {
            var p = await _context.Payrolls.FindAsync(new object[] { id }, ct);
            if (p == null)
                return ResponseFactory.Fail<PayrollResponse>("Payroll not found");

            p.GrossPay = grossPay;
            p.Deductions = deductions;
            p.OvertimeSefite = overtimeRate;
            p.OverSefit = overSefit;

            _context.Payrolls.Update(p);
            await _context.SaveChangesAsync(ct);

            var res = new PayrollResponse
            {
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                PaymentDate = p.PaymentDate,
                GrossPay = p.GrossPay,
                Deductions = p.Deductions,
                OvertimeSefite = p.OvertimeSefite,
                OverSefit = p.OverSefit,
                NetPay = p.NetPay
            };

            return ResponseFactory.Success(res, "Payroll updated");
        }

        public async Task<BaseResponse<PayrollResponse>> DeletePayroll(Guid id, CancellationToken ct)
        {
            var p = await _context.Payrolls.FindAsync(new object[] { id }, ct);
            if (p == null)
                return ResponseFactory.Fail<PayrollResponse>("Payroll not found");

            _context.Payrolls.Remove(p);
            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success<PayrollResponse>(null, "Payroll deleted");
        }
    }
}
