using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CardFileCore.Models
{
    public class IdentityContext:IdentityDbContext<User>
    {
        //public System.Data.Entity.DbSet<Book> Books { get; set; }
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        //public IdentityContext()
        //{
        //}
    }
}
