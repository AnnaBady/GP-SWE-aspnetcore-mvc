using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBGPApps.Models
{
    public class Courses
    {
        public int CoursesId { get; set; }
        public string CodeCourse { get; set; }
        public string CoursesName { get; set; }
        public int HoursOfCourse { get; set; }
        public string SemesterType { get; set; }
        public virtual ICollection<DC> DC { get; set; }
        public virtual ICollection<SC> SC { get; set; }

    }
}
