using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot.Extensions.Polling;
using System.IO;
using System.Threading.Tasks;

namespace moorbot
{
    class CatMessageHandler : IMessageHandler
    {


     public   TelegramBotClient t_bot { get; set; }
    public    CatClientDatabase t_database { get; set; }


      public static  List<string> ansvers = new List<string> {

                   "Мур",
                    "Мяу",
                    "Мурмяу",
                    "МурМур",
                    "муррррмяу",
                    " мяу мур?",
                    "мур мур мур",
                    "мяу мур мяу",
                    "кусь",
                    "цапк",
                    "мурррр мурррр",
                    "мууур!",
                    "мууурр?",
                    "мяу...",
                    "миу(",
                    "мурррр))))", "мяяу", "мурмяу", "мяурр", "мяур", "мурмр", "мяумиу", "миумяу", "миумур", "мяумррр", "мурмиу", "мурмрр", "мурмиумяу", "мяяумиу",
            "мурмяумур", "мяумррмиу", "мурмиумур", "мяумурмур", "мямрмур", "мяяумурмиу", "мяумррмур", "мурмурмиу", "мяумиумур", "мяумрмур", "мяумурмрр", "мяяумрррр", "мурмиумрр", "мурмяумр", 
            "мяумурмр", "миумиумяу", "мяумьрмур", "мяяумрмур", "мурмурмр", "мяумиумрр", "мямумяу", "мяумрмр", "мурмяумиу", "мяуммяу", "мяумиумр", "мяумррмрр", "мямяумяу", "мяумрмиу", "мяумурмиумяу",
            "мяумрмрмур", "мяумиумиумяу", "мурмрмрмур", "мурмурмяу", "мурмиумрмур", "мурмиумиумур", "мяумурмиумрр", "мяумиумиумур", "мярмурмяу", "мурмяумрмур", "миумурмиумяу", "мяумрмрмр",
            "миумурмрмр", "мяумурммяу", "мяумрмиумиу", "мяяумиумрмр", "мяумурмрмр", "мяумрмиумр", "мяумурмрмиу", "мяумьрмрмр", "мурмиумрмиу", "мяумиумрмр", "мяумрмиумиумяу", "мурмиумиумрр", 
            "мурмяумиумиу", "мяяумрмрмиу", "мяумиумурмр", "мурмяумрмиумяу", "миумиумрмур", "мяяумурмрмр", "мяумурмиумиумяу"
                    };

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            try
            {
                // Некоторые действия
                //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                  
                    string message = update.Message.Text;
                    Random random = new Random(message.GetHashCode());
                    Console.WriteLine(update.Message.From + ":    " + message);
                    if (commands.ContainsKey(message))
                    {
                        commands[message].HandleCommand(this, update);

                        string json = JsonConvert.SerializeObject((CatClientDatabase)t_database);
                        System.IO.File.WriteAllText("database.json", json);

                    }
                    else
                    {
          

                        await botClient.SendTextMessageAsync(update.Message.Chat, ansvers[random.Next(0, ansvers.Count)]);


                    }
                }

            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }

        }

      public  CatMessageHandler()
        {
            t_database = new CatClientDatabase();
            string database = System.IO.File.ReadAllText("database.json");
            if(database.Length >0)
            {
                t_database = JsonConvert.DeserializeObject<CatClientDatabase>(database);
            }
            commands = new Dictionary<string, ICatCommandHandler>();
            commands.Add("/start",new StartCommand());
            commands.Add("/pet", new PetCommand());
            commands.Add("/feed", new FeedCommand());
            commands.Add("/take", new TakeCommand());
            commands.Add("/status", new StatusCommand());

        }
        Dictionary<string, ICatCommandHandler> commands;
    }
}
