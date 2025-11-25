using System;
using Auth.Models;

namespace CaroProjectNhom15.Models
{
    public class RoomModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public UserModel Host {  get; set; }
        public UserModel Guest { get; set; }
        public string Status { get; set; }
        public RoomModel()
        {
            Status = "Waiting";
        }

    }
}
