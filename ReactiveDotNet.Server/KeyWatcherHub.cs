using Microsoft.AspNetCore.SignalR;
using ReactiveDotNet.Contracts;
using ReactiveDotNet.Messages;
using System;
using System.Threading.Tasks;

namespace ReactiveDotNet.Server
{
	public sealed class KeyWatcherHub
		: Hub<IKeyWatcherHub>
	{
		public async Task SendNotificationAsync(NotificationMessage message) => 
			await this.Clients.All.SendNotificationAsync(
				new NotificationMessage(message.Recipient, message.Title, message.Message));

		public override async Task OnConnectedAsync() => 
			await Console.Out.WriteLineAsync($"New connection made - {this.Context.UserIdentifier}");

		public override async Task OnDisconnectedAsync(Exception exception) =>
			await Console.Out.WriteLineAsync("Disconnected.");
	}
}