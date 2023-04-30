using System;
using System.Text.Json.Serialization;

namespace TourAgency.Models
{
    public class Tour : IComparable<Tour>, ILocation
    {
        #region Static properties
        public static int Counter { get; set; } = 0;
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public float Price { get; set; }
        public TourStatus Status { get; set; }
        public int MaxTicketsNumber { get; set; }
        public int CurrentTicketsNumber { get; set; }
        public string Description { get; set; } = null!;
        #endregion

        public Tour(string country, string city, DateTime date, int duration, float price, TourStatus status, int ticketsNumber, string description)
        {
            Id = Counter++;
            Country = country;
            City = city;
            Date = date;
            Duration = duration;
            Price = price;
            Status = status;
            MaxTicketsNumber = CurrentTicketsNumber = ticketsNumber;
            Description = description;
        }

        [JsonConstructor]
        public Tour(int id, string country, string city, DateTime date, int duration, float price, TourStatus status, int maxTicketsNumber, int currentTicketsNumber, string description)
        {
            Id = id;
            Country = country;
            City = city;
            Date = date;
            Duration = duration;
            Price = price;
            Status = status;
            MaxTicketsNumber = maxTicketsNumber;
            CurrentTicketsNumber = currentTicketsNumber;
            Description = description;

            if (Counter <= id)
                Counter = id + 1;
        }

        public bool BuyTickets(int tickets) => CurrentTicketsNumber >= tickets;

        public int CompareTo(Tour? other)
        {
            return Id.CompareTo(other?.Id);
        }
    }
}
