using System;

namespace TicketManagement.DataAccess.Entities
{
    public class UserTicket
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int SeatId { get; set; }

        public DateTime PurchaseTime { get; set; }
    }
}
