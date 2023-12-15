using Messenger.Data;
using Messenger.Data.Interfaces;
using Messenger.Data.Repositories;
using Messenger.Domain;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MessengerContext>(options =>
{
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Transient);

builder.Services.AddIdentity<User, IdentityRole>().AddUserStore<MessengerContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Events = new JwtBearerEvents
//        {
//            OnMessageReceived = context =>
//            {
//                var accessToken = context.Request.Query["access_token"];

//                // если запрос направлен хабу
//                var path = context.HttpContext.Request.Path;
//                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
//                {
//                    // получаем токен из строки запроса
//                    context.Token = accessToken;
//                }
//                return Task.CompletedTask;
//            }
//        };
//    });

app.Run();
