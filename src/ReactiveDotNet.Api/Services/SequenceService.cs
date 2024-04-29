using Collatz;
using Microsoft.AspNetCore.SignalR;
using ReactiveDotNet.Api.Hubs;
using ReactiveDotNet.Core;
using Spackle;
using System.Numerics;
using System.Threading.Channels;

namespace ReactiveDotNet.Api.Services;

public sealed class SequenceService
	: BackgroundService
{
	private readonly IHubContext<SequenceHub, ISequenceHub> hub;
	private readonly ChannelReader<Range<BigInteger>> reader;

	public SequenceService(IHubContext<SequenceHub, ISequenceHub> hub, ChannelReader<Range<BigInteger>> reader) =>
		(this.hub, this.reader) = (hub, reader);

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (await this.reader.WaitToReadAsync(stoppingToken))
		{
			var range = await this.reader.ReadAsync(stoppingToken);

			var statistic = new SequenceStatistics(0, 0);

			for (var i = range.Start; i < range.End; i++)
			{
				var sequence = CollatzSequenceGenerator.Generate<BigInteger>(i);

				if (statistic.Length < sequence.Length)
				{
					statistic = new SequenceStatistics(i, sequence.Length);
					await this.hub.Clients.All.PublishSequenceAsync(statistic);
				}
			}
		}
	}
}