﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelService.Models
{
    public partial class Images
    {
        public Images()
        {
            Comment = new HashSet<Comment>();
            Rating = new HashSet<Rating>();
        }

        public int ImageId { get; set; }
        public byte[] ImageLink { get; set; }
        public int? PlaceId { get; set; }

        public virtual Places Place { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
