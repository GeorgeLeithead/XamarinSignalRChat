// <copyright file="ChatModalViewModel.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Threading.Tasks;
	using System.Windows.Input;
	using Xamarin.Forms;
	using XamarinSignalRChat.Helpers;
	using XamarinSignalRChat.Models;
	using XamarinSignalRChat.ViewModels.Base;

	/// <summary>Chat popup view model.</summary>
	public class ChatModalViewModel : ViewModelBase
	{
		private bool isConnected;

		private bool lastMessageVisible = true;

		private int pendingMessageCount = 0;

		private bool showScroolTap = false;

		private string textToSend;

		/// <summary>Initialises a new instance of the <see cref="ChatModalViewModel"/> class.</summary>
		public ChatModalViewModel()
		{
			if (DesignMode.IsDesignModeEnabled)
			{
				return;
			}

			this.IsBusy = true;
			this.Title = $"Chat - {UserSettingsManager.Group}";
			this.ConnectCommand = new Command(async () => await this.Connect());
			this.DisconnectCommand = new Command(async () => await this.Disconnect());
			this.ChatService.Init(UserSettingsManager.ChatServerIP, UserSettingsManager.UseHttps);

			this.MessageAppearingCommand = new Command<ChatMessage>(this.OnMessageAppearing);
			this.MessageDisappearingCommand = new Command<ChatMessage>(this.OnMessageDisappearing);
			this.OnSendCommand = new Command(async () => await this.SendMessage());
			this.ChatService.OnReceivedMessage += (sender, args) =>
			{
				this.AddLocalMessage(args.Message, args.User);
			};

			this.ChatService.OnEnteredOrExited += (sender, args) =>
			{
				this.AddLocalMessage(args.Message, args.User);
			};

			this.ChatService.OnConnectionClosed += (sender, args) =>
			{
				this.AddLocalMessage(args.Message, args.User);
			};

			this.IsBusy = false;
		}

		/// <summary>Gets connect command.</summary>
		public Command ConnectCommand { get; }

		/// <summary>Gets or sets a queue of delayed chat messages.</summary>
		public Queue<ChatMessage> DelayedMessages { get; set; } = new Queue<ChatMessage>();

		/// <summary>Gets disconnect command.</summary>
		public Command DisconnectCommand { get; }

		/// <summary>Gets or sets a value indicating whether chat service is connected.</summary>
		public bool IsConnected
		{
			get => this.isConnected;
			set
			{
				if (value != this.isConnected)
				{
					this.isConnected = value;
					this.NotifyPropertyChanged(() => this.IsConnected);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether last message is visible.</summary>
		public bool LastMessageVisible
		{
			get => this.lastMessageVisible;
			set
			{
				if (value != this.lastMessageVisible)
				{
					this.lastMessageVisible = value;
					this.NotifyPropertyChanged(() => this.LastMessageVisible);
				}
			}
		}

		/// <summary>Gets or sets message appearing command.</summary>
		public ICommand MessageAppearingCommand { get; set; }

		/// <summary>Gets or sets message dissapearing command.</summary>
		public ICommand MessageDisappearingCommand { get; set; }

		/// <summary>Gets or sets a collection of chat messages.</summary>
		public ObservableCollection<ChatMessage> Messages { get; set; } = new ObservableCollection<ChatMessage>();

		/// <summary>Gets or sets on send command.</summary>
		public ICommand OnSendCommand { get; set; }

		/// <summary>Gets or sets the pending message count.</summary>
		public int PendingMessageCount
		{
			get => this.pendingMessageCount;
			set
			{
				if (value != this.pendingMessageCount)
				{
					this.pendingMessageCount = value;
					this.NotifyPropertyChanged(() => this.PendingMessageCount);
					this.NotifyPropertyChanged(() => this.PendingMessageCountVisible);
				}
			}
		}

		/// <summary>Gets a value indicating whether the pending message count is visible.</summary>
		public bool PendingMessageCountVisible
		{
			get { return this.PendingMessageCount > 0; }
		}

		/// <summary>Gets or sets a value indicating whether to show scroll tap.</summary>
		public bool ShowScrollTap
		{
			get => this.showScroolTap;
			set
			{
				if (value != this.showScroolTap)
				{
					this.showScroolTap = value;
					this.NotifyPropertyChanged(() => this.ShowScrollTap);
				}
			}
		}

		/// <summary>Gets or sets message to send.</summary>
		public string TextToSend
		{
			get => this.textToSend;
			set
			{
				if (value != this.textToSend)
				{
					this.textToSend = value;
					this.NotifyPropertyChanged(() => this.TextToSend);
				}
			}
		}

		private void AddLocalMessage(string message, string user)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			Device.BeginInvokeOnMainThread(() =>
			{
				if (this.LastMessageVisible)
				{
					this.Messages.Insert(0, new ChatMessage() { Message = message, User = user });
				}
				else
				{
					this.DelayedMessages.Enqueue(new ChatMessage() { Message = message, User = user });
					this.PendingMessageCount = this.DelayedMessages.Count;
				}
			});
		}

		private async Task Connect()
		{
			if (this.IsConnected)
			{
				return;
			}

			try
			{
				this.IsBusy = true;
				await this.ChatService.ConnectAsync();
				await this.ChatService.JoinChannelAsync(UserSettingsManager.Group, UserSettingsManager.DisplayName);
				this.IsConnected = true;

				await Task.Delay(500);
				await this.ChatService.SendMessageAsync(UserSettingsManager.Group, UserSettingsManager.DisplayName, $"{UserSettingsManager.DisplayName} connected...");

				// this.AddLocalMessage("Connected...", UserSettingsManager.DisplayName);
			}
			catch (Exception ex)
			{
				this.AddLocalMessage($"Connection error: {ex.Message}", UserSettingsManager.DisplayName);
			}
			finally
			{
				this.IsBusy = false;
			}
		}

		private async Task Disconnect()
		{
			if (!this.IsConnected)
			{
				return;
			}

			// TODO: When this happens is crashes the app!
			await this.ChatService.LeaveChannelAsync(UserSettingsManager.Group, UserSettingsManager.DisplayName);
			await this.ChatService.DisconnectAsync();
			this.IsConnected = false;
			this.AddLocalMessage("Disconnected...", UserSettingsManager.DisplayName);
		}

		private void OnMessageAppearing(ChatMessage message)
		{
			int messageIndex = this.Messages.IndexOf(message);
			if (messageIndex <= 6)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					while (this.DelayedMessages.Count > 0)
					{
						this.Messages.Insert(0, this.DelayedMessages.Dequeue());
					}

					this.ShowScrollTap = false;
					this.LastMessageVisible = true;
					this.PendingMessageCount = 0;
				});
			}
		}

		private void OnMessageDisappearing(ChatMessage message)
		{
			int messageIndex = this.Messages.IndexOf(message);
			if (messageIndex >= 6)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					this.ShowScrollTap = true;
					this.LastMessageVisible = false;
				});
			}
		}

		private async Task SendMessage()
		{
			if (!this.IsConnected)
			{
				await this.DialogService.DisplayAlert("Chat not connected", "Please connect to the server and try again.", "OK");
				return;
			}

			if (string.IsNullOrEmpty(this.TextToSend))
			{
				return;
			}

			try
			{
				this.IsBusy = true;
				string message = this.TextToSend;
				this.TextToSend = string.Empty;
				await this.ChatService.SendMessageAsync(UserSettingsManager.Group, UserSettingsManager.DisplayName, message);
			}
			catch (Exception ex)
			{
				this.AddLocalMessage($"Send failed: {ex.Message}", UserSettingsManager.DisplayName);
			}
			finally
			{
				this.IsBusy = false;
			}
		}
	}
}