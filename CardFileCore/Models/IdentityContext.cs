using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

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
