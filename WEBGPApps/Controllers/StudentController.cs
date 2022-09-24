using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEBGPApps.Data;
using WEBGPApps.Models;

namespace WEBGPApps.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        public class Doc_Cor
        {
            public ApplicationUser user = new ApplicationUser();
            public Courses Course = new Courses();
        }

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationDbContext db1;

        public StudentController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            db1 = context;
            _signInManager = signInManager;
            _userManager = userManager;

        }


        public IActionResult RegisterCourses()
        {
            List<Doc_Cor> doc_Cors = new List<Doc_Cor>();
            Doc_Cor docCor = new Doc_Cor();
            List<DC> cc = db1.DC.ToList();

            foreach (var item in cc)
            {
                docCor.Course = db1.Courses.Find(item.CoursesId);
                docCor.user = db1.Users.Find(item.DoctorId);
                doc_Cors.Add(docCor);
                docCor = new Doc_Cor();
            }
            return View(doc_Cors);
        }


        [HttpPost]
        public IActionResult RegisterCourses(IFormCollection form)
        {
            int idcours = Convert.ToInt32(form["cours"]);
            var idstudent = _userManager.GetUserId(HttpContext.User);

            db1.SC.RemoveRange(
                db1.SC.Where(m => m.CoursesId == idcours && m.StudentId == idstudent).ToList()
                );
            db1.SaveChanges();

            SC SC = new SC();
            SC.Courses = db1.Courses.Find(idcours);
            SC.Student = db1.Users.Find(idstudent);

            db1.SC.Add(SC);
            db1.SaveChanges();

            List<Doc_Cor> doc_Cors = new List<Doc_Cor>();
            Doc_Cor docCor = new Doc_Cor();

            List<DC> cc = db1.DC.ToList();

            foreach (var item in cc)
            {
                docCor.Course = db1.Courses.Find(item.CoursesId);
                docCor.user = db1.Users.Find(item.DoctorId);
                doc_Cors.Add(docCor);
                docCor = new Doc_Cor();
            }
            return View(doc_Cors);
        }


        public IActionResult ShowRegisterCourses()
        {
            List<Doc_Cor> doc_Cors = new List<Doc_Cor>();
            Doc_Cor docCor = new Doc_Cor();

            var idas = _userManager.GetUserId(HttpContext.User);
            List<SC> cc = db1.SC.Where(x => x.StudentId == idas).ToList();

            foreach (var item in cc)
            {
                docCor.Course = db1.Courses.Find(item.CoursesId);
                docCor.user = db1.Users.Find(item.StudentId);
                doc_Cors.Add(docCor);
                docCor = new Doc_Cor();
            }

            return View(doc_Cors);
        }

        public IActionResult TablesAndTimes()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Exam_Info()
        {
            var ss = db1.Quiz.ToList();
            return View(ss);
        }
        [HttpPost]
        public IActionResult Save_AQuiz(int id,Quiz model)
        {
            var iddd = _userManager.GetUserId(HttpContext.User);

            var q = db1.Quiz.Find(Convert.ToInt32(id));

            if (q.Ans1==model.Ans1 && q.Ans2 == model.Ans2)
            {
                Att att = new Att();
                att.AttType = 1;
                att.UserId = iddd;
                att.CoursesId = 1;
                db1.Att.Add(att);
                db1.SaveChanges();
            }
            else
            {
                Att att = new Att();
                att.AttType = 1;
                att.UserId = iddd;
                att.CoursesId = 1;
                db1.Att.Add(att);
                db1.SaveChanges();
            }

            return RedirectToAction("Exam_Info");
        }

    }
}
