using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TiacApp.Models;

namespace TiacApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
    }
}