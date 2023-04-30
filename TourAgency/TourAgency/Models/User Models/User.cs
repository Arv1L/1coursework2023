using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using TourAgency.Models;

namespace TourAgency
{
    public abstract class User : IPerson
    {
        #region Static properties
        public static int Counter { get; set; } = 0;
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
        #endregion

        public User(string email, string name, string password)
        {
            Id = Counter++;
            Email = email;
            Name = name;
            Password = password;
        }

        [JsonConstructor]
        public User(int id, string email, string name, string password, UserStatus status)
        {
            Id = id;
            Email = email;
            Name = name;
            Password = password;
            Status = status;

            if (Counter <= id)
                Counter = id + 1;
        }

        public virtual bool OrderTour(Tour tour, int ticketsNumber, out Order order)
        {
            order = new Order();

            if (tour.BuyTickets(ticketsNumber))
            {
                tour.CurrentTicketsNumber -= ticketsNumber;

                OrderStatus status = OrderStatus.Actual;

                order = new Order(ticketsNumber, status, this, tour);

                return true;
            }

            return false;
        }

        public virtual bool ReturnTickets(int orderId, List<Order> orders, out int index, out int ticketsReturned)
        {
            index = -1;
            ticketsReturned = 0;

            Order? order = orders.Where(o => o.Id == orderId).First();

            if (order != null && order.CancelOrder())
            {
                index = orders.IndexOf(order);

                ticketsReturned = order.TicketNumber;

                return true;
            }

            return false;
        }
    }
}
