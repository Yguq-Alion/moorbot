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
    class PetCommand : ICatCommandHandler
    {
        public void HandleCommand(CatMessageHandler baseHandler, Update update)
        {
            var random = new Random();
            var botClient = baseHandler.t_bot;
            var i_dataBase = (CatClientDatabase)baseHandler.t_database;
            if (i_dataBase.database.ContainsKey(update.Message.Chat.Id))
            {
                var data = i_dataBase.database[update.Message.Chat.Id];
                if (DateTime.Now.ToOADate() > DateTime.FromOADate(data.lastPet).AddMinutes(data.petDelay).ToOADate())
                {
                    if (data.level <10)
                    {   data.happines = data.happines + random.Next(1,3);
                        data.level = LevelManager.CalcLevel(data.level,data.happines);
                        string ansv = "Котик поглажен!!!" + Environment.NewLine + $"текущее настроение {data.happines}, уровень котика {data.level}";
                     
                        botClient.SendPhotoAsync(update.Message.Chat, new InputOnlineFile(new MemoryStream(LevelManager.GetPhotoFromLevel(i_dataBase.database[update.Message.Chat.Id].level))), ansv);
                        data.petState = data.petState + 1;
                        if (data.petState > 3) { data.petDelay = random.Next(2,10);data.lastPet =DateTime.Now.ToOADate(); data.petState = 0; }
                    }
                    else
                    {
                        data.happines = data.happines + random.Next(1, 3);
                      
                        string ansv = "Котик поглажен!!!" + Environment.NewLine + $"текущее настроение {data.happines}, уровень котика {data.level} МАКСИМАЛЬНЫЙ УРОВЕНЬ";
                       
                        botClient.SendPhotoAsync(update.Message.Chat, new InputOnlineFile(new MemoryStream(LevelManager.GetPhotoFromLevel(i_dataBase.database[update.Message.Chat.Id].level))), ansv);
                        data.petState = data.petState + 1;
                        if (data.petState > 3) { data.petDelay = random.Next(2, 10); data.lastPet = DateTime.Now.ToOADate(); data.petState = 0; }

                    }
                }
                else
                {
                    string ansv = "Котик хочет отдохнуть, погладить не удалось" + Environment.NewLine + $"текущее настроение {data.happines}, уровень котика {data.level}";
                  
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
