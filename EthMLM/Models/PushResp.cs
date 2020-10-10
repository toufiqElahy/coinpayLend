using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthMLM.Models
{
	using System;
	using System.Collections.Generic;

	using System.Globalization;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public partial class PushResp
	{
		[JsonProperty("nonce")]
		public long Nonce { get; set; }

		[JsonProperty("gasPrice")]
		public GasLimit GasPrice { get; set; }

		[JsonProperty("gasLimit")]
		public GasLimit GasLimit { get; set; }

		[JsonProperty("to")]
		public string To { get; set; }

		[JsonProperty("value")]
		public GasLimit Value { get; set; }

		[JsonProperty("data")]
		public string Data { get; set; }

		[JsonProperty("chainId")]
		public long ChainId { get; set; }

		[JsonProperty("v")]
		public long V { get; set; }

		[JsonProperty("r")]
		public string R { get; set; }

		[JsonProperty("s")]
		public string S { get; set; }

		[JsonProperty("from")]
		public string From { get; set; }

		[JsonProperty("hash")]
		public string Hash { get; set; }
	}

	public partial class GasLimit
	{
		[JsonProperty("_hex")]
		public string Hex { get; set; }
	}

	public partial class PushResp
	{
		public static PushResp FromJson(string json) => JsonConvert.DeserializeObject<PushResp>(json, EthMLM.Models.Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this PushResp self) => JsonConvert.SerializeObject(self, EthMLM.Models.Converter.Settings);
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}
}
