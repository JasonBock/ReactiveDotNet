using ReactiveDotNet.Core;

namespace ReactiveDotNet.Api.Hubs;

public interface ISequenceHub
{
	Task PublishSequenceAsync(SequenceStatistics statistics);
}