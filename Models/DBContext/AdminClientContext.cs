using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AdminClientMVC.Models.DBContext
{
    public class AdminClientContext : DbContext
    {
        public AdminClientContext(DbContextOptions<AdminClientContext> options) : base(options) { }

        #region EF: Datase-Tabels to Models
        //------------< region: Datase-Tables to Models >------------
        public DbSet<tbl_user_types> tbl_user_types { get; set; }
        public DbSet<tbl_employees> tbl_employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<tbl_user_types>().ToTable("tbl_user_types");
            modelBuilder.Entity<tbl_employees>().ToTable("tbl_employees");
        }

        //------------</ region : Datase-Tables to Models >------------

        #endregion /EF: Datase-Tabels to Models

    }
}
