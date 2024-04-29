using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using ReactiveDotNet.Client;
using ReactiveDotNet.Client.Extensions;
using ReactiveDotNet.Core;
using ReactiveDotNet.Core.Hubs;
using System.Reactive.Linq;

var builder = Host.CreateApplicationBuilder(args);

Console.WriteLine("Setting up SignalR client...");

var connection = new HubConnectionBuilder()
	.WithUrl(Shared.ServerUrl)
	.Build();

connection.On<SequenceStatistics>(nameof(ISequenceHub.PublishSequenceAsync), data =>
{
	Console.WriteLine();
	Console.WriteLine($"On: {nameof(ISequenceHub.PublishSequenceAsync)} received: {data}");
	Console.WriteLine();
});

connection.Observe<SequenceStatistics>(nameof(ISequenceHub.PublishSequenceAsync))
	.Skip(30)
	.Delay(TimeSpan.FromSeconds(3))
	.Where(_ => _.Length % 2 == 0)
	.Subscribe(message =>
	{
		Console.WriteLine();
		Console.WriteLine($"{nameof(ISequenceHub.PublishSequenceAsync)} received: {message}");
		Console.WriteLine();
	});

await connection.StartAsync();
await builder.Build().RunAsync();