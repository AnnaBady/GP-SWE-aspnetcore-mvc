using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using WEBGPApps.Data;
using WEBGPApps.Models;
using System.Drawing;
using System.Drawing.Imaging;

using static QRCoder.PayloadGenerator;


namespace WEBGPApps.Controllers
{
    
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {

        public class Doc_Cor
        {
            public ApplicationUser user = new ApplicationUser();
            public Courses Course = new Courses();
        }

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationDbContext db1;

        public DoctorController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,ApplicationDbContext context)
        {
            db1 = context;
            _signInManager = signInManager;
            _userManager = userManager;

        }


        [HttpGet]
        public IActionResult AddCoursesD()
        {
            var cc = db1.Courses.ToList();
            return View(cc);
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Doc_Cor> doc_Cors = new List<Doc_Cor>();
            Doc_Cor docCor = new Doc_Cor();

            var idas = _userManager.GetUserId(HttpContext.User);
            List<DC> cc = db1.DC.Where(x => x.DoctorId == idas).ToList();
           
            foreach(var item in  cc)
            {
                docCor.Course = db1.Courses.Find(item.CoursesId);
                docCor.user = db1.Users.Find(item.DoctorId);
                doc_Cors.Add(docCor);
                docCor = new Doc_Cor();
            }

            return View(doc_Cors);
        }


        public IActionResult MyStudents(int id)
        {
            List<SC> SCs = db1.SC.Where(m => m.CoursesId == Convert.ToInt32(id)).ToList();

            List<ApplicationUser> users = new List<ApplicationUser>();
            ApplicationUser user = new ApplicationUser();

            ViewBag.ids = Convert.ToInt32(id);

            foreach(var item in SCs)
            {
                user = db1.Users.Find(item.StudentId);
                users.Add(user);
                user = new ApplicationUser();
            }
            
            return View(users);
        }
        [HttpPost]
        public IActionResult Addmark(int id, string ids,IFormCollection form)
        {
            int mark_mid = Convert.ToInt32(form["mark_mid"]);
            int mark_practical = Convert.ToInt32(form["mark_practical"]);
            int mark_Final = Convert.ToInt32(form["mark_Final"]);
            SC sC = db1.SC.Where(m => m.CoursesId == id && m.StudentId == ids).FirstOrDefault();
            
            sC.mark_mid = mark_mid;
            sC.mark_practical = mark_practical;
            sC.mark_Final = mark_Final;

            if (ModelState.IsValid)
            {
                try
                {
                    db1.Update(sC);
                    db1.SaveChanges();
                }
                catch
                {
                    
                } 
            }
            return  Redirect("/Doctor/MyStudents/"+id);
        }
        
        
        public IActionResult ListStudents()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ViewMaterial()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Save_CoursesD(IFormCollection form)
        {
            int idu = Convert.ToInt32(form["Courses"]);

            var idd = _userManager.GetUserId(HttpContext.User);

            DC dC = new DC();
            dC.Courses = db1.Courses.Find(idu);
            dC.Doctor = db1.Users.Find(idd);
            db1.DC.Add(dC);
            db1.SaveChanges();
            return RedirectToAction("AddCoursesD");
        }
        [HttpGet]
        public IActionResult CreateQRCode()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateQuiz()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save_Quiz(Quiz model)
        {

            db1.Quiz.Add(model);
            db1.SaveChanges();
            return RedirectToAction("CreateQuiz");
        }
        [HttpPost]
        public IActionResult CreateQRCode(QRCodeModel qRCode)
        {

            var WebUri = new Url(qRCode.QRCodeText = "http://mahmoudhamada719-001-site1.htempurl.com/Student/Exam_info");
            string UriPayload = WebUri.ToString();
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(UriPayload, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            ViewBag.QrCodeUri = QrUri;
            return View();
        }
        
    }

    //Extension method to convert Bitmap to Byte Array
    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

}

