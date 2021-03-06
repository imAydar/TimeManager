using System.Collections.Generic;

namespace TimeManager.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        
    }
}