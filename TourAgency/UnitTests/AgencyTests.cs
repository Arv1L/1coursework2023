using TourAgency;
using TourAgency.Models;

namespace UnitTests
{
    [TestClass]
    public class AgencyTests
    {
        [TestMethod]
        public void AddTour_WithValidParameters_AddsTourToTours()
        {
            // Arrange
            string country = "USA";
            string city = "New York";
            DateTime date = DateTime.Now;
            int duration = 1;
            float price = 100;
            int ticketsNumber = 10;
            string description = "Test tour";

            // Act
            Agency.Instance.AddTour(country, city, date, duration, price, ticketsNumber, description);
            List<Tour> tours = Agency.Instance.Tours;
            Tour tour = tours.FirstOrDefault(t => t.City == city && t.Date == date && t.Duration == duration)!;

            // Assert
            Assert.IsNotNull(tour);
        }

        [TestMethod]
        public void LoginUser_WithValidEmailAndPassword_ReturnsTrueAndSetsOutParameters()
        {
            // Arrange
            Agency.Instance.ReadUsers();
            string email = "text@gmail.com";
            string password = "1q2w3e4r";

            // Act
            bool result = Agency.Instance.LoginUser(email, password, out string message, out UserStatus status, out string name);
            User? user = Agency.Instance.CurrentUser;

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(user);
            Assert.AreEqual("Ласкаво просимо!", message);
            Assert.AreEqual(user.Status, status);
            Assert.AreEqual(user.Name, name);
        }

        [TestMethod]
        public void LoginUser_WithInvalidEmail_ReturnsFalseAndSetsOutParameters()
        {
            // Arrange
            Agency.Instance.ReadUsers();
            string email = "invalid@example.com";
            string password = "password";

            // Act
            bool result = Agency.Instance.LoginUser(email, password, out string message, out UserStatus status, out string name);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Такого акаунту не існує!", message);
        }

        [TestMethod]
        public void LoginUser_WithInvalidPassword_ReturnsFalseAndSetsOutParameters()
        {
            // Arrange
            Agency.Instance.ReadUsers();
            string email = "text@gmail.com";
            string password = "invalid";

            // Act
            bool result = Agency.Instance.LoginUser(email, password, out string message, out UserStatus status, out string name);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Пароль не правильний!", message);
        }

        [TestMethod]
        public void AddUser_WithValidParameters_AddsUserToUsers()
        {
            // Arrange
            int counter = Agency.Instance.Users.Count;
            string email = $"test{counter}@example.com";
            string name = "Test User";
            string password = "password";

            // Act
            bool result = Agency.Instance.AddUser(email, name, password);
            User? user = Agency.Instance.Users.FirstOrDefault(u => u.Email == email);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(password, user.Password);
        }

        [TestMethod]
        public void AddUser_WithInvalidParameters_NotAddsUserToUsers()
        {
            // Arrange
            string email = $"test@example.com";
            string name = "Test User";
            string password = "password";

            // Act
            bool result = Agency.Instance.AddUser(email, name, password);
            User? user = Agency.Instance.Users.FirstOrDefault(u => u.Email == email);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(password, user.Password);
        }

        [TestMethod]
        public void OrderATour_UserNull_ReturnsErrorMessage()
        {
            // Arrange
            Agency.Instance.CurrentTour = new Tour("Country", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, "Description");
            Agency.Instance.CurrentUser = null;

            // Act
            string result = Agency.Instance.OrderATour(1);

            // Assert
            Assert.AreEqual("Ви не увійшли в акаунт", result);
        }

        [TestMethod]
        public void OrderATour_TourNull_ReturnsErrorMessage()
        {
            // Arrange
            Agency.Instance.CurrentUser = new RegisteredUser("text@gmail.com", "Name Surname", "1q2w3e4r");
            Agency.Instance.CurrentTour = null;

            // Act
            string result = Agency.Instance.OrderATour(1);

            // Assert
            Assert.AreEqual("Ви не обрали тур", result);
        }

