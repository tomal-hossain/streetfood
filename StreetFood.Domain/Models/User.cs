using System.Collections.Generic;

namespace StreetFood.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public string Pasword { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
