using Coffee.Models;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Data
{
	public class DBCoffeeContext : DbContext
	{
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(@"Data Source=DataBase\Coffee.db");
        public DBCoffeeContext(DbContextOptions<DBCoffeeContext> options) : base(options)
        {

            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<CoffeeClass>().HasData(GetCoffees());
            base.OnModelCreating(model);
        }

        private List<CoffeeClass> GetCoffees()
        {
            return new List<CoffeeClass>
            {
                new CoffeeClass {Id = 1, Name = "Капучино", Time = 30, Tempurature = 60},
                new CoffeeClass {Id = 2, Name = "Американо", Time = 25, Tempurature = 70},
                new CoffeeClass {Id = 3, Name = "Раф", Time = 22, Tempurature = 40}
            };
        }
        public DbSet<CoffeeClass> Coffees { get; set; }
	}
}
