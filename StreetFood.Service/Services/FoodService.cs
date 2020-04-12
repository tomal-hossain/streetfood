using Microsoft.EntityFrameworkCore;
using StreetFood.Domain;
using StreetFood.Domain.Models;
using StreetFood.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreetFood.Service.Services
{
    public class FoodService : IFoodService
    {
        private readonly StreetFoodDbContext _context;

        public FoodService(StreetFoodDbContext context)
        {
            _context = context;
        }

        public List<Food> GetAllFoods()
        {
            return _context.Food.ToList();
        }

        public Food GetFood(int id)
        {
            return _context.Food
                .Where(x => x.Id == id)
                .Include(x => x.PopularIn)
                .ThenInclude(x => x.Country)
                .Include(x => x.AddedBy)
                .FirstOrDefault();
        }

        public int AddFood(int userId, Food food)
        {
            try
            {
                food.AddedById = userId;
                food.AddedAt = DateTime.Now;
                _context.SaveChanges();
                return food.Id;
            }
            catch
            {
                return -1;
            }
        }

        public bool IsFoodExist(int userId, int foodId)
        {
            Food food = _context.Food
                .Where(x => x.Id == foodId && x.AddedById == userId)
                .FirstOrDefault();
            if (food != null)
            {
                return true;
            }
            return false;
        }

        public bool UpdateFood(Food food)
        {
            try
            {
                _context.Food.Update(food);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFood(int id)
        {
            Food food = _context.Food
                .Where(x => x.Id == id)
                .FirstOrDefault();
            try
            {
                _context.Food.Remove(food);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
