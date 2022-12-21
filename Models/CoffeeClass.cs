using System.ComponentModel.DataAnnotations;
namespace Coffee.Models
{
    public class CoffeeClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(20, 90)]
        [Required]
        public double Tempurature { get; set; }
        [Range(20, 180)]
        [Required]
        public int Time { get; set; }
    }
}
