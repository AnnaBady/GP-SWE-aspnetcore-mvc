
using System.ComponentModel.DataAnnotations;

namespace WEBGPApps.Models
{
    public class QRCodeModel
    {
        public int QuizId { get; set; }

        [Display(Name = "Enter QRCode Text")]
        public string QRCodeText { get; set; }
        public string Q1 { get; set; }
        public string Ans1 { get; set; }
        public string Q2 { get; set; }
        public string Ans2 { get; set; }

    }
}