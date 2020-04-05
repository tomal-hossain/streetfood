using System;
using System.Collections.Generic;

namespace StreetFood.Domain.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public ICollection<CountryFood> PopularIn { get; set; }
        public DateTime AddedAt { get; set; }
        public int AddedById { get; set; }
        public User AddedBy { get; set; }
    }
}
