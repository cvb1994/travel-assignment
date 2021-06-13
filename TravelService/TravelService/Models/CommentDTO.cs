using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TravelService.Models
{
    [DataContract]
    public class CommentDTO
    {
        [DataMember]
        public string username { get; set; }

        [DataMember]
        public string info { get; set; }

        [DataMember]
        public int commnetId { get; set; }

        public CommentDTO(int commentId, string name,string info)
        {
            this.commnetId = commentId;
            this.username = name;
            this.info = info;
        }
    }
}
