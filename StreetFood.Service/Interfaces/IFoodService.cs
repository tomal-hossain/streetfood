using StreetFood.Domain.Models;
using System.Collections.Generic;

namespace StreetFood.Service.Interfaces
{
    public interface IFoodService
    {
        List<Food> GetAllFoods(int userId = 0);
        Food GetFood(int id);
        bool AddFood(int userId, Food food);
        bool IsFoodExist(int userId, int foodId);
        bool UpdateFood(Food food);
        bool DeleteFood(int id);
        List<Country> GetAllCountries();
    }
}
