namespace ReactiveDotNet.Messages
{
	public sealed class NotificationSentMessage
	{
		public NotificationSentMessage(string user, string badWords) =>
			(this.User, this.BadWords) = (user, badWords);

		public string User { get; }
		public string BadWords { get; }
	}
}
