using System;
using System.Collections.Generic;

namespace ReactiveDotNet.KeyWatching
{
	public sealed class ObservingCancellingKeyWatcher
		: IObserver<char>
	{
		private readonly string id;
		private readonly KeyWatcherBase keyWatcher;
		private readonly Queue<char> buffer = new Queue<char>();
		private readonly string terminator;

		public ObservingCancellingKeyWatcher(string id, KeyWatcherBase keyWatcher, string terminator) =>
			(this.id, this.keyWatcher, this.terminator) = (id, keyWatcher, terminator);

		public void OnCompleted() =>
			Console.Out.WriteLine($"{this.id} - {nameof(this.OnCompleted)}");

		public void OnError(Exception error) =>
			Console.Out.WriteLine($"{this.id} - {nameof(this.OnError)} - {error.Message}");

		public void OnNext(char value)
		{
			Console.Out.WriteLine($"{this.id} - {nameof(this.OnNext)} - {value}");
			if (this.CheckForTermination(value)) { this.keyWatcher.Cancel(); }
		}

		private bool CheckForTermination(char key)
		{
			this.buffer.Enqueue(key);

			while (this.buffer.Count > this.terminator.Length)
			{
				this.buffer.Dequeue();
			}

			if (this.buffer.Count == this.terminator.Length)
			{
				var termination = new string(this.buffer.ToArray());

				if (termination == this.terminator)
				{
					return true;
				}
			}

			return false;
		}
	}
}
