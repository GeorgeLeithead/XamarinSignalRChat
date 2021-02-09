// <copyright file="MessageEventArgs.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Shared.EventHandlers
{
	/// <summary>Message event arguments interface.</summary>
	public class MessageEventArgs : IMessageEventArgs
	{
		/// <summary>Initialises a new instance of the <see cref="MessageEventArgs" /> class.</summary>
		/// <param name="message">Event message argument.</param>
		/// <param name="user">Event user argument.</param>
		public MessageEventArgs(string message, string user)
		{
			this.Message = message;
			this.User = user;
		}

		/// <summary>Gets the Message for the event argument.</summary>
		public string Message { get; }

		/// <summary>Gets the User for the event argument.</summary>
		public string User { get; }
	}
}