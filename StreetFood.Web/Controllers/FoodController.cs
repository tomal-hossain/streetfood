using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreetFood.Domain.Models;
using StreetFood.Service.Interfaces;
using StreetFood.Web.Models;
using StreetFood.Web.Services;

namespace StreetFood.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        public ActionResult<List<FoodModel>> GetAllFoods()
        {
            List<Food> foods = _foodService.GetAllFoods();
            List<FoodModel> foodModels = _mapper.Map<List<FoodModel>>(foods);
            return Ok(foodModels);
        }

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
            int foodId = _foodService.AddFood(userId, food);
            if (foodId > 0)
            {
                return Ok(new { Id = foodId });
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
                Food food = _mapper.Map<Food>(foodModel);
                bool status = _foodService.UpdateFood(food);
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
                bool status = _foodService.DeleteFood(foodId);
                if (status)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}