using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelService.Models
{
    public partial class Places
    {
        public Places()
        {
            Comment = new HashSet<Comment>();
            Images = new HashSet<Images>();
            Rating = new HashSet<Rating>();
        }

        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public int? UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Images> Images { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
