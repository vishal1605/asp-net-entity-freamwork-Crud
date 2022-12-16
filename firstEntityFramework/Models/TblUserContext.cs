using Microsoft.EntityFrameworkCore;

namespace firstEntityFramework.Models
{
    public class TblUserContext : DbContext
    {
        public TblUserContext(DbContextOptions<TblUserContext> option):base(option)
        {
            
        }

        public DbSet<TblUser> tblUser { get; set; }
    }
}
