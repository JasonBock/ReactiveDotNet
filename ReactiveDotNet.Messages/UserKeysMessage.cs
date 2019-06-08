namespace ReactiveDotNet.Messages
{
	public sealed class UserKeysMessage
	{
		public UserKeysMessage(string name, char[] keys) =>
			(this.Name, this.Keys) = (name, keys);

		public string Name { get; }
		public char[] Keys { get; }
	}
}