        [TestMethod]
        public void OrderATour_OrderTourReturnsTrue_AddsOrderAndReturnsSuccessMessage()
        {
            // Arrange

            int counter = Agency.Instance.Orders.Count;
            User user = new RegisteredUser("text@gmail.com", "Name Surname", "1q2w3e4r");
            Tour tour = new Tour($"Country{counter}", "City", DateTime.Parse("10.07.2023").AddDays(counter), 10, 999, TourStatus.Actual, 30, "Description");
            Agency.Instance.CurrentUser = user;
            Agency.Instance.CurrentTour = tour;
            int actualOrderNumber = Agency.Instance.Orders.Count;

            // Act
            int ticketNumber = 1;
            string result = Agency.Instance.OrderATour(ticketNumber);
            List<Order> orders = Agency.Instance.Orders;
            Order? order = orders[^1];

            // Assert
            Assert.IsTrue(Agency.Instance.Orders.Count == actualOrderNumber + 1);
            Assert.IsNotNull(order);
            Assert.AreEqual($"Номер замовлення: {order.Id}\n" +
                            $"Тур: {tour.City}, {tour.Country}\n" +
                            $"Ціна: {tour.Price}\n" +
                            $"Куплено квитків: {ticketNumber}\n" +
                            $"Дата відправлення: {tour.Date.ToShortDateString()}\n" +
                            $"Тривалість туру: {tour.Duration}\n" +
                            $"Статус туру: {OrderStatus.Actual}\n" +
                            $"\n{user.Email}Замовлення оформлене", result);
        }

        [TestMethod]
        public void OrderATour_OrderTourReturnsFalse_ReturnsErrorMessage()
        {
            // Arrange
            User user = new RegisteredUser("text@gmail.com", "Name Surname", "1q2w3e4r");
            Tour tour = new Tour("Country", "City", DateTime.Parse("10.07.2023"), 10, 999, TourStatus.Actual, 30, "Description") { CurrentTicketsNumber = 5 };
            Agency.Instance.CurrentUser = user;
            Agency.Instance.CurrentTour = tour;
            int actualOrderNumber = Agency.Instance.Orders.Count;

            // Act
            string result = Agency.Instance.OrderATour(10);

            // Assert
            Assert.IsTrue(Agency.Instance.Orders.Count == actualOrderNumber);
            Assert.AreEqual("Помилка бронювання туру", result);
        }

        [TestMethod]
        public void SaveUsers_SuccessfulSave_ReturnsTrue()
        {
            // Arrange
            string path = Agency.USERS_PATH;

            // Act
            bool result = Agency.Instance.SaveUsers();

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(path));
        }

        [TestMethod]
        public void SaveTours_SuccessfulSave_ReturnsTrue()
        {
            // Arrange
            string path = Agency.TOURS_PATH;

            // Act
            bool result = Agency.Instance.SaveTours();

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(path));
        }

        [TestMethod]
        public void SaveOrders_SuccessfulSave_ReturnsTrue()
        {
            // Arrange;
            string path = Agency.ORDERS_PATH;

            // Act
            bool result = Agency.Instance.SaveOrders();

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(path));
        }

        [TestMethod]
        public void ReadUsers_SuccessfulRead_ReturnsTrue()
        {
            // Arrange

            // Act
            Agency.Instance.ReadUsers();
            List<User> users = Agency.Instance.Users;

            // Assert
            Assert.AreNotEqual(0, users.Count);
        }

        [TestMethod]
        public void ReadTours_SuccessfulRead_ReturnsTrue()
        {
            // Arrange

            // Act
            Agency.Instance.ReadTours();
            List<Tour> tours = Agency.Instance.Tours;

            // Assert
            Assert.AreNotEqual(0, tours.Count);
        }

        [TestMethod]
        public void ReadOrders_SuccessfulRead_ReturnsTrue()
        {
            // Arrange

            // Act
            Agency.Instance.ReadOrders();
            List<Order> orders = Agency.Instance.Orders;

            // Assert
            Assert.AreNotEqual(0, orders.Count);
        }
    }
}
