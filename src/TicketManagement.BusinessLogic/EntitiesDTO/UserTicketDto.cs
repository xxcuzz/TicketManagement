using System;

namespace TicketManagement.BusinessLogic.EntitiesDTO
{
    public class UserTicketDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int SeatId { get; set; }

        public DateTime PurchaseTime { get; set; }
    }
}
