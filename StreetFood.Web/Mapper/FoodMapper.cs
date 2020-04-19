using AutoMapper;
using StreetFood.Domain.Models;
using StreetFood.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetFood.Web.Mapper
{
    public class FoodMapper: Profile
    {
        public FoodMapper()
        {
            CreateMap<Food, FoodModel>()
                .ForMember(dest => dest.AddedByName, opt => opt.MapFrom(src => src.AddedBy.Name))
                .ForMember(dest => dest.PopularInList, opt => opt.MapFrom(src => src.PopularIn.Select(x => x.Country).ToList()));
            CreateMap<FoodModel, Food>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AddedBy, opt => opt.Ignore())
                .ForMember(dest => dest.AddedAt, opt => opt.Ignore())
                .ForMember(dest => dest.AddedById, opt => opt.Ignore())
                .ForMember(dest => dest.PopularIn, opt => opt.MapFrom(src => src.PopularInList));
            CreateMap<Country, CountryFood>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
