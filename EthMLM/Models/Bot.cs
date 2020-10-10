using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace EthMLM.Models
{
    public class Bot
    {
        private static TelegramBotClient botClient;
        //private static List<Command> commandsList;

        //public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }

            //commandsList = new List<Command>();
            //commandsList.Add(new StartCommand());
            //TODO: Add more commands

            botClient = new TelegramBotClient("1044715419:AAHTlz4vEFuXCijNsrbUz7a8e_kEgjprFcI");
            string hook = $"{Contract.SiteURL}/api/message/update";
            await botClient.SetWebhookAsync(hook);
            return botClient;
        }
    }
}
