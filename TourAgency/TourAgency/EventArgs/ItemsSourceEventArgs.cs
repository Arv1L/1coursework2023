using System;
using System.Collections;

namespace TourAgency
{
    public class ItemsSourceEventArgs : EventArgs
    {
        public IEnumerable? ItemsSource { get; set; }
    }
}
