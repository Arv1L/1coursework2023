using System;
using System.Text.Json.Serialization;

namespace TourAgency.Models
{
    public class Order : ILocation
    {
        #region Static properties
        public static int Counter { get; set; } = 0;
        #endregion

        #region Properties
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public float Price { get; set; }
        public int TicketNumber { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public OrderStatus Status { get; set; }
        #endregion

        public Order()
        {
            Id = -1;
            UserId = -1;
            TourId = -1;
            UserName = "Anon";
            City = "?";
            Price = 0F;
            TicketNumber = 0;
            Date = DateTime.Now;
            Duration = 0;
            Status = OrderStatus.Actual;
        }

        public Order(int ticketNumber, OrderStatus status, User user, Tour tour)
        {
            Id = Counter++;
            UserId = user.Id;
            TourId = tour.Id;
            UserName = user.Name;
            City = tour.City;
            Price = tour.Price * ticketNumber;
            TicketNumber = ticketNumber;
            Date = tour.Date;
            Duration = tour.Duration;
            Status = status;
        }

        [JsonConstructor]
        public Order(int id, int userId, int tourId, string userName, string city, float price, int ticketNumber, DateTime date, int duration, OrderStatus status)
        {
            Id = id;
            UserId = userId; 
            TourId = tourId;
            UserName = userName;
            City = city;
            Price = price;
            TicketNumber = ticketNumber;
            Date = date;
            Duration = duration;
            Status = status;

            if (Counter <= id)
                Counter = id + 1;
        }

        public bool CancelOrder()
        {
            if (Status == OrderStatus.Actual)
            {
                Status = OrderStatus.Canceled;
                return true;
            }

            return false;
        }

        public void CheckStatus()
        {
            DateTime currentDate = DateTime.Now;

            if (Status != OrderStatus.Canceled)
            {
                if (currentDate < Date)
                    Status = OrderStatus.Actual;
                else if (currentDate >= Date && currentDate < Date.AddDays(Duration))
                    Status = OrderStatus.Current;
                else
                    Status = OrderStatus.Happened;
            }
        }
    }
}
