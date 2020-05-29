using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HR.Api.Contracts;
using HR.UI.Contracts;
using HR.UI.Models.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.UI.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly HttpClientWrapper _clientWrapper;

        public EmployeesController(HttpClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }

        public async Task<IActionResult> Index(string firstName, string lastName)
        {
            ViewData["FirstName"] = firstName;
            ViewData["LastName"] = lastName;

            var apiResult = await _clientWrapper.GetAsync<List<EmployeeDto>>($"api/Employees?firstName={firstName}&lastName={lastName} ");
            return View(apiResult.Select(item => new EmployeeListViewModel
            {
                Id = item.Id,
                Gender = item.Gender,
                FirstName = item.FirstName,
                LastName = item.LastName,
                BirthDate = item.BirthDate,
                JobPosition = item.JobPosition,
                PersonalId = item.PersonalId,
                Status = item.Status
            }).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel model)
        {
            var employeeToCreate = new CreateEmployeeDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Status = model.Status,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                JobPosition = model.JobPosition,
                PersonalId = model.PersonalId
            };
            var response = await _clientWrapper.PostAsync("api/Employees", employeeToCreate);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", await response.Content.ReadAsStringAsync());

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _clientWrapper.GetAsync<EmployeeDto>($"api/Employees/{id}");
            return View(new DeleteEmployeeViewModel
            {
                Id = item.Id,
                Gender = item.Gender,
                FirstName = item.FirstName,
                LastName = item.LastName,
                BirthDate = item.BirthDate,
                JobPosition = item.JobPosition,
                PersonalId = item.PersonalId,
                Status = item.Status
            });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientWrapper.DeleteAsync($"api/Employees/{id}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _clientWrapper.GetAsync<EmployeeDto>($"api/Employees/{id}");
            return View(new UpdateEmployeeViewModel
            {
                Gender = item.Gender,
                FirstName = item.FirstName,
                LastName = item.LastName,
                BirthDate = item.BirthDate,
                JobPosition = item.JobPosition,
                PersonalId = item.PersonalId,
                Status = item.Status
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel model)
        {
            var employeeToUpdate = new UpdateEmployeeDto
            {
                Gender = model.Gender,
                Status = model.Status,
                BirthDate = model.BirthDate,
                LastName = model.LastName,
                PersonalId = model.PersonalId,
                JobPosition = model.JobPosition,
                FireDate = model.FireDate,
                FirstName = model.FirstName,
                IsDeleted = model.IsDeleted
            };
            await _clientWrapper.PutAsync<UpdateEmployeeDto>($"api/Employees/{model.Id}", employeeToUpdate);
            return RedirectToAction("Index");

        }
    }
}