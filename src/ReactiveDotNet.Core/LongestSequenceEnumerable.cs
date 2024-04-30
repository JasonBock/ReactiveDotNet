using Collatz;
using Spackle;
using System.Collections;
using System.Numerics;

namespace ReactiveDotNet.Core;

public sealed class LongestSequenceEnumerable
	: IEnumerable<SequenceStatistics>
{
	private readonly Range<BigInteger> range;

	public LongestSequenceEnumerable(Range<BigInteger> range) =>
		this.range = range;

	public LongestSequenceEnumerator GetEnumerator() => new(this.range);

   IEnumerator<SequenceStatistics> IEnumerable<SequenceStatistics>.GetEnumerator() => this.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<SequenceStatistics>)this).GetEnumerator();

	public struct LongestSequenceEnumerator
		: IEnumerator<SequenceStatistics>
	{
		private readonly Range<BigInteger> range;
		private BigInteger currentIndex;
		private SequenceStatistics statistic;

		internal LongestSequenceEnumerator(Range<BigInteger> range) =>
			 (this.range, this.statistic, this.currentIndex) = (range, new(0, 0), range.Start);

		public readonly SequenceStatistics Current => this.statistic;

		readonly object IEnumerator.Current => this.statistic;

		public bool MoveNext()
		{
			while (this.currentIndex < this.range.End)
			{
				var sequence = CollatzSequenceGenerator.Generate<BigInteger>(this.currentIndex);

				if (this.statistic.Length < sequence.Length)
				{
					this.statistic = new SequenceStatistics((int)this.currentIndex, sequence.Length);
					this.currentIndex++;
					return true;
				}

				this.currentIndex++;
			}

			return false;
		}

		public void Dispose() => this.Reset();

		public void Reset() => (this.statistic, this.currentIndex) = (new(0, 0), this.range.Start);
	}
}