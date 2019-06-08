using System;

namespace ReactiveDotNet.KeyWatching
{
	public sealed class KeyEventArgs
		: EventArgs
	{
		public KeyEventArgs(char key) => this.Key = key;

		public char Key { get; }
	}
}
