using Coffee.Models;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Data.Repository
{
	public class SQLRpostitory
	{
		private readonly DBCoffeeContext _context;

		/*public SQLRpostitory(DBCoffeeContext context)
		{
			_context = context;
		}

		public void AddCoffees(CoffeeClass coffeeClass)
		{
			_context.Coffees.Add(coffeeClass);
			_context.SaveChanges();
		}

		public IEnumerable<CoffeeClass> GetAllCoffees()
		{
			return _context.Coffees;
		}

		public void RemoveCoffee(int id)
		{
			var deletedItem = _context.Coffees.Find(id);
			if(deletedItem != null)
			{
				_context.Coffees.Remove(deletedItem);
			}
			_context.SaveChanges();
		}
		public CoffeeClass GetOneCoffee(int id)
        {
			return _context.Coffees.Find(id);
        }*/
		public SQLRpostitory(DBCoffeeContext context)
		{
			_context = context;
		}
		public async Task<List<CoffeeClass>> GetCoffeesAsync()
        {
			return await _context.Coffees.ToListAsync();
        }

		public async Task<CoffeeClass> AddCoffees(CoffeeClass coffee)
        {
            try
            {
				_context.Coffees.Add(coffee);
				await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
			return coffee;
        }
		public async Task DeleteCoffeesAsync(CoffeeClass coffee)
        {
            try
            {
				_context.Coffees.Remove(coffee);
				await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
		public async Task<CoffeeClass> UpdateCoffeAsync(CoffeeClass coffee)
		{
			try
			{
				var coffeeTemp = _context.Coffees.FirstOrDefault(p => p.Id == coffee.Id);
				if(coffeeTemp != null)
				{
					_context.Update(coffee);
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception)
			{

				throw;
			}
			return coffee;
		}
		public CoffeeClass GetOneCoffeeAsync(int id)
        {
			return _context.Coffees.Find(id);
		}
	}
}
