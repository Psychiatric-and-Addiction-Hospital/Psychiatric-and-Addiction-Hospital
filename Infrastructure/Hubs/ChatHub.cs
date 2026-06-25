using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    public class ChatHub: Hub
    {
        private static readonly Dictionary<string, string> OnlineUsers = new();

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (userId != null)
                OnlineUsers[userId] = Context.ConnectionId;

            await Clients.All.SendAsync("UserOnline", userId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = OnlineUsers.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (userId != null)
                OnlineUsers.Remove(userId);

            await Clients.All.SendAsync("UserOffline", userId);
            await base.OnDisconnectedAsync(exception);
        }

        // Typing indicator
        public async Task Typing(string senderId, string receiverId)
        {
            await Clients.User(receiverId)
                .SendAsync("UserTyping", senderId);
        }

        // Delivered update
        public async Task MarkAsDelivered(string messageId, string receiverId)
        {
            await Clients.User(receiverId)
                .SendAsync("MessageDelivered", messageId);
        }
    }
}
