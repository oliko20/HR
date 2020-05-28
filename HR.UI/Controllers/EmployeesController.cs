using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.UI.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            var result = client.GetAsync("api/Employee");
            return View();
        }

        public IActionResult Create()
        {
            throw new System.NotImplementedException();
        }

        public IActionResult Delete()
        {
            throw new System.NotImplementedException();
        }

        public IActionResult Edit()
        {
            throw new System.NotImplementedException();
        }

        public IActionResult Update()
        {
            throw new System.NotImplementedException();
        }
    }
}