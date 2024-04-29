using Microsoft.Extensions.Hosting;
using ReactiveDotNet.Core;
using System.Threading.Channels;

namespace ReactiveDotNet.UsingChannels;

internal sealed class SequenceStatisticConsumerSerivce
	: BackgroundService
{
	private readonly ChannelReader<SequenceStatistics> reader;

	public SequenceStatisticConsumerSerivce(ChannelReader<SequenceStatistics> reader) =>
		this.reader = reader;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (await this.reader.WaitToReadAsync(stoppingToken))
		{
			Console.WriteLine(await this.reader.ReadAsync(stoppingToken));
		}
	}
}