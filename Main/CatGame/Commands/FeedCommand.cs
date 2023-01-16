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
    class FeedCommand : ICatCommandHandler
    {

        public void HandleCommand(CatMessageHandler baseHandler, Update update)
        {
            var random = new Random();
            var botClient = baseHandler.t_bot;
            var i_dataBase = (CatClientDatabase)baseHandler.t_database;
            if (i_dataBase.database.ContainsKey(update.Message.Chat.Id))
            {
                var data = i_dataBase.database[update.Message.Chat.Id];
                if (DateTime.Now.ToOADate() > DateTime.FromOADate(data.lastFeed).AddMinutes(data.feedDelay).ToOADate())
                {
                    if (data.level < 10)
                    {
                        data.happines = data.happines + random.Next(4, 8);
                        data.level = LevelManager.CalcLevel(data.level, data.happines);
                        string ansv = "Котик съел вкусняшку!!!" + Environment.NewLine + $"текущее настроение {data.happines}, уровень котика {data.level}";
                        
                  
                        botClient.SendPhotoAsync(update.Message.Chat, new InputOnlineFile(new MemoryStream(LevelManager.GetPhotoFromLevel(i_dataBase.database[update.Message.Chat.Id].level))),ansv);
                     data.feedDelay = 20; data.lastFeed = DateTime.Now.ToOADate();
                    }
                    else
                    {
                        data.happines = data.happines + random.Next(4, 8);

                        string ansv = "Котик съел вкусняшку!!!" + Environment.NewLine + $"текущее настроение {data.happines}, уровень котика {data.level}, Уровень максимальный";

                        botClient.SendPhotoAsync(update.Message.Chat, new InputOnlineFile(new MemoryStream(LevelManager.GetPhotoFromLevel(i_dataBase.database[update.Message.Chat.Id].level))), ansv);
                        data.feedDelay = 20; data.lastFeed = DateTime.Now.ToOADate();

                    }
                }
                else
                {
                    string ansv = "Котик не голоден" + Environment.NewLine + $"текущее настроение {data.happines}, уровень котика {data.level}";
                   
                    botClient.SendPhotoAsync(update.Message.Chat, new InputOnlineFile(new MemoryStream(LevelManager.GetPhotoFromLevel(i_dataBase.database[update.Message.Chat.Id].level))), ansv);
                }
            }
            else
            {

                string ansv = "Вы пока не завели котика. Наберите /start для начала";
                botClient.SendTextMessageAsync(update.Message.Chat, ansv);

            }

        }



    }
}
