using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Threading;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.InputFiles;
using System.Threading.Tasks;
using System.IO;

namespace moorbot
{
    class StatusCommand : ICatCommandHandler
    {
        public void HandleCommand(CatMessageHandler baseHandler, Update update)
        {
            var random = new Random();
            var botClient = baseHandler.t_bot;
            var i_dataBase = (CatClientDatabase)baseHandler.t_database;
            if (i_dataBase.database.ContainsKey(update.Message.Chat.Id))
            {
                CatClientData data = i_dataBase.database[update.Message.Chat.Id];
                DateTime feedEst =  DateTime.FromOADate(data.lastFeed).AddMinutes(data.feedDelay);
                DateTime petEst = DateTime.FromOADate(data.lastFeed).AddMinutes(data.petDelay);

                string ansv = "Ваш котик" + Environment.NewLine + $"текущее настроение {data.happines}, уровень котика {data.level}" + Environment.NewLine + $" в следующий раз сможете покормить после {feedEst} , сможете погладить после {petEst} " + Environment.NewLine + $" до следующего уровня { (( data.level * 7 + data.level * 7)+data.level*3)- data.happines}";
            
                botClient.SendPhotoAsync(update.Message.Chat, new InputOnlineFile(new MemoryStream(LevelManager.GetPhotoFromLevel(i_dataBase.database[update.Message.Chat.Id].level))), ansv);
            }
            else
            {

                string ansv = "Вы пока не завели котика. Наберите /start для начала";
                botClient.SendTextMessageAsync(update.Message.Chat, ansv);

            }

        }
    }
}
