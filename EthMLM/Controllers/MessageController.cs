using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EthMLM.Models;
using Telegram.Bot.Types;
using Telegram.Bot;
using EthMLM.Services;
using System.Net;
using EthMLM.Data;
using Microsoft.AspNetCore.NodeServices;
using System.IO;
using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace EthMLM.Controllers
{

    [Route("api/message/update")]
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly INodeServices nodeServices;
        public MessageController(ApplicationDbContext ctx, INodeServices nd)
        {
            context = ctx;
            nodeServices = nd;
        }
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "Method GET unuvalable";
        }

        // POST api/values
        [HttpPost]
        public async Task<OkResult> Post()
        {
            string json = "";
            using (var reader = new StreamReader(Request.Body))
            {
                json = await reader.ReadToEndAsync();

            }

            var update = JsonConvert.DeserializeObject<Update>(json);
            if (update == null) return Ok();

            //var commands = Bot.Commands;

            var message = update.Message;
            var botClient = await Bot.GetBotClientAsync();
            await Bot_OnMessageAsync(message, botClient);

            //foreach (var command in commands)
            //{
            //    if (command.Contains(message))
            //    {
            //        await command.Execute(message, botClient);
            //        break;
            //    }
            //}
            return Ok();
        }


        private async Task Bot_OnMessageAsync(Message message, TelegramBotClient bot)
        {
            var chatId = message.Chat.Id.ToString();
            var msgText = message.Text;

            try
            {
                if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                {
                    var user = context.Users.FirstOrDefault(x => x.ChatId == chatId);
                    if (user == null)
                    {
                        if (msgText.StartsWith("/start "))
                        {
                            var refId = msgText.Split(' ')[1];
                            //bot.SendTextMessageAsync(chatId, $"Invitation URL: {Contract.TlgrmURL}?start=" + refId);
                            await bot.SendTextMessageAsync(chatId, $"Login here: {Contract.SiteURL}/Account/Login?chatId=" + chatId + "&refId=" + refId);
                        }
                        else
                        {
                            //await bot.SendTextMessageAsync(chatId, "Please Get Invitation Link to satrt...");
                            await bot.SendTextMessageAsync(chatId, $"Login here: {Contract.SiteURL}/Account/Login?chatId=" + chatId + "&refId=" + Contract.DefaultRefId);
                        }
                        return;
                    }

                    switch (msgText)
                    {
                        case "/reports":
                            await bot.SendTextMessageAsync(chatId, $"Check here: {Contract.SiteURL}/EthStvo/Reports?ethAddr=" + user.EthAddress);

                            break;
                        case "/invite":
                            var id = await nodeServices.InvokeExportAsync<string>("wwwroot/Scripts/Telegram.js", "getId", user.EthAddress);
                            if (id == "0")
                            {
                                await bot.SendTextMessageAsync(chatId, "Click /buy_level to get your Invitation Link");
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(chatId, $"{Contract.TlgrmURL}?start=" + id);
                            }


                            break;
                        case "/logout":
                            context.Users.Remove(user);
                            context.SaveChanges();
                            await bot.SendTextMessageAsync(chatId, $"Logged_Out. Login here: {Contract.SiteURL}/Account/Login?chatId=" + chatId + "&refId=" + user.RefId);

                            break;
                        case "/balance":
                            var blnc = await nodeServices.InvokeExportAsync<string>("wwwroot/Scripts/Telegram.js", "balance", user.EthAddress);

                            await bot.SendTextMessageAsync(chatId, "ETH Address: " + user.EthAddress + " Balance: " + blnc);

                            break;
                        case "/buy_level":
                            ReplyKeyboardMarkup ReplyKeyboard = new[]
                    {
                        new[] { "Yes. I am Sure!", "No" }
                    };

                            ReplyKeyboard.OneTimeKeyboard = true;
                            ReplyKeyboard.ResizeKeyboard = true;

                            await bot.SendTextMessageAsync(
                                chatId,
                                "Are U Sure: ",
                                replyMarkup: ReplyKeyboard);
                            break;
                        case "Yes. I am Sure!":
                            var isLatest = await nodeServices.InvokeExportAsync<bool>("wwwroot/Scripts/Telegram.js", "getPendingTx", user.EthAddress);
                            if (isLatest == false)
                            {
                                await bot.SendTextMessageAsync(chatId, "You Have Pending Transaction. Please Wait to Mine the Previous Transaction..!");
                                break;
                            }
                            var content = await nodeServices.InvokeExportAsync<string>("wwwroot/Scripts/Telegram.js", "connect", user.EthAddress, user.RefId);

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
                                    var bol = await SendEmail.SendEmailAsync("toufiqelahy@hotmail.com", url + " Exception:" + ex.Message);
                                    content = "server Error..!";//"Exception." + ex.Message;
                                }
                            }
                            await bot.SendTextMessageAsync(chatId, content, replyMarkup: new ReplyKeyboardRemove());

                            break;
                        case "/viewtree":
                            await bot.SendTextMessageAsync(chatId, $"Check here: {Contract.SiteURL}/EthStvo/Viewtree?addr=" + user.EthAddress);

                            break;
                        default:
                            const string usage = @"Usage:
						/invite Invitation link to share and get Reward
/buy_level If u wanna buy level to Join/Upgrade
/balance If u wanna see ETH balanace
/viewtree To see all below Users
/reports To see all report
/logout If u wanna logout";
                            await bot.SendTextMessageAsync(chatId,
                            usage,
                        replyMarkup: new ReplyKeyboardRemove());
                            break;
                    }



                }


            }
            catch (Exception ex)
            {
                await bot.SendTextMessageAsync(chatId, "error: " + ex.Message);

            }




        }


    }
}
