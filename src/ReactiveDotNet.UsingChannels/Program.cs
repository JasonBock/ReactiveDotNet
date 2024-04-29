using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveDotNet.Core;
using ReactiveDotNet.UsingChannels;
using System.Threading.Channels;

var channel = Channel.CreateUnbounded<SequenceStatistics>(new UnboundedChannelOptions
{
	SingleReader = true,
	SingleWriter = true
});

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton(channel.Writer);
builder.Services.AddSingleton(channel.Reader);
builder.Services.AddHostedService<SequenceStatisticConsumerSerivce>();
builder.Services.AddHostedService<SequenceStatisticPublisherSerivce>();

await builder.Build().RunAsync();