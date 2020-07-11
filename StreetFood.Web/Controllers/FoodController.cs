using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreetFood.Domain.Models;
using StreetFood.Service.Interfaces;
using StreetFood.Web.Models;
using StreetFood.Web.Services;

namespace StreetFood.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FoodController(
            IFoodService foodService,
            IUserService userService,
            IMapper mapper)
        {
            _foodService = foodService;
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<FoodModel>> GetAllFoods()
        {
            List<Food> foods = _foodService.GetAllFoods();
            List<FoodModel> foodModels = _mapper.Map<List<FoodModel>>(foods);
            return Ok(foodModels);
        }

        [HttpGet, Route("ownfood")]
        public ActionResult<List<FoodModel>> GetMyFoods()
        {
            int userId = _userService.GetUserIdFromRequest(Request);
            if (userId > 0)
            {
                List<Food> foods = _foodService.GetAllFoods(userId);
                List<FoodModel> foodModels = _mapper.Map<List<FoodModel>>(foods);
                return Ok(foodModels);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet, Route("{id}")]
        public ActionResult<FoodModel> GetFood(int id)
        {
            Food food = _foodService.GetFood(id);
            if(food != null)
            {
                FoodModel foodModel = _mapper.Map<FoodModel>(food);
                return Ok(foodModel);
            }
            return BadRequest();
        }

        [HttpPost]
        public ActionResult AddFood(FoodModel foodModel)
        {
            int userId = _userService.GetUserIdFromRequest(Request);
            if(userId <= 0)
            {
                return Unauthorized();
            }
            Food food = _mapper.Map<Food>(foodModel);
            bool status = _foodService.AddFood(userId, food);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpPut]
        public ActionResult UpdateFood(FoodModel foodModel)
        {
            if(foodModel.Id <= 0)
            {
                return BadRequest();
            }
            int userId = _userService.GetUserIdFromRequest(Request);
            if(userId <= 0)
            {
                return Unauthorized();
            }
            bool isFoodExist = _foodService.IsFoodExist(userId, foodModel.Id);
            if (isFoodExist)
            {
                Food food = _foodService.GetFood(foodModel.Id);
                if(food.ImageUrl != foodModel.ImageUrl)
                {
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", food.ImageUrl);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                Food updatedFood = _mapper.Map<FoodModel, Food>(foodModel, food);
                bool status = _foodService.UpdateFood(updatedFood);
                if (status)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpDelete, Route("{foodId}")]
        public ActionResult DeleteFood(int foodId)
        {
            int userId = _userService.GetUserIdFromRequest(Request);
            if (userId <= 0)
            {
                return Unauthorized();
            }
            bool isFoodExist = _foodService.IsFoodExist(userId, foodId);
            if (isFoodExist)
            {
                Food food = _foodService.GetFood(foodId);
                if (food.ImageUrl != null)
                {
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", food.ImageUrl);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                bool status = _foodService.DeleteFood(foodId);
                if (status)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet, Route("country")]
        public ActionResult<List<Country>> GetCountries()
        {
            List<Country> countries = _foodService.GetAllCountries();
            return Ok(countries);
        }

        [HttpPost, Route("image")]
        public async Task<ActionResult> UploadImageAsync([FromForm] FileModel fileModel)
        {
            if (fileModel.File != null && fileModel.File.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + '_' + Path.GetFileName(fileModel.File.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileModel.File.CopyToAsync(fileStream);
                }
                return Ok(new { Url = fileName });
            }
            return BadRequest();
        }
    }
}