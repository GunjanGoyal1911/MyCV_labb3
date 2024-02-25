using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyCV_labb3.Model
{
    public class UserModel
    {        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }        
        public string Email { get; set; }  
        public List<Skill> Skills { get; set;}
        public List<Project> Projects { get; set; }

    }
}
