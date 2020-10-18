using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthMLM.Models
{
	public class Loan
	{
		public decimal usd{ get; set; }//borrowed usd
		public string coin { get; set; }
		public int cRate { get; set; } //collateral rate
		public decimal amnt { get; set; } //collateral amount
		public int days { get; set; }
		public int iRate { get; set; } //interest rate
		public decimal rPay { get; set; } //repay usd
		public DateTime date { get; set; } = DateTime.UtcNow;
		public string Email { get; set; }
		public string LenderEmail { get; set; } = "";
		public string status { get; set; } = "Pending";
	}
	public class UserWallet
	{
		public string Email { get; set; }
		public decimal BTC { get; set; } = 1;
		public decimal ETH { get; set; } = 1;
		public decimal USD { get; set; } = 1000;
	}
	public static class UserWalletModel
	{
		public static List<UserWallet> _userWallet = new List<UserWallet>();
	}
		public static class LoanModel
	{
		public static List<Loan> _loans= new List<Loan>();
		public static List<UserTicket> _userTicket = new List<UserTicket>();
		public static List<UserTicket> _winnerTicketAtEnd = new List<UserTicket>();//
		public static int _collectedFund =0 ;
		public static int _collectedFundAtEnd = 0;
		public static int _totalWinningTicketSoldAtEnd = 0;
		const int maxTicket = 99;

		private static List<Ticket> InitTickets()
        {
			var tickets = new List<Ticket>();
			for (int i = 1; i <= maxTicket; i++)
            {
				tickets.Add(new Ticket { Number = i });
			}
			return tickets;
		}

		public static void Refresh()
		{
			_userTicket = new List<UserTicket>();
			_collectedFundAtEnd = _collectedFund;
			_collectedFund = 0;
		}

		public static void LockUnlock(Ticket ticket)//lock on 20%, unlock on 10%  //calling from buyTicket..so its unlock
		{
			

		}

	}
}
