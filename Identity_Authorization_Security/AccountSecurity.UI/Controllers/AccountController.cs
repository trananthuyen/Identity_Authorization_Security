using AccountManager.Core.Domain.IdentifyEntities;
using AccountManager.Core.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccountSecurity.UI.Controllers
{
    [Route("[Controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("/")]
        public IActionResult Index()
        {
            // Chuyển hướng đến Account/Login
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
        {
            if(ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(signUpDTO);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = signUpDTO.Email,
                PhoneNumber = signUpDTO.Phone,
                AccountName = signUpDTO.PersonName,
                UserName = signUpDTO.AccountName,

            };

            IdentityResult result = await _userManager.CreateAsync(user, signUpDTO.Password); //bo thong tin vao database, passowrd se tu dong chuyen sang ham bam trong database

            if (result.Succeeded)
            {
                //sign in
                await _signInManager.SignInAsync(user, isPersistent: false); //dk nguoi dung va luu cooke
                //isPersistent: true se luu cooke dang nhap, khi ban truy cap vao trang thi trang web se dang nhap ho ban.
            
                return View("~/Views/Home/Index.cshtml");
            } else
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("SignUp", error.Description);
                }
            }

            return View(signUpDTO);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.AccountName, loginDTO.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                return View("~/Views/Home/Index.cshtml");
            }

            ModelState.AddModelError("Login", "Invalid account name or password");
            return View(loginDTO);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return  View("Account/Login");
        }

        public async Task<IActionResult> IsEmailAlreadyRegistered(string AccountName) //kiem tra ma khong can load de xem ten tai khoan da dang ki hay chua
        {
            ApplicationUser user = await _userManager.FindByNameAsync(AccountName);
            if (user == null)
            {
                return Json(true); //valid
            }
            else
            {
                return Json(false);    //invalid
            }
        }

    }
}
