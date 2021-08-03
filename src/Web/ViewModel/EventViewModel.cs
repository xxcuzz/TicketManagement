using System;
using System.Collections.Generic;
using System.ComponentModel;
using TicketManagement.BusinessLogic.EntitiesDTO;

namespace Web.ViewModel
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [DisplayName("Event-Name")]
        public string Name { get; set; }

        [DisplayName("Event-Description")]
        public string Description { get; set; }

        [DisplayName("Event-Start")]
        public DateTime StartTime { get; set; }

        [DisplayName("Event-End")]
        public DateTime EndTime { get; set; }

        [DisplayName("EventAreas")]
        public IEnumerable<EventAreaDto> EventAreas { get; set; }

        [DisplayName("EventSeats")]
        public IEnumerable<EventSeatDto> EventSeats { get; set; }
    }
}
