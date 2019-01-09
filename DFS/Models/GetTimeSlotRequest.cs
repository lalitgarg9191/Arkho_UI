using System;
namespace DFS.Models
{
    public class GetTimeSlotRequest
    {
        public string emailID { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }
}
