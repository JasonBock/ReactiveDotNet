namespace ReactiveDotNet.Core.Hubs;

public interface ISequenceHub
{
	Task PublishSequenceAsync(SequenceStatistics statistics);
}