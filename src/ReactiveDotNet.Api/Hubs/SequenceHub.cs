using Microsoft.AspNetCore.SignalR;
using ReactiveDotNet.Core;

namespace ReactiveDotNet.Api.Hubs;

public sealed class SequenceHub
	: Hub<ISequenceHub>
{
   public async Task PublishSequenceAsync(SequenceStatistics statistics) => 
		await this.Clients.All.PublishSequenceAsync(statistics);
}