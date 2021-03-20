using System;
using Newtonsoft.Json;

namespace TimeManager.Data.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public DateTime StartDate { get; set; }
        public int UserId { get; set; }

        [JsonIgnore] 
        public User User { get; set; }
    }
}