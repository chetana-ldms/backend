using LDP_APIs;

var builder = WebApplication.CreateBuilder(args);
var startup = new LDPStartup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method
var app = builder.Build();
startup.Configure(app, builder.Environment); // calling Configure method