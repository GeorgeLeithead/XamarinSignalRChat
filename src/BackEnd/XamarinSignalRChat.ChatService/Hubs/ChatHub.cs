// <copyright file="ChatHub.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.ChatService.Hubs
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.SignalR;

	/// <summary>SignalR chat hub.</summary>
	public class ChatHub : Hub
	{
		/// <summary>Add chat user to group.</summary>
		/// <param name="groupName">Name of group.</param>
		/// <param name="user">Name of user.</param>
		/// <returns>Async task.</returns>
		public async Task AddToGroup(string groupName, string user)
		{
			await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
			await this.Clients.Group(groupName).SendAsync("Entered", user);
		}

		/// <summary>Remove chat user from group.</summary>
		/// <param name="groupName">Name of group.</param>
		/// <param name="user">Name of user.</param>
		/// <returns>Async task.</returns>
		public async Task RemoveFromGroup(string groupName, string user)
		{
			await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, groupName);
			await this.Clients.Group(groupName).SendAsync("Left", user);
		}

		/// <summary>Send message to all users.</summary>
		/// <param name="user">Name of user.</param>
		/// <param name="message">Chat message.</param>
		/// <returns>Async task.</returns>
		public async Task SendMessageAll(string user, string message)
		{
			await this.Clients.All.SendAsync("RecieveAllMessage", user, message);
		}

		/// <summary>Send message to group from a user.</summary>
		/// <param name="groupName">Name of group.</param>
		/// <param name="user">Name of user.</param>
		/// <param name="message">Chat message.</param>
		/// <returns>Async task.</returns>
		public async Task SendMessageGroup(string groupName, string user, string message)
		{
			await this.Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
		}
	}
}