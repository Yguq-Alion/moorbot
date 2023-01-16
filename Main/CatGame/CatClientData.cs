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
    class CatClientData
    {

        public long ChatId { get; set; }
     
        public double lastPet { get; set; }
        public double lastFeed { get; set; }
        public int petDelay { get; set;}
        public int feedDelay { get; set;}
        public int level { get; set;}
        public int petState { get; set;}
        public int happines { get; set;}
        public CatClientData()
        {
                      petDelay = 0;
          feedDelay = 0;
          level = 1;
          petState = 0;
          happines = 0;


    }
    }
}
