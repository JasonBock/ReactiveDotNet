using System;

namespace ReactiveDotNet.KeyWatching
{
	public sealed class EventedKeyWatcher
		: KeyWatcherBase
	{
		public event EventHandler<KeyEventArgs> KeyLogged;

		protected override void HandleKey(char key) =>
			this.KeyLogged?.Invoke(this, new KeyEventArgs(key));
	}
}
