using Microsoft.EntityFrameworkCore;
using MyInfoDAL.DataModel;
using MyInfoDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyInfoDAL.DataModel
{
    public interface IMyInfoContext : IMyInfoDbContext
    {
        public DbSet<NewTable> NewTable { get; set; }
    }
}
