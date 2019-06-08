namespace ReactiveDotNet.Messages
{
	public sealed class NotificationMessage
	{
		public NotificationMessage(string recipient, string title, string message) =>
			(this.Recipient, this.Title, this.Message) = (recipient, title, message);

		public string Recipient { get; }
		public string Title { get; }
		public string Message { get; }
	}
}