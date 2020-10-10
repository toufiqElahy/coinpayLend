using System.Threading.Tasks;
using EthMLM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Collections.Generic;
using System.Web;

namespace EthMLM.Controllers
{
    [AllowAnonymous]
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ApplicationDbContext _ctx;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_ctx = ctx;
        }

        [Route("Login/{returnUrl?}")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }
        [HttpPost]
        [Route("Login/{returnUrl?}")]
        public async Task<IActionResult> Login(string email, string password, string chatId = null, string refId = null)
        {
            //api
            WebClient syncClient = new WebClient();
            string content = "";
            try
            {
                //email = HttpUtility.UrlEncode(email);
                //password = HttpUtility.UrlEncode(password);
                string str = "";
                foreach (char c in password)
                {
                    if (c == '#')
                    {
                        str += "%23";
                    }
                    else if (c == '%')
                    {
                        str += "%25";
                    }
                    else if (c == '&')
                    {
                        str += "%26";
                    }
                    else
                    {
                        str += c;
                    }
                }

                password = str;

                //content = syncClient.DownloadString($"https://coinpay.cr/MLMApi/api/MLM/Login?username={email}&password={password}");
                
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Invalid login attempt." + ex.Message;
                return View();
            }
            //content = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(content);
            content = "0xc52CAD8E5D577AD027d50e8a93F860abeE11b33c";

            var addr = new Nethereum.Util.AddressUtil();

            if (addr.IsValidEthereumAddressHexFormat(content))
            {
                if (chatId != null)
                {
                    var usr = new ApplicationUser { UserName = email, Email = email, Passwrd = password, ChatId = chatId, EthAddress = content, RefId = refId };
                    var result = await _userManager.CreateAsync(usr, password);
                    _ = Program.bot.SendTextMessageAsync(chatId, @"Usage:
						/invite Invitation link to share and get Reward
/buy_level If u wanna buy level to Join/Upgrade
/balance If u wanna see ETH balanace
/viewtree To see all below Users
/reports To see all report
/logout If u wanna logout
						");
                    return Redirect(Contract.TlgrmURL);
                }

                var user = new ApplicationUser();//_userManager.FindByNameAsync(email).Result;
                user.Id = content;
                user.UserName = email;
                user.SecurityStamp = email;

                IList<Claim> claimCollection = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, password)
                , new Claim(ClaimTypes.Sid, refId+"")

            };

                await _signInManager.SignInWithClaimsAsync(user, true, claimCollection);
                return RedirectToAction(nameof(LendController.Index), "Lend");
            }

            TempData["msg"] = content;
            return View();
        }
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }


    }
}