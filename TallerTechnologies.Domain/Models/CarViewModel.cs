using AutoMapper;
//using TallerTechnologies.Domain.Mappings;

namespace TallerTechnologies.Domain.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Doors { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
    }
}