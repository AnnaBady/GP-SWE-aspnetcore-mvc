using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBGPApps.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public int NationalID { get; set; }
        public string Gender { get; set; }
        public string HomeAddress { get; set; }
        public string Emergency { get; set; }
        public DateTime DOB { get; set; }
        public virtual ICollection<DC> DC { get; set; }
        public virtual ICollection<SC> SC { get; set; }
    }
}
