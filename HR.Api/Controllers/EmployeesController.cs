using HR.Api.Contracts;
using HR.Api.Db;
using HR.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDao _employeeDao;

        public EmployeesController(EmployeeDao employeeDao)
        {
            _employeeDao = employeeDao;
        }

        [HttpGet]
        public async Task<List<Employee>> List([FromQuery]string firstName, [FromQuery]string lastName)
        {
            return await _employeeDao.ListAsync(firstName, lastName);
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetById(int id)
        {
            return await _employeeDao.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateEmployeeDto model)
        {
            var employee = await _employeeDao.GetByPersonalIdAsync(model.PersonalId);
            if (employee != null)
            {
                return Conflict($"Employee with personalId {model.PersonalId} already exists");
            }

            return Ok(await _employeeDao.CreateAsync(model.GetEmployee()));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateEmployeeDto model)
        {
            var employee = await _employeeDao.GetByIdAsync(id);
            if (employee is null)
            {
                return NotFound($"Employee with id { id} not found");
            }

            await _employeeDao.UpdateAsync(model.GetEmployee(id));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await _employeeDao.GetByIdAsync(id);
            if (employee is null)
            {
                return NotFound($"Employee with id {id} not found");
            }
            employee.IsDeleted = true;
            await _employeeDao.UpdateAsync(employee);
            return Ok();
        }


    }
}