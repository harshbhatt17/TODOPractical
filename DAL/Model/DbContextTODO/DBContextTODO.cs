using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model.DbContextTODO
{
    public class DBContextTODO : DbContext
    {
        public DBContextTODO(DbContextOptions<DBContextTODO> options) : base(options)
        {

        }
        public DbSet<Tbl_ToDo> Tbl_ToDo { get; set; }
    }
}
