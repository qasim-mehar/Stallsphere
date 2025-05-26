using Microsoft.EntityFrameworkCore;
using Stallsphere.Models.Entities;

namespace Stallsphere.Models
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base (options) { 
         
        }
        //Dbset class hai or wo bata rahi hai ke authentication.cs se info le ke
        public DbSet<Authentication> Authentications { get; set; }//ye table banaye ga
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Stall> Stalls { get; set; }
        public DbSet<Renter> Renters { get; set; }
        //Yaha pe baaki table bananay ke liye DbSet banay gay Stall and Users
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // yaha pe API configure hongi
        }
    }
    
}
