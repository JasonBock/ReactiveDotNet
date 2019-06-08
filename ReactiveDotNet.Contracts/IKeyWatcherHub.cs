using ReactiveDotNet.Messages;
using System.Threading.Tasks;

namespace ReactiveDotNet.Contracts
{
	public interface IKeyWatcherHub
	{
		Task SendNotificationAsync(NotificationMessage message);
	}
}
