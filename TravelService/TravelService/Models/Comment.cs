using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelService.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int? PlaceId { get; set; }
        public int? UserId { get; set; }

        public virtual Places Place { get; set; }
        public virtual Users User { get; set; }
    }
}
