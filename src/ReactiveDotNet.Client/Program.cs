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

connection.Observe<SequenceStatistics>(nameof(ISequenceHub.PublishSequenceAsync))
	.Skip(30)
	.Delay(TimeSpan.FromSeconds(3))
	.Where(_ => _.Length % 2 == 0)
	.Subscribe(message => Console.WriteLine($"{nameof(ISequenceHub.PublishSequenceAsync)} received: {message}"));

await connection.StartAsync();

Console.WriteLine("Waiting for signals...");

await builder.Build().RunAsync();