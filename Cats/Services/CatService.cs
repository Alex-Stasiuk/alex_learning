using CatsCat.Models;
using Microsoft.EntityFrameworkCore;

namespace CatsCat.Services
{
    public class CatDataServices : DbContext
    {
        public DbSet<Cat> Cats => Set<Cat>();
        public CatDataServices() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = Cats; User Id = sa; Password = 8054;");
        }
    }
    public static class CatService
    {
        static List<Cat> Cats { get; }
        
        public static List<Cat> GetAll()
        {
            CatDataServices services = new CatDataServices();
            return services.Cats.ToList();
        }

        public static Cat? Get(int id)
        {
            CatDataServices services = new CatDataServices();
            return services.Cats.FirstOrDefault(c => c.Id == id);
        }

        public static void Add(Cat cat)
        {
            CatDataServices services = new CatDataServices();
            services.Cats.Add(cat);
            services.SaveChanges();
        }

        public static void Delete(int id)
        {
            CatDataServices services = new CatDataServices();
            var cat = services.Cats.FirstOrDefault(c => c.Id == id);
            if (cat is null)
                return;

            services.Cats.Remove(cat);
            services.SaveChanges();
        }

        public static void Update(Cat cat)
        {
            CatDataServices services = new CatDataServices();
            var catdb = services.Cats.FirstOrDefault(c => c.Id == cat.Id);
            if (catdb is null)
                return;

            catdb.Name = cat.Name;
            catdb.Age = cat.Age;
            services.SaveChanges();
        }
    }
}