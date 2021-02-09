// <copyright file="ChatMessage.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Models
{
	using System;
	using XamarinSignalRChat.ViewModels.Base;

	/// <summary>Chat message binable obbject.</summary>
	public class ChatMessage : ExtendedBindableObject
	{
		private string message;
		private string user;
		private DateTime messageDateTime;

		/// <summary>Gets or sets a message.</summary>
		public string Message
		{
			get => this.message;
			set
			{
				this.message = value;
				this.NotifyPropertyChanged(() => this.Message);
			}
		}

		/// <summary>Gets or sets chat user name.</summary>
		public string User
		{
			get => this.user;
			set
			{
				this.user = value;
				this.NotifyPropertyChanged(() => this.User);
			}
		}

		/// <summary>Gets or sets message received datetime.</summary>
		public DateTime MessageDateTime
		{
			get => this.messageDateTime;
			set
			{
				this.messageDateTime = value;
				this.NotifyPropertyChanged(() => this.MessageDateTime);
			}
		}
	}
}