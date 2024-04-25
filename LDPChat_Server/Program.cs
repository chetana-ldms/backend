using LDPChat_Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("ClientPermission", policy =>
//    {
//        policy.AllowAnyHeader()
//            //.AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyOrigin()
//            //.WithOrigins(*)
//            .WithOrigins("http://localhost:3000,http://localhost:3001,http://localhost:3002,http://localhost:3002,http://localhost:3003,http://localhost:3004,http://localhost:3005,http://localhost:3006,http://localhost:3007")
//            .AllowCredentials();
//    });
//});

;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("ClientPermission");
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});
app.UseRouting();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/hubs/chat");
});



app.Run();
