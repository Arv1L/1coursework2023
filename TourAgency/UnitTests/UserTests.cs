using TourAgency;
using TourAgency.Models;

namespace UnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void OrderTour_WhenTourHasEnoughTickets_ReturnsTrue()
        {
            // Arrange
            var tour = new Tour("Country", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, "Description");
            var user = new RegisteredUser("Email", "Name", "Password");
            var ticketsNumber = 2;
            Order order;

            // Act
            var result = user.OrderTour(tour, ticketsNumber, out order);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OrderTour_WhenTourDoesNotHaveEnoughTickets_ReturnsFalse()
        {
            // Arrange
            var tour = new Tour("Country", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, "Description") { CurrentTicketsNumber = 1 };
            var user = new RegisteredUser("Email", "Name", "Password");
            var ticketsNumber = 2;
            Order order;

            // Act
            var result = user.OrderTour(tour, ticketsNumber, out order);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnTickets_WithValidOrderId_ReturnsTrue()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var tour = new Tour("France", "Paris", DateTime.Now.AddDays(7), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love");
            User user = new RegisteredUser("Email", "Name", "Password");
            var order = new Order(2, OrderStatus.Actual, user, tour);
            travelAgency.Tours.Add(tour);
            travelAgency.Orders.Add(order);
            int orderId = order.Id;

            // Act
            bool result = user.ReturnTickets(orderId, travelAgency.Orders, out int index, out int ticketsReturned);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnTickets_WithInvalidOrderId_ReturnsFalse()
        {
            //// Arrange
            var travelAgency = Agency.Instance;
            var tour = new Tour("France", "Paris", DateTime.Now.AddDays(7), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love");
            User user = new RegisteredUser("Email", "Name", "Password");
            var order = new Order(2, OrderStatus.Actual, user, tour);
            travelAgency.Tours.Add(tour);
            travelAgency.Orders.Add(order);
            int orderId = order.Id + 1;

            // Act
            bool result = user.ReturnTickets(orderId, travelAgency.Orders, out int index, out int ticketsReturned);

            // Assert
            //Assert.IsFalse(result);
            Assert.AreEqual(-1, index);
            Assert.AreEqual(0, ticketsReturned);
        }
    }
}