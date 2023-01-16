using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Threading;
using System.Collections.Generic;
using Telegram.Bot.Extensions.Polling;
using System.Threading.Tasks;

namespace moorbot
{
    interface ICatCommandHandler
    {
        public void HandleCommand(CatMessageHandler baseHandler, Update update);
    }
}
