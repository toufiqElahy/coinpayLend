using EthMLM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EthMLM.Services
{
    public static class CoinPayments
    {
		private static string s_privkey = "";//"497DE79272786bAf7E03C05EE2F71E9e0d41C087E379183E8FcE57Cc91bEC237";
		private static string s_pubkey = "";//"1f339b0d89f29d8ddc2456c92300da28d7b4be202f0c8f421b4c486a105799e4";
        private static readonly Encoding encoding = Encoding.UTF8;

        //public CoinPayments(string privkey, string pubkey)
        //{
        //    s_privkey = privkey;
        //    s_pubkey = pubkey;
        //    if (s_privkey.Length == 0 || s_pubkey.Length == 0)
        //    {
        //        throw new ArgumentException("Private or Public Key is empty");
        //    }
        //}

        public static Dictionary<string, dynamic> CallAPI(string cmd, SortedList<string, string> parms = null)
        {
            if (parms == null)
            {
                parms = new SortedList<string, string>();
            }
            parms["version"] = "1";
            parms["key"] = s_pubkey;
            parms["cmd"] = cmd;

            string post_data = "";
            foreach (KeyValuePair<string, string> parm in parms)
            {
                if (post_data.Length > 0) { post_data += "&"; }
                post_data += parm.Key + "=" + Uri.EscapeDataString(parm.Value);
            }

            byte[] keyBytes = encoding.GetBytes(s_privkey);
            byte[] postBytes = encoding.GetBytes(post_data);
            var hmacsha512 = new System.Security.Cryptography.HMACSHA512(keyBytes);
            string hmac = BitConverter.ToString(hmacsha512.ComputeHash(postBytes)).Replace("-", string.Empty);

            // do the post:
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.WebClient cl = new System.Net.WebClient();
            cl.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            cl.Headers.Add("HMAC", hmac);
            cl.Encoding = encoding;

            var ret = new Dictionary<string, dynamic>();
            try
            {
                string resp = cl.UploadString("https://www.coinpayments.net/api.php", post_data);
                //var decoder = new System.Web.HttpUtility.Script.Serialization.JavaScriptSerializer();
                //ret = decoder.Deserialize<Dictionary<string, dynamic>>(resp);

                ret= JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(resp);
            }
            catch (System.Net.WebException e)
            {
                ret["error"] = "Exception while contacting CoinPayments.net: " + e.Message;
            }
            catch (Exception e)
            {
                ret["error"] = "Unknown exception: " + e.Message;
            }

            return ret;
        }
    }
}
