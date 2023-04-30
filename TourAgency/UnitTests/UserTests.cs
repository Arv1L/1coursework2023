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
            var tour = new Tour("Country", "City", DateTime.Parse("10.07.2023"), 10, 999, 30, "Description");
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
            var tour = new Tour("Country", "City", DateTime.Parse("10.07.2023"), 10, 999, 30, "Description") { CurrentTicketsNumber = 1 };
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
            if (JsonLogic.ReadFromJson(Agency.ORDERS_PATH, out List<Order> orders))
                Agency.Instance.Orders = orders;

            int orderId = 0;
            User user = new RegisteredUser("Email", "Name", "Password");

            // Act
            bool result = user.ReturnTickets(orderId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnTickets_WithInvalidOrderId_ReturnsFalse()
        {
            if (JsonLogic.ReadFromJson(Agency.ORDERS_PATH, out List<Order> orders))
                Agency.Instance.Orders = orders;

            // Arrange
            int orderId = -1;
            User user = new RegisteredUser("Email", "Name", "Password");

            // Act
            bool result = user.ReturnTickets(orderId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}