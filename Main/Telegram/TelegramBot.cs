using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Threading;
using Telegram.Bot.Extensions.Polling;
using System.Threading.Tasks;

namespace moorbot
{
    class TelegramBot
    {
        public TelegramBot(string token, IMessageHandler handler)
        {
            m_handler = handler;
            handler.t_bot = new TelegramBotClient(token);
            Console.WriteLine("Создан бот " + handler.t_bot.GetMeAsync().Result.FirstName);
        }
       public IMessageHandler m_handler;
        public void Start()
        {

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            m_handler.t_bot.StartReceiving(
                m_handler.HandleUpdateAsync,
                m_handler.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.WriteLine("запущен бот " + m_handler.t_bot.GetMeAsync().Result.FirstName);

        }


    }
}
