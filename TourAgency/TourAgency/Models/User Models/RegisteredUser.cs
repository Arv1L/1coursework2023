using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency
{
    public class RegisteredUser : User
    {
        public RegisteredUser(string email, string name, string password) : base(email, name, password)
        {
            Status= UserStatus.RegisteredUser;
        }

        public override bool OrderTour(Tour tour, int ticketsNumber, out Order order)
        {
            return base.OrderTour(tour, ticketsNumber, out order);
        }

        public override bool ReturnTickets(int orderId, List<Order> orders, out int index, out int ticketsReturned)
        {
            return base.ReturnTickets(orderId, orders, out index, out ticketsReturned);
        }
    }
}
