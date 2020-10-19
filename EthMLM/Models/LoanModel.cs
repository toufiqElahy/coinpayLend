using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthMLM.Models
{
	public class Loan
	{
		public double usd{ get; set; }//borrowed usd
		public string coin { get; set; }
		public int cRate { get; set; } = LoanModel._cRate; //collateral rate
		public double amnt { get; set; } //collateral amount=coinPrice*2 //liquidate if price falls 20%
		public double coinPrice { get; set; }
		public int days { get; set; }
		public double iRate { get; set; }  //interest rate
		public double rPay { get; set; } //repay usd
		public DateTime date { get; set; } = DateTime.UtcNow;
		public string Email { get; set; }
		public string LenderEmail { get; set; } = "";
		public string status { get; set; } = "Pending";
	}
	public class UserWallet
	{
		public string Email { get; set; }
		public double BTC { get; set; } = 1;
		public double ETH { get; set; } = 1;
		public double USD { get; set; } = 1000;
	}
	public static class UserWalletModel
	{
		public static List<UserWallet> _userWallet = new List<UserWallet>();
	}
		public static class LoanModel
	{
		public static List<Loan> _loans= new List<Loan>();
		public static UserWallet  _coinpayUserWallet = new UserWallet { Email = "coinpay@coinpay.com",BTC=10,ETH=10,USD=10000};
		//yearly interest rate=18.25%  , daily=0.05%
		public static double _dailyInterestRate = 0.0005;
		public static int _allowanceRate = 25;//user cant take loan more than 25% of the coin
		public static int _cRate = 40;
	}
}
