namespace JWTtokenAPI.Models
{
    public class UserProfileModel
    {

        public int UserId { get; set; }
        public int profileID { get; set; }
        public int EducationID { get; set; }
        public int ExperienceID { get; set; }
        public int skillID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string location { get; set; }
        public string headline { get; set; }
        public string project { get; set; }
        public string summary { get; set; }
        public string degree { get; set; }
        public string schoolName { get; set; }
        public string Study_field { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string Position { get; set; }        
        public string Experiance_location { get; set; }
        public DateTime Experiance_start_date { get; set; }
        public DateTime Experiance_end_date { get; set; }
        public string skill_name { get; set; }
    }
}
