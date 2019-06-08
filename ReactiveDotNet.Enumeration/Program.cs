using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReactiveDotNet.Enumeration
{
	class Program
	{
		static async Task Main()
		{
			var source = new CancellationTokenSource();
			source.CancelAfter(3000);

			try
			{
				await foreach (var value in new AsynchronousRandom(10).WithCancellation(source.Token))
				{
					await Console.Out.WriteLineAsync(value.ToString());
				}
			}
			catch (OperationCanceledException)
			{
				await Console.Out.WriteLineAsync("The enumeration was cancelled.");
			}
		}
	}
}