using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnemliBookStore.Business.Abstract.Carts;
using OnemliBookStore.Domain.Extensions;
using OnemliBookStore.Entity.Dtos;
using OnemliBookStore.Ui.EmailServices;
using OnemliBookStore.Ui.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnemliBookStore.Ui.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager; // Identity yapısı
        private SignInManager<User> _signInManager; // cookie işlemleri yönetilir
        private IEmailSender _emailSender;
        private ICartService _cartService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cartService = cartService;
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View(new Login()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Form sayfalarında hırsızlıklara karşı önlem yani get ile swden alınan token ile (form açılırken otomatik token veriliyor) post ederken swye yollanan tokeni kontrol ediyor (Cross Side Attacks)
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı ile hesap oluşturulmamış");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Mailinize gelen linki onaylayınız");
                return View(model);
            }

            // 3. parametre false verirsek tarayıcıyı kapatana kadar açık durabilir true verirsek startupta ne tanımladıysak o olur. f12 Applicationdan cookieye bakabilirsin
            // 4. startupta belirtilen çok yanlış girince hesap kitlensin mi
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/"); // model.ReturnUrl != null ?? model.returnUrl : "/" aynısı
            }

            ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // get ile swden alınan token ile post ederken swye yollanan tokeni kontrol ediyor (Cross Side Attacks)
        public async Task<IActionResult> Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var user = new User()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.UserName,
                Email = register.Email,
            };

            var result = await _userManager.CreateAsync(user, register.Password); // user oluşturuldu
            if (result.Succeeded)
            {
                // token oluşturdu veritabanına kaydetti
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // url oluşturdu. Account/ConfirmEmail?userId?token query string olarak gelcek
                var url = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token });

                await _emailSender.SendEmailAsync(
                    register.Email,
                    "Hesabınızı onaylayınız.",
                    $"Hesabınızı onaylamak için lütfen linke <a href='https://localhost:44361{url}'>tıklayınız</a>.");
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Bir Hata Oluştu.");
            // view kısımdaki asp-validation-for burasıyla senkron çalışır ve hatayı basar.

            //ModelState.AddModelError("Password", "Şifreniz yeterince komplex değil"); 
            //ModelState.AddModelError("UserName", "Kullanıcı adınızı doğru giriniz");
            return View(register);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData.Put("message", new AlertMessage
            {
                Title = "Oturum Kapatıldı",
                Message = "Güvenli Bir Şekilde Kapatıldı",
                AlertType = "warning"
            });
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new AlertMessage
                {
                    Title = "Geçersiz token",
                    Message = "UZun açıklama",
                    AlertType = "danger"
                });

                return View("");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _cartService.InitializeCart(user.Id); // hesabını onayladığı anda kullanıcı potansiyel müşteri oluyor ve cart kaydı açılması lazım

                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Hesabınız onaylandı",
                        Message = "UZun açıklama",
                        AlertType = "success"
                    });

                    return View("");
                }

            }

            TempData.Put("message", new AlertMessage
            {
                Title = "Hesabınız onaylanmadı",
                Message = "UZun açıklama",
                AlertType = "warning"
            });

            return View("");
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View("");
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // url oluşturdu. Account/ResetPassword?userId?token query string olarak gelcek
            var url = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token });

            await _emailSender.SendEmailAsync(
                email,
                "Reset Password.",
                $"Parolanızı değiştirmek için lütfen linke <a href='https://localhost:44361{url}'>tıklayınız</a>.");
            return View("");

        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var model = new ResetPassword //sayfadaki hidden içerisine token yerleşir
            {
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        //private void CreateMessage(string message, string alertType)
        //{
        //    var msg = new AlertMessage
        //    {
        //        Message = message,
        //        AlertType = alertType
        //    };

        //    // return ettiği yerde Redirectle yönlendirme yapıldıığı için TempDatayı kullanmak şart
        //    TempData["message"] = JsonConvert.SerializeObject(msg);
        //    //MEssage ve AlertType stringlerini json formata serialize ediliyor. Kullanılacak yerde deserialize edicem.

        //}
    }
}
