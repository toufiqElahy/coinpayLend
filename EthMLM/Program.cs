using System;
using System.Linq;
using EthMLM.Data;
using EthMLM.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Args;
using Microsoft.Extensions.Hosting;
using System.Net;
using EthMLM.Services;

namespace EthMLM
{
	public class Program
	{
		public static readonly TelegramBotClient bot= new TelegramBotClient("1044715419:AAHTlz4vEFuXCijNsrbUz7a8e_kEgjprFcI");
		private static IServiceProvider _serviceProvider;
		public static void Main(string[] args)
		{
			
			var host = CreateHostBuilder(args).Build();
			//_serviceProvider = host.Services;

			//bot.OnMessage += Bot_OnMessage;
			//bot.OnMessageEdited += Bot_OnMessage;
			//bot.StartReceiving();
		
			//bot.StopReceiving();

			host.Run();

			
		}

		private static void Bot_OnMessage(object sender, MessageEventArgs e)
		{
			//if (_serviceProvider == null)
			//{
			//	return;
			//}
			var chatId = e.Message.Chat.Id.ToString();
			var msgText = e.Message.Text;
			using (var scope = _serviceProvider.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var context = services.GetRequiredService<ApplicationDbContext>();
					var nodeServices = services.GetRequiredService<INodeServices>();
					if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
					{
						var user = context.Users.FirstOrDefault(x => x.ChatId == chatId);
						if (user == null)
						{
							if (msgText.StartsWith("/start "))
							{
								var refId = msgText.Split(' ')[1];
								//bot.SendTextMessageAsync(chatId, $"Invitation URL: {Contract.TlgrmURL}?start=" + refId);
								bot.SendTextMessageAsync(chatId, $"Login here: {Contract.SiteURL}/Account/Login?chatId=" + chatId + "&refId=" + refId);
							}
							else
							{
								bot.SendTextMessageAsync(chatId, "Please Get Invitation Link to satrt...");
								//bot.SendTextMessageAsync(chatId, $"Login here: {Contract.SiteURL}/Account/Login?chatId=" + chatId);
							}
							return;
						}

						switch (msgText)
						{
							case "/reports":
								bot.SendTextMessageAsync(chatId, $"Check here: {Contract.SiteURL}/EthStvo/Reports?ethAddr=" + user.EthAddress);

								break;
							case "/invite":
								var id = nodeServices.InvokeExportAsync<string>("wwwroot/Scripts/Telegram.js", "getId", user.EthAddress).Result;
								if (id == "0")
								{
									bot.SendTextMessageAsync(chatId, "Click /buy_level to get your Invitation Link");
								}
								else
								{
									bot.SendTextMessageAsync(chatId, $"{Contract.TlgrmURL}?start=" + id);
								}
								

								break;
							case "/logout":
								context.Users.Remove(user);
								context.SaveChanges();
								bot.SendTextMessageAsync(chatId, $"Logged_Out. Login here: {Contract.SiteURL}/Account/Login?chatId=" + chatId + "&refId=" + user.RefId);

								break;
							case "/balance":
								var blnc = nodeServices.InvokeExportAsync<string>("wwwroot/Scripts/Telegram.js", "balance", user.EthAddress).Result;

								bot.SendTextMessageAsync(chatId, "ETH Address: " + user.EthAddress + " Balance: " + blnc);

								break;
							case "/buy_level":
								var isLatest = nodeServices.InvokeExportAsync<bool>("wwwroot/Scripts/Telegram.js", "getPendingTx", user.EthAddress).Result;
								if (isLatest == false)
								{
									bot.SendTextMessageAsync(chatId, "You Have Pending Transaction. Please Wait to Mine the Previous Transaction..!");
									break;
								}
								var content = nodeServices.InvokeExportAsync<string>("wwwroot/Scripts/Telegram.js", "connect", user.EthAddress, user.RefId).Result;

								var data = content.Split(':');
								if (data.Length > 1)
								{
									string url = $"https://coinpay.cr/MLMApi/api/MLM/getTrnxInfo?username={user.Email}&password={user.Passwrd}&addressFrom={user.EthAddress}&addressTo={Contract.ContractAddress}&data={data[0]}&price={data[1]}";

									try
									{
										//api
										WebClient syncClient = new WebClient();
										content = syncClient.DownloadString(url);
										content = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(content);
										if (content.StartsWith("0x"))
										{
											content = "https://etherscan.io/tx/" + content;
										}
										
									}
									catch (Exception ex)
									{
										//email
										var bol = SendEmail.SendEmailAsync("toufiqelahy@hotmail.com", url + " Exception:" + ex.Message).Result;
										content = "Exception." + ex.Message;
									}
								}
								bot.SendTextMessageAsync(chatId, content);

								break;
							case "/viewtree":
								bot.SendTextMessageAsync(chatId, $"Check here: {Contract.SiteURL}/EthStvo/Viewtree?addr=" + user.EthAddress);

								break;
							default:
								bot.SendTextMessageAsync(e.Message.Chat.Id, @"Usage:
						/invite Invitation link to share and get Reward
/buy_level If u wanna buy level to Join/Upgrade
/balance If u wanna see ETH balanace
/viewtree To see all below Users
/reports To see all report
/logout If u wanna logout
						");
								break;
						}



					}


				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred seeding the DB.");
					bot.SendTextMessageAsync(chatId, "error: " + ex.Message);

				}
			}



		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
