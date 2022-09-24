using System.ComponentModel.DataAnnotations;

namespace WEBGPApps.Models
{
    public class News
    {

        public int NewsId { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public string Des { get; set; }
    }
}
