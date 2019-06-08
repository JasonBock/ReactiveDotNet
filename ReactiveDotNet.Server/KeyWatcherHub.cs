using Microsoft.AspNetCore.SignalR;
using ReactiveDotNet.Contracts;
using ReactiveDotNet.Messages;
using System.Threading.Tasks;

namespace ReactiveDotNet.Server
{
	public sealed class KeyWatcherHub
		: Hub<IKeyWatcherHub>
	{
		public async Task SendNotificationAsync(NotificationMessage message) => 
			await this.Clients.All.SendNotificationAsync(
				new NotificationMessage(message.Recipient, message.Title, message.Message));
	}
}