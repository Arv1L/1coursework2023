using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TourAgency.Models;

namespace TourAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private AdminWindow? adminWindow;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            SortList();
        }

        #region UserPanel
        private void UserButtonClick(object sender, RoutedEventArgs e)
        {
            User? user = Agency.Instance.CurrentUser;

            if (user == null)
                ToLogin();
            else if (userPanel.Visibility != Visibility.Visible)
            {
                userPanel.SelectedItem = null;
                userPanel.Visibility = Visibility.Visible;

                if (user.Status == UserStatus.Administrator)
                {
                    adminToolButton.Visibility = Visibility.Visible;
                    userOrdersButton.Visibility = Visibility.Collapsed;
                }
                else if (user.Status == UserStatus.RegisteredUser)
                {
                    adminToolButton.Visibility = Visibility.Collapsed;
                    userOrdersButton.Visibility = Visibility.Visible;
                }
            }
            else
                UserPanelClose();
        }

        private void AdminToolOnLoad(object sender, RoutedEventArgs e)
        {
            UserPanelClose();
            adminWindow.Show();
            adminWindow.RefreshOrderList();
            adminWindow.RefreshTourList();
        }

        private void UserPanelClose()
        {
            userPanel.Visibility = Visibility.Collapsed;
            userPanel.SelectedItem = null;
        }

        private void UserOrdersClick(object sender, RoutedEventArgs e)
        {
            UserPanelClose();
            userOrdersPanel.Visibility = Visibility.Visible;

            int userId = Agency.Instance.CurrentUser!.Id;
            List<Order> orders = Agency.Instance.Orders.Where(o => o.UserId == userId).ToList();

            userOrders.ItemsSource = null;
            var collectionView = new ListCollectionView(orders);
            collectionView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            userOrders.ItemsSource = collectionView;

            if (userOrders.ItemsSource != null)
                ordersAreEmpty.Visibility = Visibility.Collapsed;
            else
                ordersAreEmpty.Visibility = Visibility.Visible;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            userPanel.Visibility = Visibility.Collapsed;

            if (Agency.Instance.CurrentUser!.Status == UserStatus.Administrator)
                adminWindow.Close();

            Agency.Instance.CurrentUser = null;

            userButton.Content = userButton.Tag;
            orderButton.IsEnabled = false;

            if (adminWindow != null)
            {
                adminWindow.TourItemsSourceUpdated -= ItemsSourceUpdated;
                adminWindow.Close();
            }
        }
        #endregion



        #region Tour manipulations
        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            string str = searchBox.Text.Trim();
            List<Tour> books = Agency.Instance.Tours.Where(b => b.Country.ToLower().Contains(str) || b.City.ToLower().Contains(str)).ToList();
            tourList.ItemsSource = books;
        }

        private void SortList()
        {
            var collectionView = new ListCollectionView(Agency.Instance.Tours);
            collectionView.SortDescriptions.Add(new SortDescription("Country", ListSortDirection.Ascending));
            collectionView.SortDescriptions.Add(new SortDescription("City", ListSortDirection.Ascending));
            tourList.ItemsSource = collectionView;
        }

        private void TourChanged(object sender, SelectionChangedEventArgs e)
        {
            Tour? tour = Agency.Instance.CurrentTour = tourList.SelectedItem as Tour;

            infoPanel.Visibility = Visibility.Visible;

            if (tour != null)
            {
                if (Agency.Instance.CurrentUser != null && Agency.Instance.CurrentUser.Status == UserStatus.RegisteredUser)
                {
                    tourName.TextDecorations = null;
                    tourCountry.TextDecorations = null;
                    tourDuration.TextDecorations = null;
                    tourPrice.TextDecorations = null;
                    tourDate.TextDecorations = null;
                    tourCurrentTicketNumber.TextDecorations = null;
                    tourMaxTicketNumber.TextDecorations = null;
                    tourDescription.TextDecorations = null;

                    orderButton.IsEnabled = false;

                    if (tour.Date > DateTime.Now)
                    {
                        if (tour.CurrentTicketsNumber > 0 && tour.Status == TourStatus.Actual)
                            orderButton.IsEnabled = true;
                        if (tour.Status == TourStatus.Canceled)
                        {
                            tourName.TextDecorations = TextDecorations.Strikethrough;
                            tourCountry.TextDecorations = TextDecorations.Strikethrough;
                            tourDuration.TextDecorations = TextDecorations.Strikethrough;
                            tourPrice.TextDecorations = TextDecorations.Strikethrough;
                            tourDate.TextDecorations = TextDecorations.Strikethrough;
                            tourCurrentTicketNumber.TextDecorations = TextDecorations.Strikethrough;
                            tourMaxTicketNumber.TextDecorations = TextDecorations.Strikethrough;
                            tourDescription.TextDecorations = TextDecorations.Strikethrough;
                        }
                    }
                }

                tourName.Text = tour.City;
                tourCountry.Text = tour.Country;
                tourDuration.Text = $"{tour.Duration} днів";
                tourPrice.Text = $"{tour.Price} грн";
                tourDate.Text = tour.Date.ToShortDateString();
                tourCurrentTicketNumber.Text = tour.CurrentTicketsNumber.ToString();
                tourMaxTicketNumber.Text = tour.MaxTicketsNumber.ToString();
                tourDescription.Text = tour.Description;
            }
        }

        private void ConfirmOrder(object sender, RoutedEventArgs e)
        {
            confirmOrder.Visibility = Visibility.Visible;
            userNameBox.Text = Agency.Instance.CurrentUser!.Name;
            userEmailBox.Text = Agency.Instance.CurrentUser!.Email;
            priceBlock.Text = Agency.Instance.CurrentTour!.Price.ToString();
        }

        private void ConfirmClose(object sender, RoutedEventArgs e) => confirmOrder.Visibility = Visibility.Collapsed;

        private void OnReserved(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ticketsCountBox.Text, out int count))
            {
                string info = Agency.Instance.OrderATour(count);

                tourCurrentTicketNumber.Text = Agency.Instance.CurrentTour!.CurrentTicketsNumber.ToString();
                tourList.Items.Refresh();

                MessageBox.Show(info);

                OrderItemsSourceUpdated?.Invoke(this, new ItemsSourceEventArgs { ItemsSource = Agency.Instance.Orders });

                confirmOrder.Visibility = Visibility.Collapsed;
                tourCurrentTicketNumber.Text = "";
            }
        }

        private void TicketsCountChanged(object sender, TextChangedEventArgs e)
        {
            if (confirmOrder.Visibility == Visibility.Visible && int.TryParse(((TextBox)sender).Text, out int ticketsNumber))
            {
                priceBlock.Text = (Math.Round(ticketsNumber * Agency.Instance.CurrentTour!.Price, 2)).ToString();
            }
        }
        #endregion



        #region Login methods
        private void OnLogin(object sender, RoutedEventArgs e)
        {
            string email = logEmailBox.Text;
            string pass = logPassBox.Text;

            if (email != null && pass != null)
            {
                if (Agency.Instance.LoginUser(email, pass,
                    out string message, out UserStatus status, out string name))
                {
                    if (status == UserStatus.RegisteredUser)
                        orderButton.IsEnabled = true;
                    if (status == UserStatus.Administrator)
                    {
                        adminWindow = new AdminWindow();
                        adminWindow.TourItemsSourceUpdated += ItemsSourceUpdated;
                    }

                    if (Agency.Instance.CurrentTour != null && Agency.Instance.CurrentTour.Status == TourStatus.Canceled)
                        orderButton.IsEnabled = false;

                    userButton.Content = name.Split()[0];

                    logEmailBox.Text = null;
                    logPassBox.Text = null;
                    loginForm.Visibility = Visibility.Collapsed;
                }
                else
                    MessageBox.Show(message);
            }
        }

        private void LogOnOpen(object sender, RoutedEventArgs e) => ToLogin();

        private void ToLogin()
        {
            regForm.Visibility = Visibility.Collapsed;
            loginForm.Visibility = Visibility.Visible;
        }

        private void LoginOnClose(object sender, RoutedEventArgs e) => loginForm.Visibility = Visibility.Collapsed;
        #endregion



        #region Registration methods
        private void OnRegistration(object sender, RoutedEventArgs e)
        {
            if (regEmailBox.Text != null && regNameBox.Text != null && regSurnameBox.Text != null && regPassBox.Text != null && regRepeatPassBox.Text != null)
            {
                string email = logEmailBox.Text = regEmailBox.Text;
                string name = $"{regNameBox.Text} {regSurnameBox.Text}";

                if (regPassBox.Text == regRepeatPassBox.Text)
                {
                    string pass = logPassBox.Text = regPassBox.Text;

                    if (Agency.Instance.AddUser(email, name, pass, out string message))
                    {
                        regEmailBox.Text = null;
                        regNameBox.Text = null;
                        regSurnameBox.Text = null;
                        regPassBox.Text = null;
                        regRepeatPassBox.Text = null;

                        regForm.Visibility = Visibility.Collapsed;
                        ToLogin();
                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                }
                else
                    MessageBox.Show("Паролі не співпадають!");
            }
        }

        private void RegOnOpen(object sender, RoutedEventArgs e)
        {
            loginForm.Visibility = Visibility.Collapsed;
            regForm.Visibility = Visibility.Visible;
        }

        private void RegOnClose(object sender, RoutedEventArgs e) => regForm.Visibility = Visibility.Collapsed;
        #endregion



        #region Events
        public event EventHandler<ItemsSourceEventArgs>? OrderItemsSourceUpdated;

        private void ItemsSourceUpdated(object? sender, ItemsSourceEventArgs e)
        {
            tourList.ItemsSource = e.ItemsSource;
            SortList();
            tourList.Items.Refresh();
        }
        #endregion



        #region Order manipulations
        private void CancelOrder(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;

            if (button != null)
            {
                string? status = button!.Tag.ToString();

                Order? order = userOrders.SelectedItem as Order;

                if (order != null)
                    if (status == "Actual")
                    {
                        if (MessageBox.Show("Ви впевнені, що хочете скасувати цей тур?", "Підтвердження", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            Agency.Instance.CancelOrder(order.Id);

                            userOrders.Items.Refresh();

                            SortList();

                            MessageBox.Show("Тур успішно скасовано");
                        }
                    }
                    else if (status == "Canceled")
                        MessageBox.Show("Ви вже скасували цей тур");
                    else
                        MessageBox.Show("Тур вже відбувся / відбувається");
            }
        }

        private void UserOrdersCloseClick(object sender, RoutedEventArgs e) => userOrdersPanel.Visibility = Visibility.Collapsed;
        #endregion



        #region Drag, Minimize, Close
        private void DragWindow(object sender, MouseButtonEventArgs e) => DragMove();

        private void OnMinimize(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Agency.Instance.SaveAgency();
            adminWindow?.Close();
            Application.Current.Shutdown();
        }
        #endregion
    }
}
