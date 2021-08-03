using System;
using System.ComponentModel;

namespace Web.ViewModel
{
    public class EditEventViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Layout")]
        public int LayoutId { get; set; }

        [DisplayName("Start")]
        public DateTime EventStart { get; set; }

        [DisplayName("End")]
        public DateTime EventEnd { get; set; }

        [DisplayName("Image")]
        public string Image { get; set; }
    }
}
