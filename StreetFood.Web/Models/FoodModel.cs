using StreetFood.Domain.Models;
using System;
using System.Collections.Generic;

namespace StreetFood.Web.Models
{
    public class FoodModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public ICollection<Country> PopularInList { get; set; }
        public DateTime? AddedAt { get; set; }
        public string AddedByName { get; set; }
    }
}
