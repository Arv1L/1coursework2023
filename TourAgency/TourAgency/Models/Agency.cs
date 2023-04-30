using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace TourAgency.Models
{
    public class Agency
    {
        #region Constants
        public const string TOURS_PATH     = "Json\\Tours.json";
        public const string USERS_PATH     = "Json\\Users.json";
        public const string ADMINS_PATH    = "Json\\Admins.json";
        public const string ORDERS_PATH    = "Json\\Orders.json";
        #endregion

        #region Static properties
        public static Agency Instance { get; } = new Agency();
        #endregion

        #region Properties
        public User? CurrentUser { get; set; }
        public Tour? CurrentTour { get; set; }

        public List<User> Users { get; set; }
        public List<Tour> Tours { get; set; }
        public List<Order> Orders { get; set; }
        #endregion

        private Agency()
        {
            CurrentUser = null;

            Tours = new List<Tour>();
            Orders = new List<Order>();
            Users = new List<User>();

            ReadAgency();
        }

        #region Tour manipulations
        public bool AddTour(string country, string city, DateTime date, int duration, float price, int ticketsNumber, string description)
        {
            if (date >= DateTime.Now.AddDays(30))
            {
                Tour tour = new(country, city, date, duration, price, TourStatus.Actual, ticketsNumber, description);

                if (Tours.FirstOrDefault(t => t.City == tour.City && t.Date == tour.Date && t.Duration == tour.Duration) == null)
                    Tours.Add(tour);

                SaveTours();

                return true;
            }

            return false;
        }

        public string OrderATour(int ticketNumber)
        {
            string message = "";

            if (CurrentUser != null)
            {
                if (CurrentTour != null)
                {
                    if (ticketNumber > 0)
                    {
                        if (CurrentUser.OrderTour(CurrentTour, ticketNumber, out Order order))
                        {
                            if (Orders.FirstOrDefault(o => o.UserName == order.UserName && o.City == order.City) == null)
                                Orders.Add(order);

                            SaveOrders();
                            SaveTours();

                            message = $"Номер замовлення: {order.Id}\n" +
                                      $"Тур: {order.City}\n" +
                                      $"Ціна: {Math.Round(order.Price * order.TicketNumber, 2)} ₴\n" +
                                      $"Куплено квитків: {order.TicketNumber}\n" +
                                      $"Дата відправлення: {order.Date.ToShortDateString()}\n" +
                                      $"Тривалість туру: {order.Duration}\n" +
                                      $"Статус туру: {order.Status}\n" +
                                      $"\n{order.UserName}" +
                                      "Замовлення оформлене";
                        }
                        else
                            message = "Помилка бронювання туру";
                    }
                    else
                        message = "Неможливо купити менше 1 квитка";
                }
                else
                    message = "Ви не обрали тур";
            }
            else
                message = "Ви не увійшли в акаунт";

            return message;
        }

        public bool RemoveATour(Tour? tour)
        {
            if (tour != null && !Orders.Any(o => o.TourId == tour.Id && o.Status != OrderStatus.Canceled))
            {
                Tours.Where(t => t.Id == tour.Id).First().Status = TourStatus.Canceled;
                SaveTours();
                return true;
            }
            else return false;
        }
        #endregion

        #region User manipulations
        public bool LoginUser(string email, string password, out string message, out UserStatus status, out string name)
        {
            status = UserStatus.Guest;
            name = "";

            User? user = Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                if (user.Password == password)
                {
                    CurrentUser = user;
                    status = CurrentUser.Status;
                    name = CurrentUser.Name;

                    message = "Ласкаво просимо!";
                    return true;
                }
                else
                {
                    message = "Пароль не правильний!";
                    return false;
                }
            }
            else
            {
                message = "Такого акаунту не існує!";
                return false;
            }
        }

        public bool AddUser(string email, string name, string password)
        {
            User user = new RegisteredUser(email, name, password);

            if (Users.FirstOrDefault(u => u.Email == user.Email) == null)
            {
                Users.Add(user);
                SaveUsers();
                return true;
            }
            return false;
        }
        #endregion

        #region Order manipulations
        public bool CancelOrder(int orderId)
        {
            if (CurrentUser!.ReturnTickets(orderId, Orders, out int index, out int ticketsReturned))
            {
                Orders[index].Status = OrderStatus.Canceled;

                Order order = Orders[index];

                Tours.Where(t => t.Id == order.TourId).First().CurrentTicketsNumber += ticketsReturned;
                SaveAgency();

                return true;
            }
            else return false;
        }
        #endregion

        #region Save and read agency info
        public void SaveAgency()
        {
            SaveUsers();
            SaveTours();
            SaveOrders();
        }

        public void ReadAgency()
        {
            ReadUsers();
            ReadTours();
            ReadOrders();
        }

        public bool SaveUsers() => JsonLogic.SaveToJson(Users, USERS_PATH);

        public bool SaveTours() => JsonLogic.SaveToJson(Tours, TOURS_PATH);

        public bool SaveOrders() => JsonLogic.SaveToJson(Orders, ORDERS_PATH);

        public void ReadUsers()
        {
            if (Users.Count > 0)
                Users.Clear();
            if (JsonLogic.ReadFromJson(USERS_PATH, out List<RegisteredUser> userList) &&
                JsonLogic.ReadFromJson(ADMINS_PATH, out List<Administrator> adminList))
            {
                foreach (User user in userList)
                    if (Users.FirstOrDefault(u => u.Email == user.Email) == null)
                        Users.Add(user);
                foreach (User admin in adminList)
                {
                    User? temp = Users.FirstOrDefault(u => u.Email == admin.Email);
                    if (temp != null)
                    {
                        Users.Remove(temp);
                        if (Users.FirstOrDefault(u => u.Email == admin.Email) == null)
                            Users.Add(admin);
                    }
                    else
                        if (Users.FirstOrDefault(u => u.Email == admin.Email) == null)
                            Users.Add(admin);
                }
            }
        }

        public void ReadTours()
        {
            if (Tours.Count > 0)
                Tours.Clear();
            if (JsonLogic.ReadFromJson(TOURS_PATH, out List<Tour> tours))
                Tours = tours;
        }

        public void ReadOrders()
        {
            if (Orders.Count > 0)
                Orders.Clear();
            if (JsonLogic.ReadFromJson(ORDERS_PATH, out List<Order> orders))
                Orders = orders;
        }
        #endregion
    }
}
