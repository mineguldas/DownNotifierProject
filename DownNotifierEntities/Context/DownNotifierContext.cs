using DownNotifierEntities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierEntities.Context
{
    public class DownNotifierContext : DbContext
    {
        public DownNotifierContext()
        {

        }
        public DownNotifierContext(DbContextOptions<DownNotifierContext> options) : base(options)
        {

        }

        #region kullanılacak tablolar

        public DbSet<User> User { get; set; }
        public DbSet<TargetApp> TargetApp { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public DbSet<Log> Log { get; set; }

        #endregion


    }

}
