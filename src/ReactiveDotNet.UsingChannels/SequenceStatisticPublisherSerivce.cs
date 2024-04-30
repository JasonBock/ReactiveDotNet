using Collatz;
using Microsoft.Extensions.Hosting;
using ReactiveDotNet.Core;
using System.Numerics;
using System.Threading.Channels;

namespace ReactiveDotNet.UsingChannels;

internal sealed class SequenceStatisticPublisherSerivce
	: BackgroundService
{
	private readonly ChannelWriter<SequenceStatistics> writer;

	public SequenceStatisticPublisherSerivce(ChannelWriter<SequenceStatistics> writer) =>
		this.writer = writer;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var currentStatistic = new SequenceStatistics(0, 0);

		for (var i = new BigInteger(2); i < new BigInteger(20_000_000); i++)
		{
			var sequence = CollatzSequenceGenerator.Generate<BigInteger>(i);

			if (currentStatistic.Length < sequence.Length)
			{
				currentStatistic = new SequenceStatistics((int)i, sequence.Length);
				await this.writer.WriteAsync(currentStatistic, stoppingToken);
				stoppingToken.ThrowIfCancellationRequested();
			}
		}

		this.writer.Complete();
	}
}