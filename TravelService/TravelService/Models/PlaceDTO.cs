using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TravelService.Models
{
    [DataContract]
    public class PlaceDTO
    {
        [DataMember]
        public int PlaceId { get; set; }

        [DataMember]
        public string PlaceName { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Info { get; set; }

        [DataMember]
        public string ImageLink { get; set; }

        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public List<string> imageList { get; set; }

        public PlaceDTO(int id, string name, string title, string info, string image)
        {
            this.PlaceId = id;
            this.PlaceName = name;
            this.Title = title;
            this.Info = info;
            this.ImageLink = image;
        }
    }
}
