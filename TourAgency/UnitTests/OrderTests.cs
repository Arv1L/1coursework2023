using TourAgency.Models;

namespace UnitTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void CancelOrder_WithNonCanceledStatus_ReturnsTrueAndUpdatesStatus()
        {
            // Arrange
            Order order = new Order();
            order.Status = OrderStatus.Actual;

            // Act
            bool result = order.CancelOrder();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(OrderStatus.Canceled, order.Status);
        }

        [TestMethod]
        public void CancelOrder_WithCanceledStatus_ReturnsFalseAndDoesNotUpdateStatus()
        {
            // Arrange
            Order order = new Order();
            order.Status = OrderStatus.Canceled;

            // Act
            bool result = order.CancelOrder();

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(OrderStatus.Canceled, order.Status);
        }

        [TestMethod]
        public void CheckStatus_WithNonCanceledStatusAndCurrentDateBeforeDate_UpdatesStatusToActual()
        {
            // Arrange
            Order order = new Order();
            order.Status = OrderStatus.Actual;
            order.Date = DateTime.Now.AddDays(1);

            // Act
            order.CheckStatus();

            // Assert
            Assert.AreEqual(OrderStatus.Actual, order.Status);
        }

        [TestMethod]
        public void CheckStatus_WithNonCanceledStatusAndCurrentDateBetweenDateAndDatePlusDuration_UpdatesStatusToCurrent()
        {
            // Arrange
            Order order = new Order();
            order.Status = OrderStatus.Actual;
            order.Date = DateTime.Now.AddDays(-1);
            order.Duration = 2;

            // Act
            order.CheckStatus();

            // Assert
            Assert.AreEqual(OrderStatus.Current, order.Status);
        }

        [TestMethod]
        public void CheckStatus_WithNonCanceledStatusAndCurrentDateAfterDatePlusDuration_UpdatesStatusToHappened()
        {
            // Arrange
            Order order = new Order();
            order.Status = OrderStatus.Actual;
            order.Date = DateTime.Now.AddDays(-2);
            order.Duration = 1;

            // Act
            order.CheckStatus();

            // Assert
            Assert.AreEqual(OrderStatus.Happened, order.Status);
        }

        [TestMethod]
        public void CheckStatus_WithCanceledStatus_DoesNotUpdateStatus()
        {
            // Arrange
            Order order = new Order();
            order.Status = OrderStatus.Canceled;

            // Act
            order.CheckStatus();

            // Assert
            Assert.AreEqual(OrderStatus.Canceled, order.Status);
        }
    }
}
