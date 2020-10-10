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
    public class LotteryController : Controller
    {
        public IActionResult Index()
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
        public IActionResult UserTickets()
        {
            return View(LotteryModel._userTicket.Where(x=>x.Email==User.Identity.Name).ToList());
        }
        public IActionResult SetWinner()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetWinner(int luckyNumber)
        {
            var ticket = LotteryModel._tickets.First(x => x.Number == luckyNumber);
            LotteryModel._totalWinningTicketSoldAtEnd = 100 - ticket.Available;
            //get winners
            LotteryModel._winnerTicketAtEnd = LotteryModel._userTicket.Where(x => x.TicketNumber == luckyNumber).ToList();
            //reset
            LotteryModel.Refresh();
            return RedirectToAction("Winners");
        }
        public IActionResult Winners()
        {
            return View(LotteryModel._winnerTicketAtEnd);
        }
        public IActionResult TicketsStatus()
        {
            return View(LotteryModel._userTicket);
        }
    }
}