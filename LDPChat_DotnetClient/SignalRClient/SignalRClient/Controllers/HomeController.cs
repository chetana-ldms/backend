
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Diagnostics;
using SignalRClient.Models;

namespace DemoSignalRClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HubConnection connection;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PrivacyAsync()
        {
            var connection = new HubConnectionBuilder().WithUrl("https://localhost:7031/hubs/chat", options => {
                options.Transports = HttpTransportType.WebSockets;
            }).WithAutomaticReconnect().Build();
            await connection.StartAsync();
            string newMessage1;
            connection.On<object>("receiveEvent", (message) => {
                Console.WriteLine(message); //write in the debug console
                var newMessage = JsonSerializer.Deserialize<dynamic>(message.ToString());
                //newMessage1 = $ "{newMessage.chainTip}";
            });
            return View();
        }   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}