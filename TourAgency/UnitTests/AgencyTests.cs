using TourAgency;
using TourAgency.Models;

namespace UnitTests
{
    [TestClass]
    public class AgencyTests
    {
        [TestMethod]
        public void AddTour_ValidInputs_ReturnsTrue()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var country = "Spain";
            var city = "Barcelona";
            var date = DateTime.Now.AddDays(45);
            var duration = 7;
            var price = 500;
            var ticketsNumber = 20;
            var description = "A week in sunny Barcelona";

            // Act
            var result = travelAgency.AddTour(country, city, date, duration, price, ticketsNumber, description, out string message);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("", message);
        }

        [TestMethod]
        public void AddTour_InvalidDate_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var country = "Spain";
            var city = "Barcelona";
            var date = DateTime.Now.AddDays(-1);
            var duration = 7;
            var price = 500;
            var ticketsNumber = 20;
            var description = "A week in sunny Barcelona";

            // Act
            var result = travelAgency.AddTour(country, city, date, duration, price, ticketsNumber, description, out string message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Введіть коректну дату!", message);
        }

        [TestMethod]
        public void AddTour_DateBeforeThirtyDays_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var country = "Spain";
            var city = "Barcelona";
            var date = DateTime.Now.AddDays(10);
            var duration = 7;
            var price = 500;
            var ticketsNumber = 20;
            var description = "A week in sunny Barcelona";

            // Act
            var result = travelAgency.AddTour(country, city, date, duration, price, ticketsNumber, description, out string message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Не можна додати тур, який відбудиться раніше, ніж через 30 днів!", message);
        }

        [TestMethod]
        public void LoginUser_CorrectCredentials_ReturnsTrueAndSetsCurrentUser()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var email = "user@example.com";
            var password = "1q2w3e4r";

            var user = new RegisteredUser("Firstname Lastname", email, password);
            travelAgency.Users.Add(user);

