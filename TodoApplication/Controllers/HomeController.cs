using DAL.Model;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TodoApplication.Models;

namespace TodoApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITodoRepository _repository;

        public HomeController(ILogger<HomeController> logger, ITodoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            //string url = "http://localhost:58414/api/TodoAPI/GetAll"; // sample url
            //using (HttpClient client = new HttpClient())
            //{
            //    var APIres = await client.GetStringAsync(url);
            //    var res = JsonConvert.DeserializeObject<List<Tbl_ToDo>>(APIres);
            //}
            var allTodo = _repository.GetAll();
            return View(allTodo);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var DelRes= _repository.DeleteById(id);
            return Json(new { Success = DelRes });
        }



        public async Task<IActionResult> AddUpdateTodo(int id=0)
        {
            Tbl_ToDo objTodo = new Tbl_ToDo();
            if (id > 0)
            {
                objTodo = _repository.GetById(id);
            }
            return View(objTodo);
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateTodo(Tbl_ToDo tbl_To)
        {
            if (ModelState.IsValid)
            {
                if (tbl_To != null)
                {
                    var result = _repository.AddUpdate(tbl_To);
                    return RedirectToAction("Index");
                }
            }
            return View(tbl_To);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
