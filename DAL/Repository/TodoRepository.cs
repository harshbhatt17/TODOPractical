using DAL.Model;
using DAL.Model.DbContextTODO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private DBContextTODO context;
        public TodoRepository(DBContextTODO dBContext)
        {
            context = dBContext;
        }
        public bool AddUpdate(Tbl_ToDo objTodo)
        {
            try
            {
                if (objTodo.Id == 0)
                {
                    objTodo.CreatedDate = DateTime.Now;
                    objTodo.SerialNumber = getLastSerialNo();
                    context.Add(objTodo);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    var objDb = context.Tbl_ToDo.Where(x => x.Id == objTodo.Id).FirstOrDefault();
                    if (objDb != null)
                    {
                        objDb.Title = objTodo.Title;
                        objDb.Description = objTodo.Description;
                        objDb.IsCompleted = objTodo.IsCompleted;
                        context.Entry(objDb).State = EntityState.Modified;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteById(int Id)
        {
            if (Id > 0)
            {
                var DelObj = context.Tbl_ToDo.Where(x => x.Id == Id).FirstOrDefault();
                context.Tbl_ToDo.Remove(DelObj);
                context.SaveChanges();
                UpdateSerialnumber(Id, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Tbl_ToDo> GetAll()
        {
            var allObj = context.Tbl_ToDo.ToList();
            return allObj;
        }

        public Tbl_ToDo GetById(int Id)
        {
            return context.Tbl_ToDo.Where(x=>x.Id==Id).FirstOrDefault();
        }

        private void UpdateSerialnumber(int id, bool isDeleted = false)
        {
            //var CurrentRecord = context.Tbl_ToDo.Where(x => x.Id == id).FirstOrDefault();
            var count = 1;
            if (isDeleted)
            {
                var ListofTodo = context.Tbl_ToDo.OrderBy(x => x.Id).ToList();

                foreach (var item in ListofTodo)
                {
                    var objDb = context.Tbl_ToDo.Where(x => x.Id == item.Id).FirstOrDefault();
                    if (objDb != null)
                    {
                        objDb.SerialNumber = count;
                        context.Entry(objDb).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    count = count + 1;
                }
            }
        }

        private int getLastSerialNo()
        {
            if (context.Tbl_ToDo.Count() > 0)
            {
                var maxSerial = context.Tbl_ToDo.Max(x => x.SerialNumber);
                return maxSerial+1;
            }
            else
            {
                return 1;
            }

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
