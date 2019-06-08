﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ReactiveDotNet.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveDotNet.Server.Controllers
{
	[Route("api/[controller]")]
	public sealed class KeyWatcherController
	{
		private static readonly string[] BadWords = { "cotton", "headed", "ninny", "muggins" };
		private readonly KeyWatcherHub hub;

		public KeyWatcherController(KeyWatcherHub hub) =>
			this.hub = hub ?? throw new ArgumentNullException(nameof(hub));

		public async Task Post([FromBody] UserKeysMessage message)
		{
			var keys = new string(message.Keys.ToArray()).ToLower();
			var foundBadWords = new List<string>();

			foreach (var word in KeyWatcherController.BadWords)
			{
				if (keys.Contains(word))
				{
					foundBadWords.Add(word);
				}
			}

			if (foundBadWords.Count > 0)
			{
				var badWords = string.Join(", ", foundBadWords);
				await this.hub.SendNotificationAsync(new NotificationMessage(
					"ITWatchers@YourCompany.com", "BAD WORDS SAID", $"The user {message.Name} typed the following bad words: {badWords}"));
			}
		}
	}
}