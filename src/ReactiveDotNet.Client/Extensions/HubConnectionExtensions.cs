using Microsoft.AspNetCore.SignalR.Client;
using System.Reactive.Disposables;

namespace ReactiveDotNet.Client.Extensions;

public static class HubConnectionExtensions
{
	public static IObservable<T> Observe<T>(this HubConnection self, string method)
	{
		var observable = new HubConnectionObservable<T>();
		self.On<T>(method, data => observable.On(data));
		return observable;
	}

	private sealed class HubConnectionObservable<T>
		: IObservable<T>
	{
		private readonly List<IObserver<T>> observers = [];

		public IDisposable Subscribe(IObserver<T> observer)
		{
			if (!this.observers.Contains(observer))
			{
				this.observers.Add(observer);
			}

			return Disposable.Empty;
		}

		public void On(T message)
		{
			foreach (var observer in this.observers)
			{
				observer.OnNext(message);
			}
		}
	}
}