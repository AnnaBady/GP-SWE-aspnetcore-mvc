namespace WEBGPApps.Models
{
    public class Att
    {
        public int AttID { get; set; }  
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CoursesId { get; set; }
        public Courses Courses { get; set; }
        public int AttType { get; set; }

    }
}
