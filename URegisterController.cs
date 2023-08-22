using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pharmacy.Data;
using Pharmacy.Models;
using System.Security.Claims;

namespace Pharmacy.Controllers
{
    public class URegisterController : Controller
    {

        private readonly AppDbContext _dbContext;
       
        private readonly IWebHostEnvironment _whe;

        private IHttpContextAccessor _http;


        public URegisterController(AppDbContext context,  IWebHostEnvironment whe, IHttpContextAccessor http)
        {
            _dbContext = context;
          
            _whe = whe;

            _http = http;

        }

        //HTTP GET ROUTE
        public IActionResult URegister() => View();


        //HTTP POST ROUTE
        [HttpPost]
        public IActionResult URegister(CareerRegisterRequest request, Career career, IFormFile resume)
        {
            if (ModelState.IsValid)
            {
                string ext = Path.GetExtension(resume.FileName);
                if(ext == ".jpg"|| ext == ".jpeg" || ext == ".png" || ext == ".docx" || ext == ".pdf")
                { 
                    string root = Path.Combine(_whe.WebRootPath, "Resume");
                    string fileName = Path.GetFileName(resume.FileName);
                    string filePath = Path.Combine(root, fileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        resume.CopyTo(fs);
                    }
                    career.Resume = @"Resume\" + fileName;
                    _dbContext.UserRegisters.Add(career);
                    _dbContext.SaveChanges();
                    
                }

                Career user = new Career();
                user.Name = request.Name;
                user.lastname = request.lastname;
                user.Email = request.Email;
                user.Contact = request.Contact;
                user.Address = request.Address;
                user.Education = request.Education;
                user.Degree = request.Degree;
                user.Password = request.Password;


                _dbContext.UserRegisters.Add(user);
                _dbContext.SaveChanges();

                return RedirectToAction("CareerLogin", "URegister");



            }
            return View();
        }
        public IActionResult CareerLogin(CareerLoginRequest request)
        {
            if (ModelState.IsValid)
            {
                Career loggedInUser = _dbContext.UserRegisters.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

                if (loggedInUser != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, loggedInUser.Email),

                    };
                    _http.HttpContext.Session.SetInt32("UserId", loggedInUser.id);

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties authProperties = new AuthenticationProperties()
                    {
                        AllowRefresh = false
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("User", "User");
                }
                else
                {
                    return BadRequest("User Not Found!!");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            _http.HttpContext.Session.Remove("UserId");
            HttpContext.SignOutAsync();
            return RedirectToAction("User", "User");
        }
    }
}

