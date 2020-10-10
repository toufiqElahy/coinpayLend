using EthMLM.Models;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.StandardTokenEIP20.ContractDefinition;
using Nethereum.Util;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace EthMLM.Services
{
	public static class Nether
	{
		public static Web3 web3 = new Web3("https://rinkeby.infura.io/v3/f165b595cd184b2a848716830f9804b0");
		public static async Task<decimal> GetFee(string data, string fromAddr, string toAddr, decimal price)
		{
			var cp = new CallInput
			{
				Data = data,
				From = fromAddr,
				To = toAddr
			};
			var amount = UnitConversion.Convert.ToWei(price);
			cp.Value = new Nethereum.Hex.HexTypes.HexBigInteger(amount);
			cp.GasPrice = await web3.Eth.GasPrice.SendRequestAsync();
			cp.Gas = web3.Eth.TransactionManager.EstimateGasAsync(cp).Result;

			var vv = cp.Gas.Value * cp.GasPrice.Value;
			var fee = Web3.Convert.FromWei(vv);//value+fee

			return fee;
		}
	}
}
