using System;

namespace TourAgency.Models
{
    public interface ILocation
    {
        public string City { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public float Price { get; set; }
    }
}
