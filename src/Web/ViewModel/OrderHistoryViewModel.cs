using System;
using System.ComponentModel;

namespace Web.ViewModel
{
    public class OrderHistoryViewModel
    {
        [DisplayName("Event-Description")]
        public string Description { get; set; }

        [DisplayName("Order-time")]
        public DateTime OrderTime { get; set; }

        [DisplayName("Event-Seat-Row")]
        public int Row { get; set; }

        [DisplayName("Event-Seat-Number")]
        public int Number { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }
    }
}
