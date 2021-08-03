using System;

namespace EventManagementApi.EntitiesDTO
{
    public class ThirdPartyEventDto
    {
        public Guid PrimaryKey { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string PosterImage { get; set; }
    }
}
