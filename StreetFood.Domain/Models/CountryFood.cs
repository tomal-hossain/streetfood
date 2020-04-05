namespace StreetFood.Domain.Models
{
    public class CountryFood
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
