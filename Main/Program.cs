using System;
using System.Data;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Threading;
using System.Threading.Tasks;

using System.Text;
using System.Net;


namespace moorbot
{
    class Program
    {
        //""
        static string token = System.Environment.GetEnvironmentVariable("token");

        public static TelegramBot moorbot;
        static void Main(string[] args)
        {


             moorbot = new TelegramBot(token, new CatMessageHandler());
            moorbot.Start();
            bool run = true;
            Task.Run(HttpServer.Start);

            while (run)
            {
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "stop":
                        run = false;

                        break;
                    case "message":
                        Console.WriteLine("Type a message");
                        string message = Console.ReadLine();
                        foreach (var client in ((CatClientDatabase)moorbot.m_handler.t_database).database.Values)
                        {
                            moorbot.m_handler.t_bot.SendTextMessageAsync(new ChatId(client.ChatId), message);

                        }

                        break;
                    default:
                        Console.WriteLine("no such command");
                        break;



                }


            }
        }
    }







    class HttpServer
    {
        public static HttpListener listener;
        public static string url = "http://*:80/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>HttpListener Example</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"shutdown\">" +
            "      <input type=\"submit\" value=\"ћ€укнуть\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";


        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("ћ€у requested");
                    foreach (var client in ((CatClientDatabase)Program.moorbot.m_handler.t_database).database.Values)
                    {
                        string message = CatMessageHandler.ansvers[new Random().Next(0, CatMessageHandler.ansvers.Count)];
                        Program.moorbot.m_handler.t_bot.SendTextMessageAsync(new ChatId(client.ChatId), message);

                    }
                }

                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }


        public static void Start()
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }
    }

}
