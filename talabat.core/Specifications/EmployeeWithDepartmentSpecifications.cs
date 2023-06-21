using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;

namespace talabat.Core.Specifications
{
    public class EmployeeWithDepartmentSpecifications : BaseSpecification<Employee>
    {
        public EmployeeWithDepartmentSpecifications()
        {
            Includes.Add(E => E.Department);
        }
        public EmployeeWithDepartmentSpecifications(int id): base (E => E.Id== id)
        {
            Includes.Add(E => E.Department);
        }
    }
}