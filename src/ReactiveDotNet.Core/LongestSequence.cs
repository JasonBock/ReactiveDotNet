using Collatz;
using Spackle;
using System.Numerics;

namespace ReactiveDotNet.Core;

public sealed class LongestSequence
{
	public LongestSequence() =>
		this.Statistic = new SequenceStatistics(0, 0);

	public void Calculate(Range<BigInteger> range)
	{
		this.Statistic = new SequenceStatistics(0, 0);

		for (var i = range.Start; i < range.End; i++)
		{
			var sequence = CollatzSequenceGenerator.Generate<BigInteger>(i);

			if (this.Statistic.Length < sequence.Length)
			{
				this.Statistic = new SequenceStatistics(i, sequence.Length);
			}
		}
	}

	public SequenceStatistics Statistic { get; private set; }
}