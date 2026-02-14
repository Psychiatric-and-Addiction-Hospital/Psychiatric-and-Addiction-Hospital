using Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Admin
{
    public record GetPendingDoctorsQuery():IRequest<List<DoctorApplication>>;

}
