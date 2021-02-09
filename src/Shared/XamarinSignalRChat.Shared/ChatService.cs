// <copyright file="ChatService.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Shared
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.SignalR.Client;
	using XamarinSignalRChat.Shared.EventHandlers;

	/// <summary>Chat service.</summary>
	public class ChatService
	{
		private HubConnection hubConnection;

		private Random random;

		/// <summary>Event handler for connection closed.</summary>
		public event EventHandler<MessageEventArgs> OnConnectionClosed;

		/// <summary>Event handler for entered or exited.</summary>
		public event EventHandler<MessageEventArgs> OnEnteredOrExited;

		/// <summary>Event handler for received message.</summary>
		public event EventHandler<MessageEventArgs> OnReceivedMessage;

		private Dictionary<string, string> ActiveChannels { get; } = new Dictionary<string, string>();

		private bool IsConnected { get; set; }

		/// <summary>Connect to chat service.</summary>
		/// <returns>Async task.</returns>
		public async Task ConnectAsync()
		{
			if (this.IsConnected)
			{
				return;
			}

			await this.hubConnection.StartAsync();
			this.IsConnected = true;
		}

		/// <summary>Disconnect from chat service.</summary>
		/// <returns>Async task.</returns>
		public async Task DisconnectAsync()
		{
			if (!this.IsConnected)
			{
				return;
			}

			try
			{
				await this.hubConnection.DisposeAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}

			this.ActiveChannels.Clear();
			this.IsConnected = false;
		}

		/// <summary>Gets the available chat rooms.</summary>
		/// <returns>List{string} of chat rooms.</returns>
		public List<string> GetRooms()
		{
			return new List<string>
						{
								"General",
								"Private",
						};
		}

		/// <summary>Initialises chat service.</summary>
		/// <param name="urlRoot">URL root of the chat service.</param>
		/// <param name="useHttps">Indicates whether to use HTTPS; Otherwise use HTTP.</param>
		public void Init(string urlRoot, bool useHttps)
		{
			this.random = new Random();
			string port = (urlRoot == "localhost" || urlRoot == "10.0.2.2") ? (useHttps ? ":5001" : ":5000") : string.Empty;
			string url = $"http{(useHttps ? "s" : string.Empty)}://{urlRoot}{port}/hubs/chat";
			this.hubConnection = new HubConnectionBuilder()
			.WithUrl(url)
			.Build();

			this.hubConnection.Closed += async (error) =>
			{
				this.OnConnectionClosed?.Invoke(this, new MessageEventArgs("Connection closed...", string.Empty));
				this.IsConnected = false;
				await Task.Delay(this.random.Next(0, 5) * 1000);
				try
				{
					await this.ConnectAsync();
				}
				catch (Exception ex)
				{
					// Exception!
					Debug.WriteLine(ex);
				}
			};

			this.hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
			{
				this.OnReceivedMessage?.Invoke(this, new MessageEventArgs(message, user));
			});

			this.hubConnection.On<string, string>("ReceiveAllMessage", (user, message) =>
			{
				this.OnReceivedMessage?.Invoke(this, new MessageEventArgs($"Broadcast from {user}.", message));
			});

			this.hubConnection.On<string>("Joined", (user) =>
			{
				this.OnEnteredOrExited?.Invoke(this, new MessageEventArgs($"{user} joined.", user));
			});

			this.hubConnection.On<string>("Entered", (user) =>
			{
				this.OnEnteredOrExited?.Invoke(this, new MessageEventArgs($"{user} entered.", user));
			});

			this.hubConnection.On<string>("Left", (user) =>
			{
				this.OnEnteredOrExited?.Invoke(this, new MessageEventArgs($"{user} left.", user));
			});

			this.hubConnection.On<string>("EnteredOrLeft", (message) =>
			{
				this.OnEnteredOrExited?.Invoke(this, new MessageEventArgs(message, message));
			});
		}

		/// <summary>User join a chat group.</summary>
		/// <param name="group">Name of group to join.</param>
		/// <param name="userName">Name of user to join.</param>
		/// <returns>Async task.</returns>
		public async Task JoinChannelAsync(string group, string userName)
		{
			if (!this.IsConnected || this.ActiveChannels.ContainsKey(group))
			{
				return;
			}

			await this.hubConnection.SendAsync("AddToGroup", group, userName);
			this.ActiveChannels.Add(group, userName);
		}

		/// <summary>User leave a chat group.</summary>
		/// <param name="group">Name of group to leave.</param>
		/// <param name="userName">Name of user to leave.</param>
		/// <returns>Async task.</returns>
		public async Task LeaveChannelAsync(string group, string userName)
		{
			if (!this.IsConnected || !this.ActiveChannels.ContainsKey(group))
			{
				return;
			}

			await this.hubConnection.SendAsync("RemoveFromGroup", group, userName);

			this.ActiveChannels.Remove(group);
		}

		/// <summary>Send a chat message.</summary>
		/// <param name="group">Name of group to send message.</param>
		/// <param name="userName">Name of user sending message.</param>
		/// <param name="message">Chat message.</param>
		/// <returns>Async task.</returns>
		public async Task SendMessageAsync(string group, string userName, string message)
		{
			if (!this.IsConnected)
			{
				throw new InvalidOperationException("Not connected");
			}

			await this.hubConnection.InvokeAsync("SendMessageGroup", group, userName, message);
		}
	}
}