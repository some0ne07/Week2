using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.Json;
using WebApplication2.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection.Metadata;



namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string ip;

        public string SetIp()
        {
        ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();
        return ip;
        }

        static string addocs(ref string args)
        {
            string i = args;
            string date = DateTime.Now.ToString();
            MongoClient dbClient = new MongoClient("mongodb+srv://none:userpassword@cluster0.g4bnbxw.mongodb.net/?retryWrites=true&w=majority");

            var database = dbClient.GetDatabase("user_details");
            var collection = database.GetCollection<BsonDocument>("User");

            var document = new BsonDocument { { "_id", DateTime.Now.ToString() }
                , { "ipaddress",i } };

            collection.InsertOne(document);

            return "address of the user";

        }

        static string getdocs()
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://none:userpassword@cluster0.g4bnbxw.mongodb.net/?retryWrites=true&w=majority");

            var database = dbClient.GetDatabase("user_details");
            var collection = database.GetCollection<BsonDocument>("User");

            var dbList = collection.Find(new BsonDocument()).ToList();
            BsonDocument t = new BsonDocument();
            foreach (var item in dbList)
            {
                t = item;
            }
            return $"Date : {t["_id"].ToString()}, Ip Address : {t["ipaddress"].ToString()}";

        }

        public IActionResult Index()
        {
            string ip = SetIp();
            ViewData["as"] = addocs(ref ip);
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["ip"] = getdocs();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}