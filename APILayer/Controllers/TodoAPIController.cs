using APILayer.Repository;
using DAL.Model;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoAPIController : ControllerBase
    {
        private ITodoRepository todoRepository;
        public TodoAPIController(ITodoRepository _repository)
        {
            todoRepository = _repository;
        }
        public IEnumerable<Tbl_ToDo> GetAll()
        {
            try
            {
                return todoRepository.GetAll();
            }
            catch (Exception ex)
            {
                return new List<Tbl_ToDo>();
            }
        }
    }
}
