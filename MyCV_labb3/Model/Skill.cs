using System.Text.Json.Serialization;

namespace MyCV_labb3.Model
{
    public class Skill
    {
        
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int YearsOfExperience { get; set; }
        public string SkillLevel {  get; set; }
    }
}
