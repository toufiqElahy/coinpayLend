using System;
using System.Linq;
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
    public class LendController : Controller
    {
        public IActionResult Index()
        {
            string email = User.Identity.Name;
            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x=>x.Email==email);
            if (userWallet == null)
            {
                userWallet = new UserWallet { Email = email };
                UserWalletModel._userWallet.Add(userWallet);
            }
            return View(userWallet);
        }
        [HttpPost]
        public IActionResult Index(string coin,decimal amnt)
        {
            string email = User.Identity.Name;
            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);
            if (coin == "BTC")
            {
                userWallet.BTC += amnt;
            }
            else if (coin == "ETH")
            {
                userWallet.ETH += amnt;
            }
            else
            {
                userWallet.USD += amnt;
            }
            return View(userWallet);
        }
        public IActionResult Loan()
        {
            return View(LotteryModel._tickets);
        }
        public IActionResult LoanHistory()
        {
            return View(LotteryModel._tickets);
        }
        public IActionResult BuyTicket(int number)
        {
            var ticket= LotteryModel._tickets.First(x => x.Number == number);
            if (ticket.IsLocked == false &&ticket.Available>0)
            {
                //deduct SCC coin from user account
                LotteryModel._collectedFund += 5;//if ticket price 5 SCC

                ticket.Users += User.Identity.Name + ",";
                ticket.Available = ticket.Available - 1;
                var userTicket = LotteryModel._userTicket.FirstOrDefault(x=>x.Email==User.Identity.Name&&x.TicketNumber==number);
                if (userTicket == null)
                {
                    LotteryModel._userTicket.Add(new UserTicket { Email=User.Identity.Name,TicketNumber=number});
                }
                else
                {
                    userTicket.TicketCount++;
                }
                LotteryModel.LockUnlock(ticket);
            }
            return RedirectToAction("Index");
        }
        public IActionResult BorrowerForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BorrowerForm(Loan mloan)
        {
            string email = User.Identity.Name;
            mloan.Email = email;
            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);
            var amnt = userWallet.BTC;
            if (mloan.coin == "ETH")
            {
                amnt = userWallet.ETH;
            }

            if (mloan.amnt > amnt)
            {
                TempData["msg"] = "U Have Only : "+amnt+" "+mloan.coin;
                return View(mloan);
            }

            //operate
            if (mloan.coin == "ETH")
            {
                userWallet.ETH -= mloan.amnt;
            }
            else
            {
                userWallet.BTC -= mloan.amnt;
            }
            LoanModel._loans.Add(mloan);


            return RedirectToAction("OrderHistory");
        }
        public IActionResult OrderHistory()
        {
            return View(LoanModel._loans.Where(x=>x.Email==User.Identity.Name).ToList());
        }
        public IActionResult OrderCancel(DateTime date)
        {
            string email = User.Identity.Name;
            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);
            var mloan = LoanModel._loans.FirstOrDefault(x => x.Email == email &&x.status=="Pending"&&x.date.ToString().Contains(date.ToString()));
            //operate
            if (mloan.coin == "ETH")
            {
                userWallet.ETH += mloan.amnt;
            }
            else
            {
                userWallet.BTC += mloan.amnt;
            }
            LoanModel._loans.Remove(mloan);
            return RedirectToAction("OrderHistory");
        }
      
        public IActionResult RepayLoan(DateTime date)
        {
            string email = User.Identity.Name;
            var mloan = LoanModel._loans.FirstOrDefault(x => x.Email == email  && x.date.ToString().Contains(date.ToString()));

            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);
            

            if (mloan.rPay > userWallet.USD)
            {
                TempData["msg"] = "U Have Only : " + userWallet.USD + " USD";
                return View("BorrowerForm", mloan);
            }

            //operate
            userWallet.USD -= mloan.rPay;
            var lenderWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.LenderEmail);
            lenderWallet.USD += mloan.rPay;

            if (mloan.coin == "ETH")
            {
                userWallet.ETH += mloan.amnt;
            }
            else
            {
                userWallet.BTC += mloan.amnt;
            }
            LoanModel._loans.Remove(mloan);


            return RedirectToAction("OrderHistory");
        }
        public IActionResult LendUSD()
        {
            //return View(LoanModel._loans.Where(x => x.status == "Pending").ToList());
            return View(LoanModel._loans.Where(x => x.status == "Pending"&&x.Email!=User.Identity.Name).ToList());
        }
        public IActionResult Lend(DateTime date)
        {
            string email = User.Identity.Name;
            var mloan = LoanModel._loans.FirstOrDefault(x => x.status == "Pending" && x.date.ToString().Contains(date.ToString()));

            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.Email);
            var lenderWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == email);


            if (mloan.usd > lenderWallet.USD)
            {
                TempData["msg"] = "U Have Only : " + userWallet.USD + " USD";
                return View("BorrowerForm", mloan);
            }

            //operate
            lenderWallet.USD -= mloan.usd;
            userWallet.USD += mloan.usd;
            mloan.status = "Approved";
            mloan.LenderEmail = email;
           


            return RedirectToAction("LendHistory");
        }
        public IActionResult LendHistory()
        {
            return View(LoanModel._loans.Where(x=>x.status=="Approved"&&x.LenderEmail==User.Identity.Name).ToList());
        }
        public IActionResult FailToRepay(DateTime date)
        {
            var mloan = LoanModel._loans.FirstOrDefault(x => x.date.ToString().Contains(date.ToString()));

            var userWallet = UserWalletModel._userWallet.FirstOrDefault(x => x.Email == mloan.LenderEmail);


            if (mloan.coin == "ETH")
            {
                userWallet.ETH += mloan.amnt;
            }
            else
            {
                userWallet.BTC += mloan.amnt;
            }

            //operate
            LoanModel._loans.Remove(mloan);



            return RedirectToAction("Index");
        }
    }
}