using Domain.Common;
using Domain.Entites.DoctorsModule;
using Domain.Entites.ServicesModule;
using System;
using System.Collections.Generic;
namespace Domain.Entites.HR
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }



        public List<Recruitment> Recruitments { get; set; } = new();
        public List<Service> Services { get; set; } = new List<Service>();
        public List<Employee> Employees { get; set; }= new List<Employee>(); 

   
    }
}
 