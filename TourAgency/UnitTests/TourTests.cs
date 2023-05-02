using TourAgency.Models;

namespace UnitTests
{
    [TestClass]
    public class TourTests
    {
        [TestMethod]
        public void BuyTickets_WithEnoughTickets_ReturnsTrue()
        {
            // Arrange
            int tickets = 1;
            Tour tour = new Tour(0, "Country", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, 2, "Description");

            // Act
            bool result = tour.BuyTickets(tickets);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BuyTickets_WithNotEnoughTickets_ReturnsFalse()
        {
            // Arrange
            int tickets = 3;
            Tour tour = new Tour(0, "Country", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, 2, "Description");

            // Act
            bool result = tour.BuyTickets(tickets);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CompareTo_WithNonNullOther_ReturnsExpectedValue()
        {
            // Arrange
            Tour tour1 = new Tour(0, "USA", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, 2, "Description");
            Tour tour2 = new Tour(0, "Canada", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, 2, "Description");

            // Act
            int result = tour1.CompareTo(tour2);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CompareTo_WithNullOther_ReturnsExpectedValue()
        {
            // Arrange
            Tour tour1 = new Tour(0, "USA", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, 2, "Description");

            // Act
            int result = tour1.CompareTo(null);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
