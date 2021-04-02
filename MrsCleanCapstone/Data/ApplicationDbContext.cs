using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MrsCleanCapstone.Models;
using MrsCleanCapstone.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace MrsCleanCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
