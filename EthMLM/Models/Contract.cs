using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthMLM.Models
{
	public static class Contract
	{
		public const string DefaultRefId = "1";
		public static string SiteURL = "https://coinpay.cr/mlm";//"https://88.198.197.60";//"https://coinpay-mlm.herokuapp.com";//
		public static string TlgrmURL = "https://telegram.me/ethmlmBot";

		public static double extra = 0.01;
		public static double star1 = 0.06;
		public static string PrivateKey = "F92307ABD3DFC5EEAA00CF856ED34DBD7C84E1BD01A6A7CB5094E69845F2B298";//from db
		public static int decimalNumber = 18;
		public static decimal p = Convert.ToDecimal(Math.Pow(10, decimalNumber));

		public static string etherscan = "https://etherscan.io/";
		public static string txUrl = etherscan + "tx/";//txhashh //https://rinkeby.etherscan.io/tx/0xf905bcc738960a3240921745387fa1a76a889bacbd096be25eaea8f8f9bbac8a
		public static string tokenAddressUrl = etherscan + "token/" + ContractAddress + "?a=";//address //https://rinkeby.etherscan.io/token/0x5e2b81be332eb27a51c30f9fd86a45a060f4e92d?a=0xa6b70392346be7fbfc33d2d098529efc2459b62d
		public static string tokenUrl = etherscan + "token/" + ContractAddress;//https://rinkeby.etherscan.io/token/0x5e2b81be332eb27a51c30f9fd86a45a060f4e92d
																			   //more/ https://rinkeby.etherscan.io/block/4287809

		//public static string Receiver = "0x45BD27bB68B07FE9541B26955d880b70df34B960";

		public static double CoinPrice = 0.001;//eth
		public static double CoinPriceUSD = 0.15;//eth
		public static string CoinUit = "ETH";

		public static string PublicKey = "";//"0xA6b70392346bE7fBFC33d2d098529efC2459b62D";
											///public static string PrivateKey = "";//"F92307ABD3DFC5EEAA00CF856ED34DBD7C84E1BD01A6A7CB5094E69845F2B298";
		public static decimal CoinFee = 0;

		public static string ContractAddress = "0xCA6d6989519E2e0f32b6013Cff0dF2B874254353";//"0x5e2B81be332Eb27A51c30f9FD86a45A060f4E92D";
		public static string abi = @"[{ ""constant"": true, ""inputs"": [], ""name"": ""mainAddress"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""totalFees"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_paused"", ""type"": ""bool"" }], ""name"": ""setPaused"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""address"" }], ""name"": ""userList"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""allowSponsorChange"", ""outputs"": [{ ""name"": """", ""type"": ""bool"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""total"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_userID"", ""type"": ""uint256"" }, { ""name"": ""_wallet"", ""type"": ""address"" }, { ""name"": ""_referrerID"", ""type"": ""uint256"" }, { ""name"": ""_introducerID"", ""type"": ""uint256"" }, { ""name"": ""_referral1"", ""type"": ""address"" }, { ""name"": ""_referral2"", ""type"": ""address"" }, { ""name"": ""_referral3"", ""type"": ""address"" }, { ""name"": ""star"", ""type"": ""uint256"" }], ""name"": ""setUserData"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""name"": ""users"", ""outputs"": [{ ""name"": ""isExist"", ""type"": ""bool"" }, { ""name"": ""wallet"", ""type"": ""address"" }, { ""name"": ""referrerID"", ""type"": ""uint256"" }, { ""name"": ""introducerID"", ""type"": ""uint256"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""name"": ""STAR_FEE"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""paused"", ""outputs"": [{ ""name"": """", ""type"": ""bool"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_star"", ""type"": ""uint256"" }, { ""name"": ""_price"", ""type"": ""uint256"" }], ""name"": ""setStarFee"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": ""_user"", ""type"": ""uint256"" }, { ""name"": ""_star"", ""type"": ""uint256"" }], ""name"": ""viewUserStarActive"", ""outputs"": [{ ""name"": """", ""type"": ""bool"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""name"": ""STAR_PRICE"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""currentUserID"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""tempReferrerID"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_id"", ""type"": ""uint256"" }], ""name"": ""changeReferrerID"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_allowSponsorChange"", ""type"": ""bool"" }], ""name"": ""setAllowSponsorChange"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""deployer"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_currentUserID"", ""type"": ""uint256"" }], ""name"": ""setCurrentUserID"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_mainAddress"", ""type"": ""address"" }], ""name"": ""setMainAddress"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": ""_user"", ""type"": ""uint256"" }], ""name"": ""viewUserReferrals"", ""outputs"": [{ ""name"": """", ""type"": ""address[]"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_star"", ""type"": ""uint256"" }, { ""name"": ""_price"", ""type"": ""uint256"" }], ""name"": ""setStarPrice"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": ""_user"", ""type"": ""uint256"" }], ""name"": ""findFreeReferrer"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""payable"": false, ""stateMutability"": ""view"", ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_owner"", ""type"": ""address"" }], ""name"": ""transferOwnership"", ""outputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""function"" }, { ""inputs"": [], ""payable"": false, ""stateMutability"": ""nonpayable"", ""type"": ""constructor"" }, { ""payable"": true, ""stateMutability"": ""payable"", ""type"": ""fallback"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_user"", ""type"": ""uint256"" }, { ""indexed"": true, ""name"": ""_referrer"", ""type"": ""uint256"" }, { ""indexed"": true, ""name"": ""_introducer"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_time"", ""type"": ""uint256"" }], ""name"": ""Register"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_user"", ""type"": ""uint256"" }, { ""indexed"": true, ""name"": ""_referrer"", ""type"": ""uint256"" }, { ""indexed"": true, ""name"": ""_introducer"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_time"", ""type"": ""uint256"" }], ""name"": ""SponsorChange"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_user"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_star"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_price"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_time"", ""type"": ""uint256"" }], ""name"": ""Upgrade"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_user"", ""type"": ""uint256"" }, { ""indexed"": true, ""name"": ""_referrer"", ""type"": ""uint256"" }, { ""indexed"": true, ""name"": ""_introducer"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_star"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_money"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_fee"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_time"", ""type"": ""uint256"" }], ""name"": ""Payment"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_referrer"", ""type"": ""uint256"" }, { ""indexed"": true, ""name"": ""_referral"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_star"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_money"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_time"", ""type"": ""uint256"" }], ""name"": ""LostMoney"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_referrer"", ""type"": ""uint256"" }, { ""indexed"": true, ""name"": ""_referral"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_star"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_money"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""_time"", ""type"": ""uint256"" }], ""name"": ""IntroducerLostMoney"", ""type"": ""event"" }]";

		public static string InfuraUrl = "https://mainnet.infura.io/v3/f165b595cd184b2a848716830f9804b0";//"https://rinkeby.infura.io/v3/f165b595cd184b2a848716830f9804b0";//rinkeby



		public static double feis = .0001;//fee in %


		public static bool IsQuartzDone { get; set; } = true; //quartz

		public static string PublicKeyCP = "";
		public static string PrivateKeyCP = "";

		public const string EmailNotice = ".You Will Receieve Email Within 1 Minute. Thanks For The Patient.";


		//rewards
		public static int signupReward = 15;
		public static int referralReward = 2;

		public static int fbShareReward = 5;
		public static int redditShareReward = 5;
		public static int twitterShareReward = 5;
		public static int instagramShareReward = 5;

		public static int teleramJoinReward = 5;
	}
}
