using KHP.Domain.Model;
using KHPPractice.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace KHPPractice.MVC.Data.DataAccess
{
    public class KHPDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=KHPDataBase;Integrated Security=True");
        }

        public DbSet<Catagory>? Catagories { get; set; }
        public DbSet<Article>? Articles { get; set; }
    }
}
