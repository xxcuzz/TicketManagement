using System.ComponentModel;

namespace Web.ViewModel
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        [DisplayName("Event-Name")]
        public string Name { get; set; }

        [DisplayName("Event-Description")]
        public string Description { get; set; }

        [DisplayName("Ticket-Available")]
        public string TicketAvailable { get; set; }
    }
}
