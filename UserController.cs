using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _dbContext;
        private IHttpContextAccessor _http;
        private readonly AppDbContext _db;

        public UserController(AppDbContext context, IHttpContextAccessor http, AppDbContext db)
        {
            _dbContext = context;  
            _http = http;
            _db = db;

        }
        public IActionResult User()
        {
            return View();
        }
        public IActionResult About()=>View();
       

        public IActionResult Contact() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(Contact ct)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Contacts.Add(ct);
                _dbContext.SaveChanges();
            }
            else
            {
                return View();
            }

            return RedirectToAction(nameof(Contact));
        }

      
        public IActionResult Quote() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Quote(Quote qt)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Quotes.Add(qt);
                _dbContext.SaveChanges();
            }
            else
            {
                return View();
            }

            return RedirectToAction(nameof(Quote));
        }

        [Authorize]

        public IActionResult Profile()
        {
            int? id = _http.HttpContext.Session.GetInt32("UserId");
            if(id != null)
            {
                Career loggedInUser = _dbContext.UserRegisters.Find(id);
                return View(loggedInUser);
            }
            return BadRequest("No Id Found");
        }

        
      public IActionResult ResumeEdit(int id)
        {
            Career car = _dbContext.UserRegisters.Find(id);
            return View(car);
        }

        [HttpPost]
        public IActionResult ResumeEdit(Career car) 
        {
            Career _cares = _dbContext.UserRegisters.Find(car.id);
            _cares.Resume = car.Resume;
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Profile));
        }

        
        public IActionResult PersonalEdit( int id)
        {
            Career cap = _dbContext.UserRegisters.Find(id);
            return View(cap);
        }

        [HttpPost]
        public IActionResult PersonalEdit(Career cap)
        {
            Career _caper = _dbContext.UserRegisters.Find(cap.id);
                _caper.Name = cap.Name;
            _caper.lastname = cap.lastname;
            _caper.Email = cap.Email;
            _caper.Contact = cap.Contact;
            _caper.Address = cap.Address;

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Profile));
        }

       
            public IActionResult EducationEdit( int id)
        {
            Career cae = _dbContext.UserRegisters.Find(id);
            return View(cae);
        }

        [HttpPost]
        public IActionResult EducationEdit( Career cae)
        {
            Career _caedu = _dbContext.UserRegisters.Find(cae.id);
            _caedu.Education = cae.Education;
            _caedu.Degree = cae.Degree;
           

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Profile));
        }
        [HttpGet]
        public IActionResult UserCapsuleDetail()
        {
            var capsule = _dbContext.Capsules;
            return View(capsule);
            
        }
        [HttpGet]
        public IActionResult UserTabletDetail()
        {
            var tablet = _dbContext.Tablets;
            return View(tablet);
        }
        [HttpGet]
        public IActionResult UserLiquidDetail()
        {
            var lf = _dbContext.LiquidFillings;
            return View(lf);
            
        }
    }
  
}
