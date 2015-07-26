using CIT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT.DAL
{
    public class CITDataContext : DbContext
    {
        public CITDataContext()
            : base("CITDataContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<UserInfo> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
