using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using JobsityFinancialChat.Models;
using JobsityFinancialChat.Interfaces;
using JobsityFinancialChat.Services;
using System.Threading.Tasks;

namespace JobsityFinancialChat.SignalR
{
    public class SignalRChat : Hub
    {
        private IBotService _botService;

        public async Task Send(string id, string message)
        {
            string nickname;
            try
            {
                _botService = new BotService();
                var user = new ApplicationDbContext().Users.FirstOrDefault(x => x.Id == id);
                nickname = user.Nickname;
            }
            catch 
            {
                nickname = "System";
                message = "Nickname/User not found";
            }
            await Clients.All.broadcastMessage(nickname, message, DateTime.Now.ToString("G"));

            _ = Task.Run(async () => await _botService.ParseMessage(message));
        }


    }
}