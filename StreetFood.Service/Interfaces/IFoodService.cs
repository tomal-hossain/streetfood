using StreetFood.Domain.Models;
using System.Collections.Generic;

namespace StreetFood.Service.Interfaces
{
    public interface IFoodService
    {
        public List<Food> GetAllFoods();
        public Food GetFood(int id);
        public int AddFood(int userId, Food food);
        public bool IsFoodExist(int userId, int foodId);
        public bool UpdateFood(Food food);
        public bool DeleteFood(int id);
    }
}
