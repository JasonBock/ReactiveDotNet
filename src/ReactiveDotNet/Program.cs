using Collatz;
using ReactiveDotNet.Core;
using Spackle;
using System.Numerics;
using SystemTimer = System.Timers.Timer;

var range = new Range<BigInteger>(2, 20_000_000);

//FindLongestSequence(range);

static void FindLongestSequence(Range<BigInteger> range)
{
	Console.WriteLine(nameof(FindLongestSequence));

	var currentStatistic = new SequenceStatistics(0, 0);

	for (var i = range.Start; i < range.End; i++)
	{
		var sequence = CollatzSequenceGenerator.Generate<BigInteger>(i);

		if (currentStatistic.Length < sequence.Length)
		{
			currentStatistic = new SequenceStatistics((int)i, sequence.Length);
			Console.WriteLine(currentStatistic);
		}
	}
}

//FindLongestSequenceWithCallback(range, Console.WriteLine);

static void FindLongestSequenceWithCallback(Range<BigInteger> range, Action<SequenceStatistics> newLongSequence)
{
	Console.WriteLine(nameof(FindLongestSequenceWithCallback));

	var currentStatistic = new SequenceStatistics(0, 0);

	for (var i = range.Start; i < range.End; i++)
	{
		var sequence = CollatzSequenceGenerator.Generate<BigInteger>(i);

		if (currentStatistic.Length < sequence.Length)
		{
			currentStatistic = new SequenceStatistics((int)i, sequence.Length);
			newLongSequence(currentStatistic);
		}
	}
}

//FindLongestSequenceWithTimer(range);

static void FindLongestSequenceWithTimer(Range<BigInteger> range)
{
	Console.WriteLine(nameof(FindLongestSequenceWithTimer));

	var sequenceFinder = new LongestSequence();

	using var timer = new SystemTimer(TimeSpan.FromSeconds(0.25));
	timer.Elapsed += (_, e) => Console.WriteLine(sequenceFinder.Statistic);
	timer.Start();

	sequenceFinder.Calculate(range);
}

//FindLongestSequenceWithEnumeration(range);

static void FindLongestSequenceWithEnumeration(Range<BigInteger> range)
{
	Console.WriteLine(nameof(FindLongestSequenceWithEnumeration));

	var sequences = new LongestSequenceEnumerable(range);

	foreach (var sequence in sequences)
	{
		Console.WriteLine(sequence);
	}
}

//FindLongestSequenceWithEnumerationSimplified(range);

static void FindLongestSequenceWithEnumerationSimplified(Range<BigInteger> range)
{
	static IEnumerable<SequenceStatistics> GetSequenceStatistics(Range<BigInteger> range)
	{
		var currentStatistic = new SequenceStatistics(0, 0);

		for (var i = range.Start; i < range.End; i++)
		{
			var sequence = CollatzSequenceGenerator.Generate<BigInteger>(i);

			if (currentStatistic.Length < sequence.Length)
			{
				currentStatistic = new SequenceStatistics((int)i, sequence.Length);
				yield return currentStatistic;
			}
		}
	}

	Console.WriteLine(nameof(FindLongestSequenceWithEnumerationSimplified));

	foreach (var sequence in GetSequenceStatistics(range))
	{
		Console.WriteLine(sequence);
	}
}

await FindLongestSequenceWithEnumerationAsync(range);

static async Task FindLongestSequenceWithEnumerationAsync(Range<BigInteger> range)
{
	static (bool, SequenceStatistics?) GetNextSequenceStatistic(BigInteger index, BigInteger end, SequenceStatistics currentStatistic)
	{
		for (var i = index; i < end; i++)
		{
			var sequence = CollatzSequenceGenerator.Generate<BigInteger>(i);

			if (currentStatistic.Length < sequence.Length)
			{
				currentStatistic = new SequenceStatistics((int)i, sequence.Length);
				Console.WriteLine(Environment.CurrentManagedThreadId);
				return (true, currentStatistic);
			}
		}

		Console.WriteLine(Environment.CurrentManagedThreadId);
		return (false, null);
	}

	static async IAsyncEnumerable<SequenceStatistics> GetSequenceStatisticsAsync(Range<BigInteger> range)
	{
		var currentStatistic = new SequenceStatistics(0, 0);
		var index = range.Start;

		while (index < range.End)
		{
			var (foundSequence, statistic) = await Task.Run(
				() => GetNextSequenceStatistic(index, range.End, currentStatistic));

			if (foundSequence)
			{
				currentStatistic = statistic;
				index = statistic!.Start + 1;
				yield return statistic;
			}
			else
			{
				break;
			}
		}
	}

	Console.WriteLine(nameof(FindLongestSequenceWithEnumerationAsync));

	await foreach (var statistic in GetSequenceStatisticsAsync(range))
	{
		Console.WriteLine(statistic);
	}
}