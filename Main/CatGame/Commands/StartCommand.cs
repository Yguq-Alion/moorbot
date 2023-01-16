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
    class StartCommand : ICatCommandHandler
    {

        public void HandleCommand(CatMessageHandler baseHandler, Update update)
        {
            var botClient = baseHandler.t_bot;
            var i_dataBase = (CatClientDatabase)baseHandler.t_database;
            if (i_dataBase.database.ContainsKey(update.Message.Chat.Id))
            {
                string helloMes = "Привет это твой бот-котеночек. Гладь его и давай ему вкусняшки чтобы улучшить настроение котика! Ты также можешь сказать ему что нибудь" + Environment.NewLine + $"текущее настроение {i_dataBase.database[update.Message.Chat.Id].happines}, уровень котика {i_dataBase.database[update.Message.Chat.Id].level}";
             
                botClient.SendPhotoAsync(update.Message.Chat,new InputOnlineFile(new MemoryStream(LevelManager.GetPhotoFromLevel(i_dataBase.database[update.Message.Chat.Id].level))),helloMes);
            }
           else
            {
                CatClientData newCat = new CatClientData();
                newCat.ChatId = update.Message.Chat.Id;
                newCat.lastPet = DateTime.Now.ToOADate();
                newCat.lastFeed = DateTime.Now.ToOADate();
                i_dataBase.database.Add(update.Message.Chat.Id,newCat);
                string helloMes = "Привет это твой бот-котеночек. Гладь его и давай ему вкусняшки чтобы улучшить настроение котика!  Ты также можешь сказать ему что нибудь" + Environment.NewLine + $"текущее настроение {i_dataBase.database[update.Message.Chat.Id].happines}, уровень котика {i_dataBase.database[update.Message.Chat.Id].level}";
                botClient.SendPhotoAsync(update.Message.Chat, new InputOnlineFile(new MemoryStream(LevelManager.GetPhotoFromLevel(i_dataBase.database[update.Message.Chat.Id].level))),helloMes);
            }
        
        }


    }
}
