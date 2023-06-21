using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Core.Entities;
using talabat.Core.Repositories;
using talabat.Core.Specifications;

namespace Talabat.Controllers
{

    public class EmployeeController : BaseApiController
    {
        private readonly IGenericRepository<Employee> _employeesRepo;

        public EmployeeController(IGenericRepository<Employee> employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Employee>>> GetEmployees()
        {
            var spec = new EmployeeWithDepartmentSpecifications();
            var employees = await _employeesRepo.GetAllWithSpecAsync(spec);
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications(id);
            var employee = await _employeesRepo.GetByEntityWithSpecAsync(spec);
            return Ok(employee);
        }
    }
}
