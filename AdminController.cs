using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace Pharmacy.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _whe;


        public AdminController(AppDbContext context, IWebHostEnvironment whe)
        {
            _dbContext = context;
            _whe = whe;

        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return Ok("User not allowed");
        }
        public IActionResult Index()
        {
            var c = _dbContext.Contacts.ToList();
            ViewBag.contact = c;
            var count =_dbContext.Contacts.Count();
            ViewBag.feedbacks = count;

            var q = _dbContext.Quotes.ToList();
            ViewBag.quote = q;

            var cv = _dbContext.UserRegisters.Count();
            ViewBag.UserRegister = cv;

            var ca = _dbContext.Capsules.Count();
            var t = _dbContext.Tablets.Count();
            var lf = _dbContext.LiquidFillings.Count();
            var p = ca + t + lf;
            ViewBag.TProducts = p;

            return View();
        }

        public IActionResult AddCapsule() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCapsule(Capsule capsule, IFormFile capsuleImage)
        {
            if (ModelState.IsValid)
            {
                string ext = Path.GetExtension(capsuleImage.FileName);  
                if(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".mp4" || ext == ".mp3")
                {
                    string root = Path.Combine(_whe.WebRootPath, "Capsule");
                    string fileName = Path.GetFileName(capsuleImage.FileName);
                    string filePath = Path.Combine(root, fileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        capsuleImage.CopyTo(fs);
                    }
                    capsule.CapsuleImage = @"Capsule\" + fileName;
                    _dbContext.Capsules.Add(capsule);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(DetailCapsule));
                }
            }
            return View(capsule);
        }



        public IActionResult AddTablet() => View();


        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult AddTablet(Tablet tablet, IFormFile tabletImage)
        {

            if (ModelState.IsValid)
            {
                string ext = Path.GetExtension(tabletImage.FileName);

                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".mp4" || ext == ".mp3")
                {
                    string root = Path.Combine(_whe.WebRootPath, "Tablet");
                    string fileName = Path.GetFileName(tabletImage.FileName);
                    string filePath = Path.Combine(root, fileName);
                    using (FileStream ss = new FileStream(filePath, FileMode.Create))
                    {
                        tabletImage.CopyTo(ss);
                    }
                    tablet.TabletImage = @"Tablet\" + fileName;
                    _dbContext.Tablets.Add(tablet);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(DetailTablet));
                }
            }
            return View();
        }


        public IActionResult AddLiquidFill() => View();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLiquidFill(LiquidFilling liquidFilling, IFormFile liquidImage)
        {
            if (ModelState.IsValid)
            {
                string ext = Path.GetExtension(liquidImage.FileName);

                     if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".mp4" || ext == ".mp3")
                {
                    string root = Path.Combine(_whe.WebRootPath, "Fill");
                    string fileName = Path.GetFileName(liquidImage.FileName);
                    string filePath = Path.Combine(root, fileName);
                    using (FileStream gs = new FileStream(filePath, FileMode.Create))
                    {
                        liquidImage.CopyTo(gs);
                    }
                    liquidFilling.liquidImage = @"Fill\" + fileName;
                    _dbContext.LiquidFillings.Add(liquidFilling);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(DetailLiquidFill));
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult DetailCapsule()
        {
            var capsule = _dbContext.Capsules;
            return View(capsule);
        }
        [HttpGet]
        public IActionResult DetailTablet()
        {
            var tablet = _dbContext.Tablets;
            return View(tablet);
        }
        [HttpGet]
        public IActionResult DetailLiquidFill()
        {
            var liquid = _dbContext.LiquidFillings;
            return View(liquid);
        }


        public IActionResult EditCapsule(int id)
        {
            Capsule c = _dbContext.Capsules.Find(id);
            return View(c);
        }

        [HttpPost]
        
        public IActionResult EditCapsule(Capsule c)
        {
            Capsule capsuleToEdit = _dbContext.Capsules.Find(c.Capsuleid);
            capsuleToEdit.machinename = c.machinename;
           
            capsuleToEdit.Output = c.Output;
            capsuleToEdit.capsulesize = c.capsulesize;
            capsuleToEdit.machinedimenssion = c.machinedimenssion;
            capsuleToEdit.shippingweight = c.shippingweight;

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(DetailCapsule));
        }

        public IActionResult EditTablet(int id)
        {
            Tablet t = _dbContext.Tablets.Find(id);
            return View(t);
        }

        [HttpPost]
        public IActionResult EditTablet(Tablet t)
        {
            Tablet tabletToEdit = _dbContext.Tablets.Find(t.TabletId);
            tabletToEdit.machinename = t.machinename;
            tabletToEdit.TabletImage = t.TabletImage;
            tabletToEdit.modelnumber = t.modelnumber;
            tabletToEdit.dice = t.dice;
            tabletToEdit.maxpressure = t.maxpressure;
            tabletToEdit.maxdiameteroftablet = t.maxdiameteroftablet;
            tabletToEdit.Maxdepthoffill = t.Maxdepthoffill;
            tabletToEdit.productioncapacity = t.productioncapacity;
            tabletToEdit.machinesize = t.machinesize;
            tabletToEdit.machineweight = t.machineweight;
            tabletToEdit.NetWeight = t.NetWeight;

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(DetailTablet));
        }

        public IActionResult EditLiquid(int id)
        {
            LiquidFilling lf = _dbContext.LiquidFillings.Find(id);
            return View(lf);
        }

        [HttpPost]
        public IActionResult EditLiquid(LiquidFilling lf)
        {
            LiquidFilling lfToEdit = _dbContext.LiquidFillings.Find(lf.LiquidFillingId);
            lfToEdit.machinename = lf.machinename;
            lfToEdit.liquidImage = lf.liquidImage;
            lfToEdit.airpressure = lf.airpressure;
            lfToEdit.airvolume = lf.airvolume;
            lfToEdit.fillingspeed = lf.fillingspeed;
            lfToEdit.fillingrange = lf.fillingrange;

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(DetailLiquidFill));
        }



        public IActionResult JobCareer()
        {
            return View(_dbContext.UserRegisters);
        }

        public IActionResult Contact()
        {
            var c = _dbContext.Contacts;
            return View(c);
        }
        public IActionResult EditContact(int id)
        {
            Contact ct = _dbContext.Contacts.Find(id);
            return View(ct);
        }

        [HttpPost]
        public IActionResult EditContact(Contact ct)
        {
            Contact contactToEdit = _dbContext.Contacts.Find(ct.Id);
            contactToEdit.FullName = ct.FullName;
            contactToEdit.Email = ct.Email;
            contactToEdit.Feedback = ct.Feedback;

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Contact));
        }

        public IActionResult Quote()
        {
            var q = _dbContext.Quotes.ToList();
            return View(q);

        }

        public IActionResult Logout()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register() => View();
        [AllowAnonymous]
        public IActionResult AllUsers() => Ok(_dbContext.Users);

        [AllowAnonymous]
        public IActionResult AdLogin() => View();

        //HTTP POST Routes

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Register(AdminRegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                Admin user = new Admin();
                user.Email = request.Email;
                //user.Password = request.Password;
                CreateHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Role = "admin";

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return passwordHash.SequenceEqual(computedHash);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AdLogin(AdminLoginRequest request)
        {
            if (ModelState.IsValid)
            {
                Admin loggedInUser = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);

                if (loggedInUser != null)
                {
                    if (VerifyHash(request.Password, loggedInUser.PasswordHash, loggedInUser.PasswordSalt))
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, loggedInUser.Email),
                            new Claim(ClaimTypes.Role, loggedInUser.Role)
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        AuthenticationProperties authPropwerties = new AuthenticationProperties()
                        {
                            AllowRefresh = false
                        };
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToAction("Index", "Admin");
                    }
                    
                }
                else
                {
                    return BadRequest("User Not Found");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Capsule capToDelete = _dbContext.Capsules.Find(id);
            _dbContext.Capsules.Remove(capToDelete);
            _dbContext.SaveChanges();
            return RedirectToAction("DetailCapsule");
        }


        [HttpGet]
        public IActionResult TabDelete(int id)
        {
            Tablet tabToDelete = _dbContext.Tablets.Find(id);
            _dbContext.Tablets.Remove(tabToDelete);
            _dbContext.SaveChanges();
            return RedirectToAction("DetailTablet");
        }



        [HttpGet]
        public IActionResult LiqDelete(int id)
        {
            LiquidFilling liqToDelete = _dbContext.LiquidFillings.Find(id);
            _dbContext.LiquidFillings.Remove(liqToDelete);
            _dbContext.SaveChanges();
            return RedirectToAction("DetailLiquidFill");
        }



        public IActionResult EditCimage(int id)
        {
            Capsule c = _dbContext.Capsules.Find(id);
            return View(c);
        }

        [HttpPost]
        public IActionResult EditCimage(Capsule c, IFormFile capsuleImage)
        {
            Capsule capsuleToEdit = _dbContext.Capsules.Find(c.Capsuleid);
            capsuleToEdit.CapsuleImage = c.CapsuleImage;

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(DetailCapsule));
        }

    }
}
