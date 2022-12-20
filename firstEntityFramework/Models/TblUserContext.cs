using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace firstEntityFramework.Models
{
    public class TblUserContext : DbContext
    {
        public TblUserContext()
        {
        }

        public TblUserContext(DbContextOptions<TblUserContext> option):base(option)
        {
            
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // configures one-to-many relationship
        //    modelBuilder.Entity<Department>()
        //        .HasOne(c => c.TblUser)
        //        .WithMany(s=>s.Department)
        //        .HasForeignKey(s=> s.TblUserId);
        //}

        public DbSet<TblUser> tblUser { get; set; }
        public DbSet<Department> department { get; set; }
    }
}
