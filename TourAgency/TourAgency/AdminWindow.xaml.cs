using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TourAgency.Models;

namespace TourAgency
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        #region Fields
        private Tour? currentTour;
        #endregion

        public AdminWindow()
        {
            InitializeComponent();

            RefreshTourList();
            RefreshOrderList();

            Window? mainWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "MainWindow");
            if (mainWindow != null)
                ((MainWindow)mainWindow).OrderItemsSourceUpdated += ItemsSourceUpdated;
        }

        #region Tour manipulations
        private void SaveTour(object sender, RoutedEventArgs e)
        {
            if (countryBox.Text != "" && nameBox.Text != "" && descriptionBox.Text != "" &&
                durationBox.Text != "" && priceBox.Text != "")
            {
                if (priceBox.Text.Contains('.'))
                    priceBox.Text = priceBox.Text.Replace('.', ',');

                if (int.TryParse(durationBox.Text, out int duration) &&
                    float.TryParse(priceBox.Text, out float price) &&
                    int.TryParse(ticketCountBox.Text, out int ticketCount) &&
                    DateTime.TryParse(dateBox.Text, out DateTime date))
                {
                    string country = countryBox.Text;
                    string name = nameBox.Text;
                    string description = descriptionBox.Text;

                    if (Agency.Instance.AddTour(country, name, date, duration, price, ticketCount, description))
                        countryBox.Text = nameBox.Text = descriptionBox.Text = durationBox.Text = priceBox.Text = dateBox.Text = ticketCountBox.Text = "";
                    else
                        MessageBox.Show("Занадто близька дата!");

                    RefreshTourList();

                    Agency.Instance.SaveTours();
                }
            }
            else MessageBox.Show("Усі поля мають бути заповненими!");
        }

        private void EditCurrentTour(object sender, RoutedEventArgs e)
        {
            if (currentTour != null &&
                int.TryParse(editTicketCountBox.Text, out int ticketsNumber) &&
                currentTour.CurrentTicketsNumber <= ticketsNumber &&
                float.TryParse(editPriceBox.Text, out float price) &&
                int.TryParse(editDurationBox.Text, out int duration) &&
                DateTime.TryParse(editDateBox.Text, out DateTime date))
            {
                currentTour.Country = editCountryBox.Text;
                currentTour.City = editNameBox.Text;
                currentTour.MaxTicketsNumber = ticketsNumber;
                currentTour.Price = price;
                currentTour.Duration = duration;
                currentTour.Date = date;
                currentTour.Description = editDescriptionBox.Text;

                List<Tour> tours = Agency.Instance.Tours;
                Agency.Instance.Tours[tours.IndexOf(tours.First(t => t.Id == currentTour.Id))] = currentTour;

                editCurrentTour.Visibility = Visibility.Collapsed;
                RefreshTourList();
            }
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            editCurrentTour.Visibility = Visibility.Visible;

            currentTour = tourList.SelectedItem as Tour;

            if (currentTour != null)
            {
                editCountryBox.Text = currentTour.Country;
                editNameBox.Text = currentTour.City;
                editTicketCountBox.Text = currentTour.MaxTicketsNumber.ToString();
                editPriceBox.Text = currentTour.Price.ToString();
                editDurationBox.Text = currentTour.Duration.ToString();
                editDateBox.Text = currentTour.Date.ToShortDateString();
                editDescriptionBox.Text = currentTour.Description;
            }
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            currentTour = tourList.SelectedItem as Tour;
            if (Agency.Instance.RemoveATour(currentTour))
            {
                RefreshTourList();
                currentTour = null;
            }
            else MessageBox.Show("Невдалось видалити тур");
        }
        #endregion

        #region Refresh lists
        public void RefreshTourList()
        {
            RefreshList(tourList, Agency.Instance.Tours, toursAreEmpty);

            TourItemsSourceUpdated?.Invoke(this, new ItemsSourceEventArgs { ItemsSource = tourList.ItemsSource });
        }

        public void RefreshOrderList()
        {
            RefreshList(orderList, Agency.Instance.Orders, ordersAreEmpty);
        }

        private void RefreshList<T>(DataGrid dataGrid, List<T> list, TextBlock emptyInfoBlock)
        {
            SortList(dataGrid, list);

            if (dataGrid.Items.Count > 0)
                emptyInfoBlock.Visibility = Visibility.Collapsed;
            else
                emptyInfoBlock.Visibility = Visibility.Visible;
        }

        private void SortList<T>(DataGrid dataGrid, List<T> list)
        {
            dataGrid.ItemsSource = null;
            var collectionView = new ListCollectionView(list);
            collectionView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            dataGrid.ItemsSource = collectionView;
        }
        #endregion

        #region Events
        public event EventHandler<ItemsSourceEventArgs>? TourItemsSourceUpdated;

        private void ItemsSourceUpdated(object? sender, ItemsSourceEventArgs e) => RefreshOrderList();
        #endregion

        #region Drag, Minimize, Close
        private void DragWindow(object sender, MouseButtonEventArgs e) => DragMove();

        private void OnMinimize(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void OnClose(object sender, RoutedEventArgs e) => Hide();
        #endregion
    }
}
