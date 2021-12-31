using APILayer.DbContextTODO;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILayer.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private DBContextTODO todoDB;
        //TodoRepository(DBContextTODO dBContext)
        //{
        //    todoDB = dBContext;
        //}
        public bool AddUpdate(Tbl_ToDo objTodo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Tbl_ToDo> GetAll()
        {
            var allObj = todoDB.Tbl_ToDo.ToList();
            return allObj;
        }

        public Tbl_ToDo GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
    public interface ITodoRepository
    {
        public List<Tbl_ToDo> GetAll();
        public Tbl_ToDo GetById(int Id);
        public bool AddUpdate(Tbl_ToDo objTodo);
        public bool DeleteById(int Id);
    }
}
