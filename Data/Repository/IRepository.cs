using Coffee.Models;

namespace Coffee.Data.Repository
{
	public interface IRepository
	{
		Task<List<CoffeeClass>> GetAllCoffees();
		Task<CoffeeClass> AddCoffees(CoffeeClass coffeeClass);
		Task DeleteCoffeesAsync(CoffeeClass coffee);
		Task<CoffeeClass> UpdateCoffeAsync(CoffeeClass coffee);
		Task<CoffeeClass> GetOneCoffeeAsync(int id);
	}
}
