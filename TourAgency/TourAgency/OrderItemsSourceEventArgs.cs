using System;
using System.Collections;

namespace TourAgency
{
    public class OrderItemsSourceEventArgs : EventArgs
    {
        public IEnumerable? ItemsSource { get; set; }
    }
}