            // Act
            var result = travelAgency.LoginUser(email, password, out string message, out UserStatus status, out string name);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Ласкаво просимо!", message);
            Assert.AreEqual(UserStatus.RegisteredUser, status);
            Assert.AreEqual("Firstname Lastname", name);
        }

        [TestMethod]
        public void LoginUser_IncorrectPassword_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var email = "user@example.com";
            var password = "1q2w3e4r5t";

            var user = new RegisteredUser("Firstname Lastname", email, password);
            travelAgency.Users.Add(user);

            // Act
            var result = travelAgency.LoginUser(email, password, out string message, out UserStatus status, out string name);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Пароль не правильний!", message);
            Assert.AreEqual(UserStatus.Guest, status);
            Assert.AreEqual("", name);
        }

        [TestMethod]
        public void LoginUser_IncorrectEmail_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var email = "user1@example.com";
            var password = "1q2w3e4r";

            var user = new RegisteredUser("Firstname Lastname", email, password);
            travelAgency.Users.Add(user);

            // Act
            var result = travelAgency.LoginUser(email, password, out string message, out UserStatus status, out string name);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Такого акаунту не існує!", message);
            Assert.AreEqual(UserStatus.Guest, status);
            Assert.AreEqual("", name);
        }

        [TestMethod]
        public void AddUser_ValidInput_Success()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            int counter = travelAgency.Users.Count;
            string email = $"test{counter}@example.com";
            string name = "John Doe";
            string password = "password123";

            // Act
            bool result = travelAgency.AddUser(email, name, password, out string message);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("", message);
            Assert.IsNotNull(travelAgency.Users.FirstOrDefault(u => u.Email == email));
        }

        [TestMethod]
        public void AddUser_InvalidEmailFormat_Failure()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            int counter = travelAgency.Users.Count;
            string email = $"test{counter}example.com";
            string name = "John Doe";
            string password = "password123";

            // Act
            bool result = travelAgency.AddUser(email, name, password, out string message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Неправильний формат пошти!", message);
            Assert.IsNull(travelAgency.Users.FirstOrDefault(u => u.Email == email));
        }

        [TestMethod]
        public void AddUser_WeakPassword_Failure()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            int counter = travelAgency.Users.Count;
            string email = $"test{counter}@example.com";
            string name = "John Doe";
            string password = "weak";

            // Act
            bool result = travelAgency.AddUser(email, name, password, out string message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Пароль не надійний!", message);
            Assert.IsNull(travelAgency.Users.FirstOrDefault(u => u.Email == email));
        }

        [TestMethod]
        public void AddUser_ExistingUser_Failure()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            string email = $"user@example.com";
            string name = "John Doe";
            string password = "password123";

            // Act
            bool result = travelAgency.AddUser(email, name, password, out string message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Такий користувач вже існує!", message);
            Assert.AreEqual(1, travelAgency.Users.Count(u => u.Email == email));
        }

        [TestMethod]
        public void OrderATour_SuccessfulBooking_ReturnsSuccessMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            Tour tour = new Tour("France", "Paris", DateTime.Now.AddDays(60), 7, 5000f, TourStatus.Actual, 20, "Explore the City of Love");
            travelAgency.Tours.Add(tour);
            travelAgency.CurrentTour = tour;
            travelAgency.CurrentUser = new RegisteredUser("test@test.com", "John", "password");

            // Act
            string result = travelAgency.OrderATour(2);

            // Assert
            Assert.IsTrue(result.Contains("Номер замовлення"));
            Assert.IsTrue(result.Contains("Тур: Paris"));
            Assert.IsTrue(result.Contains("Ціна: 10000"));
            Assert.IsTrue(result.Contains("Куплено квитків: 2"));
            Assert.IsTrue(result.Contains("Дата відправлення"));
            Assert.IsTrue(result.Contains("Тривалість туру"));
            Assert.IsTrue(result.Contains("John"));
            Assert.IsTrue(result.Contains("Замовлення оформлене"));
        }

        [TestMethod]
        public void OrderATour_UserNotLoggedIn_ReturnsErrorMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            travelAgency.CurrentUser = null;
            Tour tour = new Tour("Spain", "Barcelona", DateTime.Now.AddDays(60), 7, 5000f, TourStatus.Actual, 20, "Discover the vibrant city of Barcelona");
            travelAgency.Tours.Add(tour);
            travelAgency.CurrentTour = tour;

            // Act
            string result = travelAgency.OrderATour(2);

            // Assert
            Assert.AreEqual("Ви не увійшли в акаунт", result);
        }

        [TestMethod]
        public void OrderATour_NoTourSelected_ReturnsErrorMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            travelAgency.CurrentTour = null;
            travelAgency.CurrentUser = new RegisteredUser("test@test.com", "John", "password");

            // Act
            string result = travelAgency.OrderATour(2);

            // Assert
            Assert.AreEqual("Ви не обрали тур", result);
        }

        [TestMethod]
        public void OrderATour_TicketNumberLessThanOne_ReturnsErrorMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            Tour tour = new Tour("USA", "New York", DateTime.Now.AddDays(60), 7, 5000f, TourStatus.Actual, 20, "Experience the excitement of the Big Apple");
            travelAgency.Tours.Add(tour);
            travelAgency.CurrentTour = tour;
            travelAgency.CurrentUser = new RegisteredUser("test@test.com", "John", "password");

            // Act
            string result = travelAgency.OrderATour(0);

            // Assert
            Assert.AreEqual("Неможливо купити менше 1 квитка", result);
        }

        [TestMethod]
        public void OrderATour_NotEnoughTickets_ReturnsSuccessMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            Tour tour = new Tour("France", "Paris", DateTime.Now.AddDays(60), 7, 5000f, TourStatus.Actual, 20, "Explore the City of Love");
            travelAgency.Tours.Add(tour);
            travelAgency.CurrentTour = tour;
            travelAgency.CurrentUser = new RegisteredUser("test@test.com", "John", "password");

            // Act
            string result = travelAgency.OrderATour(40);

            // Assert
            Assert.AreEqual("Помилка бронювання туру", result);
        }

        [TestMethod]
        public void EditTour_WithValidTour_ReturnsTrue()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var tour = new Tour("France", "Paris2", DateTime.Now.AddDays(50), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love");
            travelAgency.Tours.Add(tour);
            travelAgency.CurrentTour = tour;

            // Act
            bool result = travelAgency.EditTour(tour, out string message);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("", message);
        }

        [TestMethod]
        public void EditTour_WithInvalidTour_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            User user = new RegisteredUser("email@email.com", "Name", "password");
            var tour = new Tour("France", "Paris", DateTime.Now.AddDays(-10), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love");
            var order = new Order(2, OrderStatus.Actual, user, tour);
            var travelAgency = Agency.Instance;
            travelAgency.Orders.Add(order);
            travelAgency.CurrentTour = tour;

            // Act
            bool result = travelAgency.EditTour(tour, out string message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Тур не може бути редагований! Тур відбувся!", message);
        }

        [TestMethod]
        public void EditTour_WithTourWithOrders_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            User user = new RegisteredUser("email@email.com", "Name", "password");
            var tour = new Tour("France", "Paris", DateTime.Now.AddDays(7), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love");
            var order = new Order(2, OrderStatus.Actual, user, tour);
            var travelAgency = Agency.Instance;
            travelAgency.Orders.Add(order);
            travelAgency.CurrentTour = tour;

            // Act
            bool result = travelAgency.EditTour(tour, out string message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Тур не може бути редагований! На тур вже є заброньовані квитки!", message);
        }

        [TestMethod]
        public void EditTour_WithNullTour_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            Tour? tour = null;
            var travelAgency = Agency.Instance;
            travelAgency.CurrentTour = tour;

            // Act
            bool result = travelAgency.EditTour(tour, out string message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Тур не обраний!", message);
        }

        [TestMethod]
        public void RemoveATour_WhenTourIsNull_ReturnsFalseAndMessage()
        {
            // Arrange
            Tour? tour = null;
            var travelAgency = Agency.Instance;
            string message;

            // Act
            bool result = travelAgency.RemoveATour(tour, out message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Тур не обраний!", message);
        }

        [TestMethod]
        public void RemoveATour_WhenTourHasNoOrdersAndDateIsInFuture_CancelsTourAndReturnsTrue()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var tour = travelAgency.Tours[^1];
            tour.Date = DateTime.Now.AddDays(30);
            string message;

            // Act
            bool result = travelAgency.RemoveATour(tour, out message);
            tour = travelAgency.Tours[^1];

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(TourStatus.Canceled, tour.Status);
        }

        [TestMethod]
        public void RemoveATour_WhenTourHasOrders_ReturnsFalseAndMessage()
        {
            // Arrange
            User user = new RegisteredUser("email@email.com", "Name", "password");
            var travelAgency = Agency.Instance;
            var tour = travelAgency.Tours[^1];
            var order = new Order(2, OrderStatus.Actual, user, tour);
            travelAgency.Orders.Add(order);
            string message;

            // Act
            bool result = travelAgency.RemoveATour(tour, out message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Тур не може бути видалений! На тур вже є заброньовані квитки!", message);
        }

        [TestMethod]
        public void RemoveATour_WhenTourDateIsInPast_ReturnsFalseAndMessage()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var tour = travelAgency.Tours[^1];
            tour.Date = DateTime.Now.AddDays(-20);

            string message;

            // Act
            bool result = travelAgency.RemoveATour(tour, out message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Тур не може бути видалений! Тур вже відбувся!", message);
        }

        [TestMethod]
        public void CancelOrder_WhenOrderExistsAndCurrentUserIsNotNull_ReturnsTrueAndCancelsOrderAndUpdatesTourTickets()
        {
            // Arrange
            var travelAgency = Agency.Instance;
            var tour = new Tour("France", "Paris", DateTime.Now.AddDays(7), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love");
            travelAgency.Tours.Add(tour);
            User user = new RegisteredUser("email@email.com", "Name", "password");
            travelAgency.CurrentUser = user;
            var order = new Order(2, OrderStatus.Actual, user, tour);
            travelAgency.Orders.Add(order);

            // Act
            bool result = travelAgency.CancelOrder(order.Id);
            var orderNew = travelAgency.Orders.Where(o => o.Id == order.Id).First();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(OrderStatus.Canceled, orderNew.Status);
        }

        [TestMethod]
        public void CancelOrder_WhenOrderIsAlreadyCanceled_ReturnsFalse()
        {
            var travelAgency = Agency.Instance;
            var tour = new Tour("France", "Paris", DateTime.Now.AddDays(7), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love");
            travelAgency.Tours.Add(tour);
            User user = new RegisteredUser("email@email.com", "Name", "password");
            travelAgency.CurrentUser = user;
            var order = new Order(2, OrderStatus.Canceled, user, tour);
            travelAgency.Orders.Add(order);

            // Act
            bool result = travelAgency.CancelOrder(order.Id);

            // Assert
            Assert.IsFalse(result);
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
