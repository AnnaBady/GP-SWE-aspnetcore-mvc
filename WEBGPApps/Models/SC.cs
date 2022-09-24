namespace WEBGPApps.Models
{
    public class SC
    {
        public int Id { get; set; }

        public string StudentId {get; set;}
        public virtual ApplicationUser Student {get; set;}


        public int CoursesId { get; set;}
        public virtual Courses Courses {get; set;}

        public int mark_mid {get;set;}
        public int mark_practical {get;set;}
        public int mark_Final {get; set;}
    }
}
