namespace WEBGPApps.Models
{
    public class DC
    {
        public int Id { get; set; }
       
        
        public int CoursesId { get; set; }
        public virtual Courses Courses { get; set; }
       
        
        
        public string DoctorId { get; set; }
        public virtual ApplicationUser Doctor { get; set; }


    }
}
