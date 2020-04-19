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

        public List<Food> GetAllFoods(int userId = 0)
        {
            if (userId > 0)
            {
                return _context.Food
                    .Where(x => x.AddedById == userId)
                    .Include(x => x.AddedBy)
                    .ToList();
            }
            return _context.Food
                .Include(x => x.AddedBy)
                .ToList();
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

        public bool AddFood(int userId, Food food)
        {
            try
            {
                food.AddedById = userId;
                food.AddedAt = DateTime.Now;
                _context.Food.Add(food);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
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
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Country> GetAllCountries()
        {
            return _context.Country.ToList();
        }
    }
}
