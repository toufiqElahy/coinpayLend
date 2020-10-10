using System;
using System.Net;
using System.Threading.Tasks;
using EthMLM.Models;
using EthMLM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Nethereum.Web3;

namespace EthMLM.Controllers
{
    [Authorize]
    public class EthStvoController : Controller
    {
        private INodeServices nodeServices;
        public EthStvoController(INodeServices node)
        {
            nodeServices = node;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Metamask()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Viewtree(string addr)
        {
            return View();
        }
        public async Task<IActionResult> Upgrade(decimal price)//buy leve 2-6
        {
            TempData["hash"] = await GetTxId(price, Contract.ContractAddress);
            return RedirectToAction("wallet");
        }
        public IActionResult wallet()
        {
            string ethAddr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            string refId = User.FindFirst(System.Security.Claims.ClaimTypes.Sid).Value;//refid
            if (string.IsNullOrWhiteSpace(refId))
            {

            }
            ViewBag.refId = refId;
            return View("wallet", ethAddr);
        }
        [AllowAnonymous]
        public IActionResult Reports(string ethAddr = null)
        {
            ethAddr = ethAddr == null ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value : ethAddr;
            return View("Reports", ethAddr);
        }
        [AllowAnonymous]
        public IActionResult Spendings(string ethAddr = null)
        {
            ethAddr = ethAddr == null ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value : ethAddr;
            return View("Spendings", ethAddr);
        }
        [AllowAnonymous]
        public IActionResult LostETH(string ethAddr = null)
        {
            ethAddr = ethAddr == null ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value : ethAddr;
            return View("LostETH", ethAddr);
        }
        public IActionResult Invite()
        {
            string ethAddr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            var id = nodeServices.InvokeExportAsync<string>("wwwroot/Scripts/Telegram.js", "getId", ethAddr).Result;
            //var callbackUrl = Url.Action("Login", "Account", new { refId = id }, protocol: HttpContext.Request.Scheme);
            //ViewBag.callbackUrl = callbackUrl;
            if (id == "0")
            {
                return Content("Please Join on Coinpay.CR Affiliate Game to get your Referral Link");
            }

            return View("Invite", id);
        }
        public IActionResult SendETH()
        {
            //need price update event listener
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendETH(string addr, decimal amnt, decimal feee)//string addrHolder, 
        {
            return View();
        }
        public async Task<string> Connect(string refid, decimal price)
        {

            return await GetTxId(price, Contract.ContractAddress, refid);
        }
        public async Task<string> GetTxId(decimal price, string addressTo, string data = "0x")
        {
            string ethAddr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            string email = User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            string password = User.FindFirst(System.Security.Claims.ClaimTypes.UserData).Value;//password
            string url = $"https://coinpay.cr/MLMApi/api/MLM/getTrnxInfo?username={email}&password={password}&addressFrom={ethAddr}&addressTo={addressTo}&data={data}&price={price}";

            var isLatest = nodeServices.InvokeExportAsync<bool>("wwwroot/Scripts/Telegram.js", "getPendingTx", ethAddr).Result;
            if (isLatest == false)
            {
                return "You Have Pending Transaction. Please Wait to Mine the Previous Transaction..!";
            }
            if (price >= await Balance(ethAddr))
            {
                return "Insufficient Balance...!";
            }

            try
            {
                //api
                WebClient syncClient = new WebClient();
                string content = syncClient.DownloadString(url);
                content = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(content);
                return content;
            }
            catch (Exception ex)
            {
                //email
                await SendEmail.SendEmailAsync("toufiqelahy@hotmail.com", url + " Exception:" + ex.Message);
                return "Exception." + ex.Message;
            }

        }
        private async Task<decimal> Balance(string ethAddr)
        {
            var web3 = new Web3("https://mainnet.infura.io/v3/f165b595cd184b2a848716830f9804b0");
            var balance = await web3.Eth.GetBalance.SendRequestAsync(ethAddr);
            var etherAmount = Web3.Convert.FromWei(balance.Value);
            return etherAmount;
        }
        [AllowAnonymous]
        public async Task<string> TelegramLink(string ethAddr)
        {
            var content = "";
            var id = await nodeServices.InvokeExportAsync<string>("wwwroot/Scripts/Telegram.js", "getId", ethAddr);
            if (id != "0")
            {
                content = $"{Contract.TlgrmURL}?start=" + id;
            }
            return content;
        }
        [AllowAnonymous]
        public async Task<string> TestUrl(string username, string password, string addressFrom, string addressTo)
        {
            string str = "";
            foreach (char c in password)
            {
                if (c == '#')
                {
                    str += "%23";
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
            
            return str;
        }
    }
}